using MainModel.MovementStrategies;
using MainModel.SokobanLevels.Level;

namespace MainModel.Game;

public class GameSession
{
    public bool IsFinish => Level.IsFinish;
    public int CurrentLevelIndex { get; }
    public ILevel Level { get; }
    public IMovementStrategy MovementStrategy { get; }

    public double ElapsedTime { get; set; } = 0.0;
    public int StorekeeperMovements { get; set; } = 0;
    public int BoxesMovements { get; set; } = 0;
    
    public GameSession(int currentLevelIndex, ILevel level, IMovementStrategy movementStrategy)
    {
        CurrentLevelIndex = currentLevelIndex;
        Level = level;
        MovementStrategy = movementStrategy;
    }

    public MovementResult TryMoveIn(Direction direction) => MovementStrategy.TryMoveIn(Level, direction);
}