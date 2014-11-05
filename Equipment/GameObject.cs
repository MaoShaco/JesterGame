using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class GameObject : IGameLoopObject
{
    #region Fields
    public GameObject Previous { get; set; }

    public Vector2 Position { get; set; }
    public Vector2 GlobalPosition
    {
        get
        {
            if (Previous != null)
                return Previous.GlobalPosition + this.Position;
            else
                return this.Position;
        }
    }
    public Vector2 Speed { get; set; }

    public int Layer { get; set; }
    public string ID { get; set; }

    #endregion

    #region Constructor
    public GameObject(int Layer = 0, string ID = "")
    {
        this.Layer = Layer;
        this.ID = ID;
        this.Position = Vector2.Zero;
        this.Speed = Vector2.Zero;
    }
    #endregion

    #region Prototypes
    public virtual void HandleInput(InputHelper inputHelper)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public virtual void Reset()
    {
    }
    #endregion
}