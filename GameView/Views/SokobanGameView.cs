using GameController;
using MainModel.Game;
using MainModel.MovementStrategies.MovementStrategyFactories;
using MainModel.SokobanLevels.LevelParsers;
using MainModel.SokobanLevels.LevelsSource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameView.Views;

public class SokobanGameView : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private MonoGameLevelView _monoGameLevelView;
    private IGameController _gameController;
    private MonoGameController _monoGameController;
    
    public SokobanGameView()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        Window.Title = "Sokoban";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        var levelSource = new LevelSourceTxtFile("Content/Levels");
        var levelParser = new LevelParserFromTxt();
        var levelMetadataParser = new LevelMetadataParser();
        var movementStrategyFactory = new DefaultMovementFactory();
        
        var game = new SokobanGame(levelSource, levelParser, levelMetadataParser, movementStrategyFactory);
        _gameController = new GameController.GameController(game);
        _gameController.Start();
        
        _monoGameController = new MonoGameController(_gameController);
        _monoGameLevelView = new MonoGameLevelView(this);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _monoGameLevelView.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One)
                .Buttons.Back
            == ButtonState.Pressed
            || Keyboard.GetState()
                .IsKeyDown(Keys.Escape))
            Exit();

        _monoGameController.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _monoGameLevelView.Render(_gameController.CurrentSession);

        base.Draw(gameTime);
    }
}