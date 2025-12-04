using MainModel.Game;
using Microsoft.Xna.Framework.Content;

namespace GameView.Views;

public interface ILevelView
{
    void Render(GameSession gameSession);
    void LoadContent(ContentManager contentManager);
}