using MainModel.Game.GameSessionFactories;
using MainModel.MovementStrategies;

namespace MainModel.Game;

public class SokobanGame : IGame
{
    private readonly IGameSessionFactory _sessionFactory;

    public GameSession? CurrentSession { get; private set; }

    public SokobanGame(IGameSessionFactory sessionFactory)
    {
        _sessionFactory = sessionFactory;
    }

    public void Start() => LoadLevel(0);

    public MovementResult TryMoveIn(Direction direction)
    {
        if (CurrentSession == null)
            return MovementResult.Blocked;

        var movementResult = CurrentSession.TryMoveIn(direction);
        
        if ((movementResult & MovementResult.Moved) != 0)
            CurrentSession.StorekeeperMovements++;
        if ((movementResult & MovementResult.PushedBox) != 0 
            || (movementResult & MovementResult.PushedBoxOnTarget) != 0)
            CurrentSession.BoxesMovements++;

        return movementResult;
    }

    public void LoadLevel(int levelIndex) =>
        CurrentSession = _sessionFactory.BuildLevel(levelIndex);

    // ??
    public void RestartLevel()
    {
        throw new NotImplementedException();
    }
}