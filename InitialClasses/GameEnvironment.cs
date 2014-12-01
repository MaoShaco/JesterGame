using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameEnvironment : Game
{
    #region Fields

    public GraphicsDeviceManager Graphics { get; private set; }
    public SpriteBatch SpriteBatch { get; private set; }
    public InputHelper InputHelper { get; private set; }
    public Matrix SpriteScale { get; private set; }

    public static Point Screen { get; set; }
    public static GameStateManager GameStateManager { get; private set; }
    public static LoadActiveManager LoadManager { get; private set; }

    public static Random Random
    {
        get { return new Random(); }
    }

    #endregion

    #region Constructor
    public GameEnvironment()
    {
        Graphics = new GraphicsDeviceManager(this);

        InputHelper = new InputHelper();
        GameStateManager = new GameStateManager();
        SpriteScale = Matrix.CreateScale(1, 1, 1);
        LoadManager = new LoadActiveManager(Content);
    }
    #endregion

    #region Methods
    public void SetFullScreen(bool fullscreen = true)
    {
        float offsetScaleX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / (float)Screen.X;
        float offsetScaleY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / (float)Screen.Y;
        float finalOffsetScale;

        if (!fullscreen)
        {
            finalOffsetScale = Math.Min(offsetScaleX, offsetScaleY);
        }
        else
        {
            finalOffsetScale = offsetScaleX;
        }

        Graphics.PreferredBackBufferWidth = (int)(finalOffsetScale * Screen.X);
        Graphics.PreferredBackBufferHeight = (int)(finalOffsetScale * Screen.Y);
        Graphics.IsFullScreen = fullscreen;
        Graphics.ApplyChanges();

        InputHelper.offsetScale = new Vector2
            (
                (float)GraphicsDevice.Viewport.Width / Screen.X,
                (float)GraphicsDevice.Viewport.Height / Screen.Y
            );

        SpriteScale = Matrix.CreateScale(InputHelper.offsetScale.X, InputHelper.offsetScale.Y, 1);
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected void HandleInput()
    {
        InputHelper.Update();

        if (InputHelper.KeyPressed(Keys.Escape))
            this.Exit();
        if (InputHelper.KeyPressed(Keys.F5))
            SetFullScreen(!Graphics.IsFullScreen);

        GameStateManager.HandleInput(InputHelper);
    }

    protected override void Update(GameTime gameTime)
    {
        HandleInput();
        GameStateManager.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, SpriteScale);
        GameStateManager.Draw(gameTime, SpriteBatch);
        SpriteBatch.End();
    }
    #endregion
}