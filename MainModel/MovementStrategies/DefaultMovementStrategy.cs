using MainModel.GameEntities;
using MainModel.SokobanLevels.Level;

namespace MainModel.MovementStrategies;

public class DefaultMovementStrategy : IMovementStrategy
{
    public MovementResult TryMoveIn(ILevel level, Direction direction)
    {
        var (dx, dy) = GetShifts(direction);
        var skNewCoordinates = new Point(level.Storekeeper.Coordinates.X + dx,
            level.Storekeeper.Coordinates.Y + dy);

        if (level.GameField[skNewCoordinates.Y, skNewCoordinates.X].IsWall)
            return MovementResult.Blocked;

        var box = level.Boxes.FirstOrDefault(box => box.Coordinates == skNewCoordinates);
        if (box == null)
        {
            level.Storekeeper.MoveTo(skNewCoordinates);
            return MovementResult.Moved;
        }

        var boxNewCoordinates = new Point(skNewCoordinates.X + dx, skNewCoordinates.Y + dy);

        if (!IsPossibleMoveBoxTo(level, boxNewCoordinates))
            return MovementResult.Blocked;

        level.Storekeeper.MoveTo(skNewCoordinates);
        box.MoveTo(boxNewCoordinates);

        return level.GameField[boxNewCoordinates.Y, boxNewCoordinates.X].IsBoxTargetPlace
            ? MovementResult.PushedBoxOnTarget
            : MovementResult.PushedBox;
    }

    private static bool IsPossibleMoveBoxTo(ILevel level, Point target)
    {
        if (level.GameField[target.Y, target.X].IsWall)
            return false;

        return level.Boxes.SingleOrDefault(box => box.Coordinates == target) == null;
    }

    private static (int dx, int dy) GetShifts(Direction direction) =>
        direction switch
        {
            Direction.Left => (-1, 0),
            Direction.Right => (1, 0),
            Direction.Up => (0, -1),
            Direction.Down => (0, 1),
            _ => throw new ArgumentException("Invalid direction", nameof(direction))
        };
}