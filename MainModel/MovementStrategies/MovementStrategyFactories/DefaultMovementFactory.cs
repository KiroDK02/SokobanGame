using MainModel.SokobanLevels;

namespace MainModel.MovementStrategies.MovementStrategyFactories;

public class DefaultMovementFactory : IMovementStrategyFactory
{
    public IMovementStrategy CreateMovementStrategy(LevelMetadata metadata) =>
        new DefaultMovementStrategy();
}