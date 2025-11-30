namespace MainModel.SokobanLevels.LevelsSource;

public interface ILevelSource
{
    bool HasMoreLevels { get; }
    
    LevelData GetNewLevel();
}