using MainModel.MovementStrategies.MovementStrategyFactories;
using MainModel.SokobanLevels.LevelParsers;
using MainModel.SokobanLevels.LevelsSource;

namespace MainModel.Game.GameSessionFactories;

public class TxtGameSessionFactory : IGameSessionFactory
{
    private static TxtGameSessionFactory? _instance;

    private readonly LevelSourceTxtFile _levelSource;
    private readonly LevelParserFromTxt _levelParser;
    private readonly LevelMetadataParser _levelMetadataParser;
    private readonly MovementStrategyFactory _movementStrategyStrategyFactory;

    private TxtGameSessionFactory(string directoryName)
    {
        _levelSource = new LevelSourceTxtFile(directoryName);
        _levelParser = new LevelParserFromTxt();
        _levelMetadataParser = new LevelMetadataParser();
        _movementStrategyStrategyFactory = new MovementStrategyFactory();
    }

    public GameSession BuildLevel(int levelIndex)
    {
        var levelData = _levelSource.GetNewLevel(levelIndex);
        var level = _levelParser.Parse(levelData);
        var levelMetadata = _levelMetadataParser.ParseLevelMetadata(levelData);
        var movementStrategy = _movementStrategyStrategyFactory.CreateMovementStrategy(levelMetadata);
        
        return new(level, movementStrategy);
    }

    public static TxtGameSessionFactory GetInstance(string directoryName)
    {
        _instance ??= new(directoryName);
        return _instance;
    }
}