using MainModel.Game;
using MainModel.Game.GameSessionFactories;
using MainModel.MovementStrategies;

namespace GameController;

public class SokobanGameController : IGameController
{
    public GameSession? CurrentSession => _game.CurrentSession;
    
    private readonly SokobanGame _game;

    public SokobanGameController(IGameSessionFactory sessionFactory)
    {
        _game = new(sessionFactory);
    }

    public void Start() => _game.Start();

    public void LoadLevel(int levelIndex) => _game.LoadLevel(levelIndex);

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

        // Идейно можно в этом контроллере создать какой-то ивент
        // и подписать его на метод из вьюхи,
        // который будет реагировать на результат попытки перемещения
        // какими-нибудь звуками и тд?
        var movementResult = _game.TryMoveIn(direction);
    }
}