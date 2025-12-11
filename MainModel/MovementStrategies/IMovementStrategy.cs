using MainModel.SokobanLevels.Level;

namespace MainModel.MovementStrategies;

public interface IMovementStrategy
{
    MovementResult TryMoveIn(ILevel level, Direction direction);
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

[Flags]
public enum MovementResult
{
    None = 0,
    Moved = 1, 
    Blocked = 2,
    PushedBox = 4,
    PushedBoxOnTarget = 8 // ?
}