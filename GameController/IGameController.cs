using MainModel.Game;
using MainModel.MovementStrategies;

namespace GameController;

public interface IGameController
{
    GameSession? CurrentSession { get; }
    
    void Start();
    void LoadLevel(int levelIndex);
    void OnKeyPressed(Key key);
}

public enum Key
{
    Left,
    Right,
    Up,
    Down,
}