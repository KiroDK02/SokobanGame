using GameController;
using GameView.Views;
using MainModel.Game.GameSessionFactories;
using Microsoft.Xna.Framework;

namespace GameView.Scenes;

public class LevelScene : IGameScene
{
    private readonly ILevelView _levelRenderer;
    private readonly IGameController _gameController;
    private readonly IGameSessionFactory _gameSessionFactory;
    private readonly MonoGameController _monoGameController;
    private readonly SceneManager _sceneManager;

    private readonly int _levelIndex;

    public LevelScene(
        ILevelView levelRenderer,
        IGameController gameController,
        IGameSessionFactory gameSessionFactory,
        MonoGameController monoGameController,
        SceneManager sceneManager,
        int levelIndex)
    {
        _levelRenderer = levelRenderer;
        _gameController = gameController;
        _gameSessionFactory = gameSessionFactory;
        _monoGameController = monoGameController;
        _sceneManager = sceneManager;
        _levelIndex = levelIndex;
    }

    public void Update(GameTime gameTime)
    {
        var session = _gameController.CurrentSession;
        if (session is null)
            return;

        session.LevelStatistics.ElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        _monoGameController.Update();
        
        if (session.IsFinish)
        {
            _gameSessionFactory.CompletedLevels.Add(_levelIndex);
            BuildCompletedLevelScene();
        }
    }

    public void Draw(GameTime gameTime)
    {
        _levelRenderer.Render();
    }

    private void BuildCompletedLevelScene()
    {
        var completedLevelScene = new CompletedLevelScene(
            _sceneManager,
            _gameController.CurrentSession?.LevelStatistics,
            _gameSessionFactory);
        completedLevelScene.LoadContent();
        
        _sceneManager.CurrentScene = completedLevelScene;
    }
}