using System;
using System.Linq;
using MainModel.Game.GameSessionFactories;
using MainModel.SokobanLevels.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameView.Scenes;

public class CompletedLevelScene : IGameScene
{
    private const float StatisticsPanelWidthPart = 0.6f;
    private const float StatisticsPanelHeightPart = 0.6f;
    private const float StatisticsTextPaddingPart = 0.9f;
    private const float StatisticsTextLinesMargin = 0.05f;

    private readonly string[] _statisticsText;
    
    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D _whitePixel;
    private SpriteFont _font;

    private readonly SceneManager _sceneManager;
    private readonly IGameSessionFactory _gameSessionFactory;
    
    private KeyboardState _previousKeyboardState;

    public CompletedLevelScene(
        SceneManager sceneManager,
        LevelStatistics completedLevelStatistics,
        IGameSessionFactory gameSessionFactory)
    {
        _sceneManager = sceneManager;
        _gameSessionFactory =  gameSessionFactory;
        
        _spriteBatch = new SpriteBatch(_sceneManager.GraphicsDevice);
        _whitePixel = new Texture2D(_sceneManager.GraphicsDevice, 1, 1);
        _whitePixel.SetData([Color.White]);

        _statisticsText =
        [
            $"Уровень {completedLevelStatistics.CurrentLevelIndex + 1} пройден",
            $"Время: {completedLevelStatistics.ElapsedTime:F0} сек.",
            $"Передвижений игрока: {completedLevelStatistics.StorekeeperMovements}",
            $"Перемещений ящиков: {completedLevelStatistics.BoxesMovements}",
            "",
            "Нажмите ENTER, чтобы перейти в меню выбора уровня"
        ];
    }

    public void LoadContent()
    {
        _font = _sceneManager.ContentManager.Load<SpriteFont>("Fonts/fontSokoban");
    }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (IsKeyPressedWithoutHolding(keyboardState, _previousKeyboardState, Keys.Enter))
            BuildLevelMenuScene();

        _previousKeyboardState = keyboardState;
    }

    public void Draw(GameTime gameTime)
    {
        _sceneManager.GraphicsDevice.Clear(Color.Green);

        var windowWidth = _sceneManager.GraphicsDevice.Viewport.Width;
        var windowHeight = _sceneManager.GraphicsDevice.Viewport.Height;
        var statisticsPanelWidth = (int)(windowWidth * StatisticsPanelWidthPart);
        var statisticsPanelHeight = (int)(windowHeight * StatisticsPanelHeightPart);

        var statisticsPanel = new Rectangle(
            (windowWidth - statisticsPanelWidth) / 2,
            (windowHeight - statisticsPanelHeight) / 2,
            statisticsPanelWidth, statisticsPanelHeight);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_whitePixel, statisticsPanel, Color.LightBlue);
        DrawStatisticsText(statisticsPanel);

        _spriteBatch.End();
    }

    private void BuildLevelMenuScene()
    {
        var levelMenuScene = new LevelMenuScene(
            _sceneManager,
            _gameSessionFactory);
        levelMenuScene.LoadContent();

        _sceneManager.CurrentScene = levelMenuScene;
    }

    private void DrawStatisticsText(Rectangle statisticsPanel)
    {
        var measuringLines = _statisticsText
            .Select(line => _font.MeasureString(line))
            .ToArray();
        var maxTextWidth = measuringLines.Max(size => size.X);
        var totalTextHeight = measuringLines.Sum(size => size.Y);

        var linesMargin = statisticsPanel.Height * StatisticsTextLinesMargin;
        totalTextHeight += linesMargin * (_statisticsText.Length - 1);

        var scale = GetTextScale(maxTextWidth, totalTextHeight, statisticsPanel);
        var y = statisticsPanel.Y + (statisticsPanel.Height - totalTextHeight * scale) / 2.0f;

        foreach (var line in _statisticsText)
        {
            var size = _font.MeasureString(line) * scale;
            var x = statisticsPanel.X + (statisticsPanel.Width - size.X) / 2.0f;
            
            _spriteBatch.DrawString(
                _font,
                line,
                new Vector2(x, y),
                Color.Black,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f);

            y += size.Y + linesMargin * scale;
        }
    }

    private static float GetTextScale(float textWidth, float textHeight, Rectangle statisticsPanel)
    {
        var scaleX = (statisticsPanel.Width * StatisticsTextPaddingPart) / textWidth;
        var scaleY = (statisticsPanel.Height * StatisticsTextPaddingPart) / textHeight;

        return Math.Min(scaleX, scaleY);
    }

    private static bool IsKeyPressedWithoutHolding(
        KeyboardState currentState,
        KeyboardState previousState,
        Keys key) => currentState.IsKeyUp(key) && previousState.IsKeyDown(key);
}