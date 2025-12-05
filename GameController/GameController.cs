using MainModel.Game;
using MainModel.MovementStrategies;

namespace GameController;

public class GameController : IGameController
{
    private readonly IGame _game;

    public GameSession? CurrentSession => _game.CurrentSession;

    public GameController(IGame game)
    {
        _game = game;
    }

    public void Start()
    {
        _game.Start();
    }

    public void OnKeyPressed(Key key)
    {
        var direction = key switch
        {
            Key.Up => Direction.Up,
            Key.Down => Direction.Down,
            Key.Left => Direction.Left,
            Key.Right => Direction.Right,
            _ => throw new InvalidOperationException("Unknown key.")
        };

        _game.TryMoveIn(direction);
    }
}