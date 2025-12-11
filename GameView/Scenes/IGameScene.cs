using Microsoft.Xna.Framework;

namespace GameView.Scenes;

public interface IGameScene
{
    void Update(GameTime gameTime);
    void Draw(GameTime gameTime);
}