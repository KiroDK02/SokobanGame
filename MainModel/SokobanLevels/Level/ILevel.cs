using MainModel.GameEntities;

namespace MainModel.SokobanLevels.Level;

public interface ILevel
{
    bool IsFinish { get; }
    Cell[,] GameField { get; }
    Storekeeper Storekeeper { get; set; }
    HashSet<Box> Boxes { get; }
}