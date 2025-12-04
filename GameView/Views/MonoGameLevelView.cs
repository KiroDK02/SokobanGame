using MainModel.Game;
using MainModel.GameEntities;
using MainModel.SokobanLevels.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameView.Views;

public class MonoGameLevelView : ILevelView
{
    private readonly Game _game;
    private SpriteBatch _spriteBatch;
    private const int CellSize = 32;
    
    private Texture2D _playerTexture;
    private Texture2D _wallTexture;
    private Texture2D _floorTexture;
    private Texture2D _boxTexture;
    private Texture2D _targetCellTexture;
    
    public MonoGameLevelView(Game game)
    {
        _game = game;
    }

    public void LoadContent(ContentManager contentManager)
    {
        _playerTexture = contentManager.Load<Texture2D>("Textures/Player");
        _wallTexture = contentManager.Load<Texture2D>("Textures/Wall");
        _floorTexture = contentManager.Load<Texture2D>("Textures/Floor");
        _boxTexture = contentManager.Load<Texture2D>("Textures/Box");
        _targetCellTexture = contentManager.Load<Texture2D>("Textures/TargetCell");
        
        _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
    }
    
    public void Render(GameSession gameSession)
    {
        var level =  gameSession.Level;
        
        _spriteBatch.Begin();
        
        for (int y = 0; y < level.Height; y++)
            for(int x = 0; x < level.Width; x++)
                DrawStaticObjects(level, x, y);
        
        DrawPlayer(level.Storekeeper);
        DrawBoxes(level);
        
        _spriteBatch.End();
    }
    
    private void DrawStaticObjects(ILevel level, int x, int y)
    {
        var cell = level.GameField[x, y];
        var cellTexture = 
            cell.IsWall 
            ? _wallTexture
            : cell.IsBoxTargetPlace
                ? _targetCellTexture
                : _floorTexture;
        
        _spriteBatch.Draw(
            cellTexture, 
            new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), 
            Color.White);
    }

    private void DrawBoxes(ILevel level)
    {
        foreach (var box in level.Boxes)
        {
            _spriteBatch.Draw(
                _boxTexture,
                new Rectangle(
                    box.Coordinates.X * CellSize, 
                    box.Coordinates.Y * CellSize, 
                    CellSize, CellSize),
                Color.White);
        }
    }
    
    private void DrawPlayer(Storekeeper storekeeper) =>
        _spriteBatch.Draw(
            _playerTexture,
            new Rectangle(
                storekeeper.Coordinates.X * CellSize,
                storekeeper.Coordinates.Y * CellSize, 
                CellSize, CellSize),
            Color.White);
}