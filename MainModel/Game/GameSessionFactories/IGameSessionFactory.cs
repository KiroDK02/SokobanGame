using MainModel.SokobanLevels.Level;

namespace MainModel.Game.GameSessionFactories;

public interface IGameSessionFactory
{
    GameSession BuildLevel(int levelIndex);
}