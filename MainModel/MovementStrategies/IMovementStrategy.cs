using MainModel.SokobanLevels.Level;

namespace MainModel.MovementStrategies;

public interface IMovementStrategy
{
    void Move(Level level);
}