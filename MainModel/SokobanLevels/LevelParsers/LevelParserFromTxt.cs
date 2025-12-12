using MainModel.GameEntities;

namespace MainModel.SokobanLevels.LevelParsers;

public class LevelParserFromTxt : ILevelParser
{
    public Level.Level Parse(LevelData levelData)
    {
        var height = levelData.LevelMap.Length;
        var width = levelData.LevelMap[0].Length;

        var field = new Cell[height, width];
        var boxes = new HashSet<Box>();
        Storekeeper? storekeeper = null;
        
        for (int i = 0; i < height; i++)
        for (int j = 0; j < width; j++)
        {
            var symbol = levelData.LevelMap[i][j];
            field[i, j] = ParseSymbolToCell(symbol);
            if (symbol == 'S')
                storekeeper = new Storekeeper(new(j, i));
            else if (symbol == 'B')
                boxes.Add(new(new(j, i)));
        }
        
        if (storekeeper == null)
            throw new ArgumentException("Storekeeper doesn't exist in field.");
        
        return new(field, storekeeper, boxes);
    }

    private static Cell ParseSymbolToCell(char symbol)
    {
        return symbol switch
        {
            '#' => new(CellType.Wall),
            'T' => new(CellType.BoxTargetPlace),
            'O' => new(CellType.OutField),
            _ => new(CellType.Floor)
        };
    }
}