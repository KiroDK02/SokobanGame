using GameView.Scenes;
using MainModel.Game.GameSessionFactories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameView.Views;

public class SokobanGameView : Game
{
    private readonly GraphicsDeviceManager _graphics;

    private SceneManager _sceneManager;
    
    public SokobanGameView()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        Window.Title = "Sokoban";
        Window.AllowUserResizing = true;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        _graphics.PreferredBackBufferHeight = 800;
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.ApplyChanges();

        _sceneManager = new SceneManager(GraphicsDevice, Content);
        
        var levelMenuScene = new LevelMenuScene(
            _sceneManager,
            TxtGameSessionFactory.GetInstance("Content/Levels"));
        levelMenuScene.LoadContent();
        
        _sceneManager.CurrentScene = levelMenuScene;
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _sceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _sceneManager.Draw(gameTime);
    }
}