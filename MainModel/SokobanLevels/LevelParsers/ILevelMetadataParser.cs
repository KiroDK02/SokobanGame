namespace MainModel.SokobanLevels.LevelParsers;

public interface ILevelMetadataParser
{
    LevelMetadata ParseLevelMetadata(LevelData levelData);
}