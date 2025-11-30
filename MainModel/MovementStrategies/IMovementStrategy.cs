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

public enum MovementResult
{
    Moved,
    Blocked,
    PushedBox,
    PushedBoxOnTarget // ?
}