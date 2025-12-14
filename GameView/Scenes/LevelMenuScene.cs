using System;
using GameController;
using GameView.Views;
using MainModel.Game.GameSessionFactories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameView.Scenes;

public class LevelMenuScene : IGameScene
{
    private const float HorizontalPaddingPart = 0.05f;
    private const float VerticalPaddingPart = 0.05f;
    private const float CellsMargin = 0.9f;
    private const float TextPadding = 0.9f;
    
    private const float WidthRectangleSizePart = 0.1f;
    private const float HeightRectangleSizePart = 0.1f;

    private readonly SceneManager _sceneManager;
    private readonly IGameSessionFactory _gameSessionFactory;

    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D _whitePixel;
    private SpriteFont _font;

    private int _selectedLevelIndex;
    private int _columnsCount;

    private Rectangle[] _cells;

    private KeyboardState _previousKeyboardState;
    
    public LevelMenuScene(
        SceneManager sceneManager,
        IGameSessionFactory gameSessionFactory)
    {
        _sceneManager = sceneManager;
        _gameSessionFactory = gameSessionFactory;

        _spriteBatch = new SpriteBatch(_sceneManager.GraphicsDevice);
        _whitePixel = new(_sceneManager.GraphicsDevice, 1, 1);
        _whitePixel.SetData([Color.White]);
    }

    public void LoadContent()
    {
        _font = _sceneManager.ContentManager.Load<SpriteFont>("Fonts/fontSokoban");
    }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (IsKeyPressedWithoutHolding(keyboardState, _previousKeyboardState, Keys.Up))
            SelectedLevelIndexRemove(_columnsCount);
        if (IsKeyPressedWithoutHolding(keyboardState, _previousKeyboardState, Keys.Down))
            SelectedLevelIndexAdd(_columnsCount);
        if (IsKeyPressedWithoutHolding(keyboardState, _previousKeyboardState, Keys.Left))
            SelectedLevelIndexRemove(removing: 1);
        if (IsKeyPressedWithoutHolding(keyboardState, _previousKeyboardState, Keys.Right))
            SelectedLevelIndexAdd(adding: 1);

        if (keyboardState.IsKeyDown(Keys.Enter))
            BuildLevel();

        _previousKeyboardState = keyboardState;
    }

    public void Draw(GameTime gameTime)
    {
        PrepareToDraw();
        
        _spriteBatch.Begin();

        for (int i = 0; i < _gameSessionFactory.LevelsCount; i++)
            DrawButton(i);

        _spriteBatch.End();
    }

    private void BuildLevel()
    {
        var gameController = new SokobanGameController(_gameSessionFactory);
        var levelRenderer = new MonoGameLevelView(
            _sceneManager.GraphicsDevice,
            _sceneManager.ContentManager,
            gameController);
        var monogameController = new MonoGameController(gameController);

        gameController.LoadLevel(_selectedLevelIndex);
        
        levelRenderer.LoadContent();
        _sceneManager.CurrentScene = new LevelScene(
            levelRenderer,
            gameController,
            _gameSessionFactory,
            monogameController,
            _sceneManager,
            _selectedLevelIndex);
    }

    private void PrepareToDraw()
    {
        var windowWidth = _sceneManager.GraphicsDevice.Viewport.Width;
        var windowHeight = _sceneManager.GraphicsDevice.Viewport.Height;

        var width = windowWidth * (1.0f - 2 * HorizontalPaddingPart);
        var height = windowHeight * (1.0f - 2 * VerticalPaddingPart);
        var targetCellWidth = width * WidthRectangleSizePart;
        var targetCellHeight = height * HeightRectangleSizePart;

        SetColumnsAndRows(width, targetCellWidth);

        var offsetX = windowWidth * HorizontalPaddingPart;
        var offsetY = windowHeight * VerticalPaddingPart;

        _cells = new Rectangle[_gameSessionFactory.LevelsCount];

        BuildCells(targetCellWidth, targetCellHeight, offsetX, offsetY);
    }

    private void SelectedLevelIndexAdd(int adding) =>
        _selectedLevelIndex = Math.Min(_selectedLevelIndex + adding, _gameSessionFactory.LevelsCount - 1);

    private void SelectedLevelIndexRemove(int removing) =>
        _selectedLevelIndex = Math.Max(_selectedLevelIndex - removing, 0);

    private void SetColumnsAndRows(float width, float targetCellWidth)
    {
        _columnsCount = int.Min(_gameSessionFactory.LevelsCount, int.Max(1, (int)(width / targetCellWidth)));
    }

    private void BuildCells(
        float cellWidth, float cellHeight,
        float offsetX, float offsetY)
    {
        for (int i = 0; i < _gameSessionFactory.LevelsCount; i++)
        {
            var row = i / _columnsCount;
            var column = i % _columnsCount;

            var x = (int)(offsetX + column * cellWidth);
            var y = (int)(offsetY + row * cellHeight);
            
            _cells[i] = new(x, y, (int)(cellWidth * CellsMargin), (int)(cellHeight * CellsMargin));
        }
    }

    private void DrawButton(int levelNumber)
    {
        var currentButton = _cells[levelNumber];
        var text = $"{levelNumber + 1}";

        var backGroundColor =
            levelNumber == _selectedLevelIndex
                ? Color.Orange
                : _gameSessionFactory.CompletedLevels.Contains(levelNumber)
                    ? Color.DarkGreen
                    : Color.Gray;

        _spriteBatch.Draw(_whitePixel, currentButton, backGroundColor);
        var measuredString = _font.MeasureString(text);

        var scaleX = (currentButton.Width * TextPadding) / measuredString.X;
        var scaleY = (currentButton.Height * TextPadding) / measuredString.Y;
        var scale = Math.Min(scaleX, scaleY);

        var sizeText = measuredString * scale;
        var positionText = new Vector2(
            currentButton.X + (currentButton.Width - sizeText.X) / 2.0f,
            currentButton.Y + (currentButton.Height - sizeText.Y) / 2.0f);

        _spriteBatch.DrawString(
            _font,
            text,
            positionText,
            Color.Black,
            0f,
            Vector2.Zero,
            scale,
            SpriteEffects.None,
            0f);
    }
    
    private static bool IsKeyPressedWithoutHolding(
        KeyboardState currentState,
        KeyboardState previousState, 
        Keys key) => currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
}