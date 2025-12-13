namespace MainModel.SokobanLevels.Level;

public class LevelStatistics
{
    public int CurrentLevelIndex { get; }
    public double ElapsedTime { get; set; }
    public int StorekeeperMovements { get; set; }
    public int BoxesMovements { get; set; }

    public LevelStatistics(int currentLevelIndex)
    {
        CurrentLevelIndex = currentLevelIndex;
    }
}