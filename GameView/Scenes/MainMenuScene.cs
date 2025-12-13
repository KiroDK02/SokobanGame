using System;
using GameController;
using GameView.Views;
using MainModel.Game.GameSessionFactories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameView.Scenes;

public class MainMenuScene : IGameScene
{
    private const float ButtonsPanelWidthPart = 0.6f;
    private const float ButtonsPanelHeightPart = 0.4f;
    private const float ButtonsTextPaddingPart = 0.9f;
    private const float ButtonsWidthMargin = 0.3f;
    private const float ButtonsHeightMargin = 0.3f;
    
    private const int ButtonsCount = 3;

    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D _whitePixel;
    private SpriteFont _font;
    
    private readonly IGameSessionFactory _gameSessionFactory;
    private readonly SceneManager _sceneManager;
    private readonly Action _exitAction;

    private string[] _buttonsText;
    private Rectangle _panelButtons;
    private Rectangle[] _buttons = new Rectangle[ButtonsCount];
    private int _selectedButtonIndex;
    private KeyboardState _previousKeyboardState;

    public MainMenuScene(IGameSessionFactory gameSessionFactory, SceneManager sceneManager, Action exitAction)
    {
        _gameSessionFactory = gameSessionFactory;
        _sceneManager = sceneManager;
        _exitAction = exitAction;
        
        _spriteBatch = new SpriteBatch(_sceneManager.GraphicsDevice);
        _whitePixel = new Texture2D(_sceneManager.GraphicsDevice, 1, 1);
        _whitePixel.SetData([Color.White]);

        _buttonsText =
        [
            "Старт",
            "Список уровней",
            "Выход"
        ];
    }

    public void LoadContent()
    {
        _font = _sceneManager.ContentManager.Load<SpriteFont>("Fonts/fontSokoban");
    }
    
    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (IsKeyPressedWithoutHolding(keyboardState, _previousKeyboardState, Keys.Up))
            _selectedButtonIndex = Math.Max(0, _selectedButtonIndex - 1);
        if (IsKeyPressedWithoutHolding(keyboardState, _previousKeyboardState, Keys.Down))
            _selectedButtonIndex = Math.Min(ButtonsCount - 1, _selectedButtonIndex + 1);

        if (IsKeyPressedWithoutHolding(_previousKeyboardState, keyboardState, Keys.Enter))
            ExecuteCommandButton((MainMenuButtons)_selectedButtonIndex);

        _previousKeyboardState = keyboardState;
    }

    public void Draw(GameTime gameTime)
    {
        PrepareToDraw();
        
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_whitePixel, _panelButtons, Color.CornflowerBlue);
        for (int i = 0; i < ButtonsCount; i++)
            DrawButton(i);
        
        _spriteBatch.End();
    }

    private void PrepareToDraw()
    {
        var windowWidth =  _sceneManager.GraphicsDevice.Viewport.Width;
        var windowHeight = _sceneManager.GraphicsDevice.Viewport.Height;
        
        var panelWidth = (int)(windowWidth * ButtonsPanelWidthPart);
        var panelHeight = (int)(windowHeight * ButtonsPanelHeightPart);
        var panelX = (windowWidth - panelWidth) / 2;
        var panelY = (windowHeight - panelHeight) / 2;
        
        _panelButtons = new Rectangle(panelX, panelY, panelWidth, panelHeight);

        var buttonHeightWithoutMargin = panelHeight / (float)ButtonsCount;
        var buttonWidth = panelWidth * (1.0f - ButtonsWidthMargin);
        var buttonHeight = buttonHeightWithoutMargin * (1.0f - ButtonsHeightMargin);
        var buttonOffsetX = panelX + (panelWidth - buttonWidth) / 2.0f;

        for (int i = 0; i < ButtonsCount; i++)
        {
            var buttonYWithoutMargin = panelY + i * buttonHeightWithoutMargin;
            var buttonY = buttonYWithoutMargin + (buttonHeightWithoutMargin - buttonHeight) / 2.0f;

            _buttons[i] = new(
                (int)buttonOffsetX,
                (int)buttonY,
                (int)buttonWidth,
                (int)buttonHeight);
        }
    }

    private void DrawButton(int buttonNumber)
    {
        var button = _buttons[buttonNumber];
        var buttonText = _buttonsText[buttonNumber];

        var backgroundColor = buttonNumber == _selectedButtonIndex
            ? Color.Orange
            : Color.Gray;
        var textColor = Color.Black;
        
        _spriteBatch.Draw(_whitePixel, button, backgroundColor);
        
        var textSize = _font.MeasureString(buttonText);
        var scale = GetButtonTextScale(button, textSize);
        
        var scaledTextSize = textSize * scale;
        var textPosition = new Vector2(
            button.X + (button.Width - scaledTextSize.X) / 2.0f,
            button.Y + (button.Height - scaledTextSize.Y) / 2.0f);
        
        _spriteBatch.DrawString(
            _font,
            buttonText,
            textPosition,
            textColor,
            0f,
            Vector2.Zero,
            scale,
            SpriteEffects.None,
            0f);
    }

    private void ExecuteCommandButton(MainMenuButtons mainMenuButton)
    {
        switch (mainMenuButton)
        {
            case MainMenuButtons.Start:
                StartFirstLevel();
                break;

            case MainMenuButtons.LevelMenu:
                BuildLevelMenuScene();
                break;

            case MainMenuButtons.Exit:
                _exitAction.Invoke();
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(mainMenuButton), mainMenuButton, "Unknown button.");
        }
    }

    private void StartFirstLevel()
    {
        var gameController = new SokobanGameController(_gameSessionFactory);
        var levelRenderer = new MonoGameLevelView(
            _sceneManager.GraphicsDevice,
            _sceneManager.ContentManager,
            gameController);
        var monogameController = new MonoGameController(gameController);

        gameController.Start();

        levelRenderer.LoadContent();
        _sceneManager.CurrentScene = new LevelScene(
            levelRenderer,
            gameController,
            _gameSessionFactory,
            monogameController,
            _sceneManager,
            0);
    }

    private void BuildLevelMenuScene()
    {
        var levelMenuScene = new LevelMenuScene(_sceneManager, _gameSessionFactory);
        levelMenuScene.LoadContent();

        _sceneManager.CurrentScene = levelMenuScene;
    }

    private static float GetButtonTextScale(Rectangle button, Vector2 textSize)
    {
        var scaleX = (button.Width * ButtonsTextPaddingPart) / textSize.X;
        var scaleY = (button.Height * ButtonsTextPaddingPart) / textSize.Y;

        return Math.Min(scaleX, scaleY);
    }

    private static bool IsKeyPressedWithoutHolding(
        KeyboardState currentState,
        KeyboardState previousState,
        Keys key) => currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
}

public enum MainMenuButtons
{
    Start,
    LevelMenu,
    Exit
}