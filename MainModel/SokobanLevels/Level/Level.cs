using MainModel.GameEntities;

namespace MainModel.SokobanLevels.Level;

public class Level : ILevel
{
    public Cell[,] GameField { get; }
    public Storekeeper Storekeeper { get; set; }
    public HashSet<Box> Boxes { get; }

    public bool IsFinish => 
        Boxes.All(box => GameField[box.Coordinates.X, box.Coordinates.Y].CellType == CellTypeEnum.BoxTargetPlace);

    public int Width { get; }
    public int Height { get; }

    public Level(Cell[,] gameField, Storekeeper storekeeper, HashSet<Box> boxes)
    {
        GameField = gameField;
        Storekeeper = storekeeper;
        Boxes = boxes;
        
        Width = GameField.GetLength(1);
        Height = GameField.GetLength(0);
    }
}