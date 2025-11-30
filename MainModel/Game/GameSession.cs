using MainModel.MovementStrategies;
using MainModel.SokobanLevels.Level;

namespace MainModel.Game;

public class GameSession
{
    public bool IsFinish => Level.IsFinish;
    public ILevel Level { get; }
    public IMovementStrategy MovementStrategy { get; }
    
    public GameSession(ILevel level, IMovementStrategy movementStrategy)
    {
        Level = level;
        MovementStrategy = movementStrategy;
    }

    public MovementResult TryMoveIn(Direction direction) => MovementStrategy.TryMoveIn(Level, direction);
}