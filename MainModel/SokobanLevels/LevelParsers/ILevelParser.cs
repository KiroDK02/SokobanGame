using MainModel.SokobanLevels.Level;

namespace MainModel.SokobanLevels.LevelParsers;

public interface ILevelParser
{
    Level.Level Parse(LevelData levelData);
}