namespace MainModel.GameEntities;

public class Box
{
    public Point Coordinates { get; set; }
    
    public Box(Point coordinates)
    {
        Coordinates = coordinates;
    }
}