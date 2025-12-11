using MainModel.SokobanLevels;

namespace MainModel.MovementStrategies.MovementStrategyFactories;

public class MovementStrategyFactory : IMovementStrategyFactory
{
    public IMovementStrategy CreateMovementStrategy(LevelMetadata metadata) =>
        new DefaultMovementStrategy();
}