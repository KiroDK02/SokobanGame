namespace MainModel.SokobanLevels.LevelParsers;

// для своих метаданных (своих приколов у уровней) свой парсер
public class LevelMetadataParser : ILevelMetadataParser
{
    public LevelMetadata ParseLevelMetadata(LevelData levelData)
    {
        var metadata = levelData.Metadata;

        if (!int.TryParse(metadata["playerSpeed"], out var playerSpeed))
            throw new ArgumentException("Invalid value of player's speed.");

        return new(metadata["name"], playerSpeed);
    }
}