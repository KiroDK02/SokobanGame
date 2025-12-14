namespace MainModel.Game.GameSessionFactories;

public interface IGameSessionFactory
{
    int LevelsCount { get; }
    HashSet<int> CompletedLevels { get; }
    
    GameSession BuildLevel(int levelIndex);
}