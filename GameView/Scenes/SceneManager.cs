using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameView.Scenes;

public class SceneManager
{
    public GraphicsDevice GraphicsDevice { get; }
    public ContentManager ContentManager { get; }
    public IGameScene CurrentScene { get; set; }
    
    public SceneManager(GraphicsDevice graphicsDevice,  ContentManager contentManager)
    {
        GraphicsDevice = graphicsDevice;
        ContentManager = contentManager;
    }
    
    public void Update(GameTime gameTime) => CurrentScene.Update(gameTime);
    public void Draw(GameTime gameTime) => CurrentScene.Draw(gameTime);
}