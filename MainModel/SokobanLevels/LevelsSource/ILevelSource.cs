namespace MainModel.SokobanLevels.LevelsSource;

public interface ILevelSource
{
    int LevelsCount { get; }
    
    LevelData GetNewLevel(int levelIndex);
}