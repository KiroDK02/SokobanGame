namespace MainModel.GameEntities;

public class Box
{
    public Point Coordinates { get; set; }
    
    public Box(Point coordinates)
    {
        Coordinates = coordinates;
    }
    
    public void MoveTo(Point newCoordinates) => Coordinates = newCoordinates;

    public bool IsBoxOnTarget(Cell[,] gameField) =>
        gameField[Coordinates.Y, Coordinates.X].IsBoxTargetPlace;
}