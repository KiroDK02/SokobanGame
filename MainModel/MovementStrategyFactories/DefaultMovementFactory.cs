using MainModel.MovementStrategies;

namespace MainModel.MovementStrategyFactories;

public class DefaultMovementFactory : IMovementStrategyFactory
{
    public IMovementStrategy CreateMovementStrategy() => new DefaultMovementStrategy();
}