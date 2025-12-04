using GameController;
using Microsoft.Xna.Framework.Input;

namespace GameView;

public class MonoGameController
{
    private readonly IGameController _gameController;
    private KeyboardState _previousKeyboardState;
    
    public MonoGameController(IGameController gameController)
    {
        _gameController = gameController;
    }

    public void Update()
    {
        var currentKeyboardState = Keyboard.GetState();
        
        CheckKeyOnPressedWithoutHolding(currentKeyboardState, _previousKeyboardState, Keys.Up, Key.Up);
        CheckKeyOnPressedWithoutHolding(currentKeyboardState, _previousKeyboardState, Keys.Down, Key.Down);
        CheckKeyOnPressedWithoutHolding(currentKeyboardState, _previousKeyboardState, Keys.Left, Key.Left);
        CheckKeyOnPressedWithoutHolding(currentKeyboardState, _previousKeyboardState, Keys.Right, Key.Right);
        
        _previousKeyboardState = currentKeyboardState;
    }

    private void CheckKeyOnPressedWithoutHolding(
        KeyboardState currentState,
        KeyboardState previousState, 
        Keys monoGameKey,
        Key myKey)
    {
        if (currentState.IsKeyDown(monoGameKey) && previousState.IsKeyUp(monoGameKey))
            _gameController.OnKeyPressed(myKey);
    }
}