using MainModel.MovementStrategies;
using MainModel.MovementStrategies.MovementStrategyFactories;
using MainModel.SokobanLevels.LevelParsers;
using MainModel.SokobanLevels.LevelsSource;

namespace MainModel.Game;

public class Game : IGame
{
    private readonly ILevelSource _levelSource;
    private readonly ILevelParser _levelParser;
    private readonly ILevelMetadataParser _levelMetadataParser;
    private readonly IMovementStrategyFactory _movementStrategyFactory;

    public GameSession? CurrentSession { get; private set; }

    public Game(
        ILevelSource levelSource,
        ILevelParser levelParser,
        ILevelMetadataParser levelMetadataParser,
        IMovementStrategyFactory movementStrategyFactory)
    {
        _levelSource = levelSource;
        _levelParser = levelParser;
        _levelMetadataParser = levelMetadataParser;
        _movementStrategyFactory = movementStrategyFactory;
    }

    public void Start()
    {
        LoadNewLevel();
    }

    public MovementResult TryMoveIn(Direction direction)
    {
        if (CurrentSession == null)
            return MovementResult.Blocked;

        var movementResult = CurrentSession.TryMoveIn(direction);

        return movementResult;
    }

    public void LoadNewLevel()
    {
        if (!_levelSource.HasMoreLevels)
            throw new InvalidOperationException("No more levels available.");

        var levelData = _levelSource.GetNewLevel();
        var level = _levelParser.Parse(levelData);
        var levelMetadata = _levelMetadataParser.ParseLevelMetadata(levelData);
        var movementStrategy = _movementStrategyFactory.CreateMovementStrategy(levelMetadata);

        CurrentSession = new GameSession(level, movementStrategy);
    }

    // ??
    public void RestartLevel()
    {
        throw new NotImplementedException();
    }
}