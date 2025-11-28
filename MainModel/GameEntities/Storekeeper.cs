namespace MainModel.GameEntities;

public class Storekeeper
{
    public Point Coordinates { get; set; }
    
    public Storekeeper(Point coordinates)
    {
        Coordinates = coordinates;
    }
}