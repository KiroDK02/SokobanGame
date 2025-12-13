using System;
using GameController;
using MainModel.GameEntities;
using MainModel.SokobanLevels.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameView.Views;

public class MonoGameLevelView : ILevelView
{
    private const int MinCellSize = 16;
    private const int MaxCellSize = 256;
    private const float PaddingPart = 0.05f;

    public IGameController GameController { get; }

    private int _cellSize;
    private int _mapWidth;
    private int _mapHeight;
    private int _offsetX;
    private int _offsetY;

    private readonly GraphicsDevice _graphicsDevice;
    private readonly ContentManager _contentManager;
    private SpriteBatch _spriteBatch;

    private Texture2D _playerTexture;
    private Texture2D _wallTexture;
    private Texture2D _floorTexture;
    private Texture2D _boxTexture;
    private Texture2D _boxOnTargetTexture;
    private Texture2D _targetCellTexture;
    private Texture2D _blackBlockTexture;
    
    public MonoGameLevelView(GraphicsDevice graphicsDevice, ContentManager contentManager,
        IGameController gameController)
    {
        GameController = gameController;

        _graphicsDevice = graphicsDevice;
        _contentManager = contentManager;
    }

    public void LoadContent()
    {
        _playerTexture = _contentManager.Load<Texture2D>("Textures/Player");
        _wallTexture = _contentManager.Load<Texture2D>("Textures/Wall");
        _floorTexture = _contentManager.Load<Texture2D>("Textures/Floor");
        _boxTexture = _contentManager.Load<Texture2D>("Textures/Box");
        _boxOnTargetTexture = _contentManager.Load<Texture2D>("Textures/BoxOnTarget");
        _targetCellTexture = _contentManager.Load<Texture2D>("Textures/TargetCell");
        
        _blackBlockTexture = new Texture2D(_graphicsDevice, 1, 1);
        _blackBlockTexture.SetData([Color.Black]);
        
        _spriteBatch = new SpriteBatch(_graphicsDevice);
    }

    public void Render()
    {
        var level = GameController.CurrentSession?.Level;
        if (level == null)
            return;
        
        PrepareToDraw();

        _graphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin();

        for (int y = 0; y < level.Height; y++)
        for (int x = 0; x < level.Width; x++)
            DrawStaticObjects(level, x, y);

        DrawPlayer(level.Storekeeper);
        DrawBoxes(level);

        _spriteBatch.End();
    }

    private void PrepareToDraw()
    {
        var level = GameController.CurrentSession?.Level;
        if (level == null)
            return;

        var windowWidth = _graphicsDevice.Viewport.Width;
        var windowHeight = _graphicsDevice.Viewport.Height;
        var cellCountInRow = level.Width;
        var cellCountInColumn = level.Height;

        var width = windowWidth * (1.0f - 2.0f * PaddingPart);
        var height = windowHeight * (1.0f - 2.0f * PaddingPart);

        _cellSize = GetCellSize(width, height, cellCountInRow, cellCountInColumn);

        _mapWidth = _cellSize * cellCountInRow;
        _mapHeight = _cellSize * cellCountInColumn;
        _offsetX = (windowWidth - _mapWidth) / 2;
        _offsetY = (windowHeight - _mapHeight) / 2;
    }

    private void DrawStaticObjects(ILevel level, int x, int y)
    {
        var cell = level.GameField[y, x];
        var cellTexture = cell.CellType switch
        {
            CellType.Wall => _wallTexture,
            CellType.BoxTargetPlace => _targetCellTexture,
            CellType.Floor => _floorTexture,
            _ => _blackBlockTexture
        };
        
        var (globalX, globalY) = GetGlobalCoordinates(x, y);

        _spriteBatch.Draw(
            cellTexture,
            new Rectangle(globalX, globalY, _cellSize, _cellSize),
            Color.White);
    }

    private void DrawBoxes(ILevel level)
    {
        foreach (var box in level.Boxes)
        {
            var (globalX, globalY) = GetGlobalCoordinates(box.Coordinates.X, box.Coordinates.Y);
            var texture = box.IsBoxOnTarget(level.GameField)
                ? _boxOnTargetTexture
                : _boxTexture;
            
            _spriteBatch.Draw(
                texture,
                new Rectangle(globalX, globalY, _cellSize, _cellSize),
                Color.White);
        }
    }

    private void DrawPlayer(Storekeeper storekeeper)
    {
        var (globalX, globalY) = GetGlobalCoordinates(storekeeper.Coordinates.X, storekeeper.Coordinates.Y);

        _spriteBatch.Draw(
            _playerTexture,
            new Rectangle(globalX, globalY, _cellSize, _cellSize),
            Color.White);
    }

    private static int GetCellSize(float width, float height, int cellCountInRow, int cellCountInColumn)
    {
        var cellWidth = (int)Math.Floor(width / cellCountInRow);
        var cellHeight = (int)Math.Floor(height / cellCountInColumn);

        return Math.Max(MinCellSize, int.Min(int.Min(cellWidth, cellHeight), MaxCellSize));
    }

    private (int globalX, int globalY) GetGlobalCoordinates(int x, int y) =>
        (_offsetX + x * _cellSize, _offsetY + y * _cellSize);
}