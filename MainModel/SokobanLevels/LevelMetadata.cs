namespace MainModel.SokobanLevels;

// По идеи можно сюда добавлять что-нибубь,
// какие-нибудь новые фичи (массу ящиков например),
public class LevelMetadata
{
    public string Name { get; }
    public int PlayerSpeed { get; }

    public LevelMetadata(string name, int playerSpeed)
    {
        Name = name;
        PlayerSpeed = playerSpeed;
    }
}