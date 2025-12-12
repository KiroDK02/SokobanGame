using Microsoft.Xna.Framework;

namespace GameView.Scenes;

public class SceneManager
{
    public IGameScene CurrentScene { get; set; }
    
    public void Update(GameTime gameTime) => CurrentScene.Update(gameTime);
    public void Draw(GameTime gameTime) => CurrentScene.Draw(gameTime);
}