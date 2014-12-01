using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputHelper
{
    #region Fields
    private MouseState currentMouseState;
    private MouseState previousMouseState;

    private KeyboardState currentKeyboardState;
    private KeyboardState previousKeyboardState;

    public Vector2 offsetScale { get; set; }
    #endregion

    #region Constructor
    public InputHelper()
    {
        offsetScale = Vector2.One;
    }
    #endregion

    #region initialMethods
    public virtual void Update()
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
    }

    public virtual Vector2 MousePosition
    {
        get { return new Vector2(currentMouseState.X, currentMouseState.Y) / offsetScale; }
    }

    public virtual bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
    }

    public virtual bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }

    public bool IsKeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }
    #endregion
}