using System.Text;

namespace MainModel.SokobanLevels.LevelsSource;

public class LevelSourceTxtFile : ILevelSource
{
    public int LevelsCount => _levelsData.Count;
    
    private readonly Dictionary<int, LevelData> _levelsData = [];
    
    public LevelSourceTxtFile(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            throw new DirectoryNotFoundException($"Directory {directoryPath} was not found.");
        
        var levelsFiles = Directory.GetFiles(directoryPath, "*.txt");
        ReadAllLevelsData(levelsFiles);
    }

    public LevelData GetNewLevel(int levelId) => _levelsData[levelId + 1];

    private void ReadAllLevelsData(string[] levelsFiles)
    {
        foreach (var levelFileName in levelsFiles)
        {
            using var streamReader = new StreamReader(levelFileName);
            var levelId = int.Parse(streamReader.ReadLine()!);
            
            var levelMap = new List<string>();
            var input = "";

            while ("[metadata]" != (input = streamReader.ReadLine()))
            {
                levelMap.Add(input!);
            }

            var metadata = streamReader
                .ReadToEnd()
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(':', StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(data => data[0], data => data[1]);
            
            _levelsData[levelId] = new LevelData(levelId, [.. levelMap], metadata);
        }
    }
}