using MainModel.MovementStrategies;
using MainModel.SokobanLevels.Level;

namespace MainModel.Game;

public class GameSession
{
    public bool IsFinish => Level.IsFinish;
    public ILevel Level { get; }
    public IMovementStrategy MovementStrategy { get; }
    public LevelStatistics LevelStatistics { get; }
    
    public GameSession(int levelIndex, ILevel level, IMovementStrategy movementStrategy)
    {
        Level = level;
        MovementStrategy = movementStrategy;
        LevelStatistics = new(levelIndex);
    }

    public MovementResult TryMoveIn(Direction direction) => MovementStrategy.TryMoveIn(Level, direction);
}