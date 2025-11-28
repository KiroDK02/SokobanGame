using MainModel.GameEntities;

namespace MainModel.SokobanLevels.Level;

public interface ILevel
{
    bool IsFinish { get; }
    Storekeeper Storekeeper { get; set; }
    HashSet<Box> Boxes { get; }
}