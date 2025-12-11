namespace MainModel.SokobanLevels.LevelsSource;

public interface ILevelSource
{
    LevelData GetNewLevel(int levelIndex);
}