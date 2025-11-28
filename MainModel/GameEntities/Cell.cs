namespace MainModel.GameEntities;

public class Cell
{
    public bool IsWall => CellType is CellTypeEnum.Wall;
    public CellTypeEnum CellType { get; }

    public Cell(CellTypeEnum cellType)
    {
        CellType = cellType;
    }
}

public enum CellTypeEnum
{
    Wall,
    BoxTargetPlace,
    Emptiness,
    OutField
}