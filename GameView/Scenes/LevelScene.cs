using GameController;
using GameView.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameView.Scenes;

public class LevelScene : IGameScene
{
    private readonly ILevelView _levelRenderer;
    private readonly IGameController _gameController;
    private readonly MonoGameController _monoGameController;
    private readonly SceneManager _sceneManager;

    private readonly int _levelIndex;

    public LevelScene(
        ILevelView levelRenderer,
        IGameController gameController,
        MonoGameController monoGameController,
        SceneManager sceneManager,
        int levelIndex)
    {
        _levelRenderer = levelRenderer;
        _gameController = gameController;
        _monoGameController = monoGameController;
        _sceneManager = sceneManager;
        _levelIndex = levelIndex;
    }

    public void Update(GameTime gameTime)
    {
        var session = _gameController.CurrentSession;
        if (session is null)
            return;

        session.ElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        _monoGameController.Update();
        
        if (session.IsFinish)
        {
            // TODO: Добавить сцену завершения уровня
        }
    }

    public void Draw(GameTime gameTime)
    {
        _levelRenderer.Render();
    }
}