using MainModel.Game;

namespace GameController;

public interface IGameController
{
    GameSession? CurrentSession { get; }
    
    void Start();
    void OnKeyPressed(Key key);
}

public enum Key
{
    Left,
    Right,
    Up,
    Down,
}