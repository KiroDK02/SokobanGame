using MainModel.GameEntities;

namespace MainModel.SokobanLevels.Level;

public interface ILevel
{
    bool IsFinish { get; }
    int Width { get; }
    int Height { get; }
    Cell[,] GameField { get; }
    Storekeeper Storekeeper { get; }
    HashSet<Box> Boxes { get; }
}