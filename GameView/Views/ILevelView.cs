using GameController;

namespace GameView.Views;

public interface ILevelView
{
    IGameController GameController { get; }
    
    void Render();
    void LoadContent();
}