using System.Text;

namespace MainModel.SokobanLevels.LevelsSource;

public class LevelSourceTxtFile : ILevelSource
{
    public int LevelsCount => _levelsFiles.Length;
    
    private readonly string[] _levelsFiles;
    
    public LevelSourceTxtFile(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            throw new DirectoryNotFoundException($"Directory {directoryPath} was not found.");
        
        _levelsFiles = Directory.GetFiles(directoryPath, "*.txt");
    }

    public LevelData GetNewLevel(int levelIndex)
    {
        using var streamReader = new StreamReader(_levelsFiles[levelIndex]);
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
        
        return new(levelIndex, [.. levelMap], metadata);
    }
}