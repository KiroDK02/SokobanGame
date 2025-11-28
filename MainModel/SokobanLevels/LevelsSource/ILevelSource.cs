namespace MainModel.SokobanLevels.LevelsSource;

public interface ILevelSource
{
    LevelData GetNextLevel();
}