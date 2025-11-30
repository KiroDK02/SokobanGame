using MainModel.MovementStrategies;

namespace MainModel.Game;

public interface IGame
{
    GameSession? CurrentSession { get; }

    void Start();
    MovementResult TryMoveIn(Direction direction);
    void LoadNewLevel();
    void RestartLevel();
}