using MainModel.SokobanLevels;

namespace MainModel.MovementStrategies.MovementStrategyFactories;

// Через фабрику можно будет создавать разные стратегии движения,
// например, с доп ускорением в 2 раза, возможно интерфейс
// не нужен и можно просто один класс реализовать, который через
// свитч будет создавть разные стратегии. Как лучше не знаю.
public interface IMovementStrategyFactory
{
    IMovementStrategy CreateMovementStrategy(LevelMetadata metadata);
}