using System.Text;

namespace MainModel.SokobanLevels.LevelsSource;

public class LevelSourceTxtFile : ILevelSource
{
    private readonly string[] _levelsFiles;
    private int _currentFile = 0;

    public bool HasMoreLevels => _currentFile < _levelsFiles.Length;
    
    public LevelSourceTxtFile(string directoryPath)
    {
        _levelsFiles = Directory.GetFiles(directoryPath, "*.txt");
    }

    public LevelData GetNewLevel()
    {
        using var streamReader = new StreamReader(_levelsFiles[_currentFile]);
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
        
        return new(_currentFile++, [.. levelMap], metadata);
    }
}