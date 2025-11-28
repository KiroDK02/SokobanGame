namespace MainModel.SokobanLevels;

public class LevelData
{
    public int Id { get; }
    public string[] LevelMap { get; }
    public Dictionary<string, string> Metadata { get; }

    public LevelData(int id, string[] levelMap, Dictionary<string, string> metadata)
    {
        Id = id;
        LevelMap = levelMap;
        Metadata = metadata;
    }
}