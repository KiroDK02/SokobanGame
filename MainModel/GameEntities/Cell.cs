namespace MainModel.GameEntities;

public class Cell
{
    public bool IsWall => CellType is GameEntities.CellType.Wall;
    public bool IsBoxTargetPlace => CellType is GameEntities.CellType.BoxTargetPlace;
    public CellType CellType { get; }

    public Cell(CellType cellType)
    {
        CellType = cellType;
    }
}

public enum CellType
{
    Wall,
    BoxTargetPlace,
    Floor,
    OutField
}