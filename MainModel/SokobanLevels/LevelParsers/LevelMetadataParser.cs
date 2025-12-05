namespace MainModel.SokobanLevels.LevelParsers;

// для своих метаданных (своих приколов у уровней) свой парсер
public class LevelMetadataParser : ILevelMetadataParser
{
    public LevelMetadata ParseLevelMetadata(LevelData levelData)
    {
        var metadata = levelData.Metadata;
        var playerSpeed = 0;
        
        if (metadata.TryGetValue("playerSpeed", out var speed))
            if (!int.TryParse(speed, out playerSpeed))
                throw new ArgumentException("Invalid value of player's speed.");

        return new(metadata["name"], playerSpeed);
    }
}