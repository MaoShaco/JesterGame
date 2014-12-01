using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class GameObject : IGameLoopObject
{
    #region Fields

    public GameObject Parent { get; set; }

    public int Layer { get; private set; }
    public string ID { get; private set; }
    public bool Visible { get; set; }

    protected Vector2 position;

    public virtual Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
    protected Vector2 velocity;

    public virtual Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public GameObject Root
    {
        get
        {
            if (Parent != null)
                return Parent.Root;
            else
                return this;
        }
    }

    public GameObjectList GameWorld
    {
        get
        {
            return Root as GameObjectList;
        }
    }

    public virtual Vector2 GlobalPosition
    {
        get
        {
            if (Parent != null)
                return Parent.GlobalPosition + this.Position;
            else
                return this.Position;
        }
    }
    #endregion

    #region Concsturctor
    public GameObject(int Layer = 0, string ID = "")
    {
        this.Layer = Layer;
        this.ID = ID;
        this.position = Vector2.Zero;
        this.velocity = Vector2.Zero; 
        this.Visible = true;
    }
    #endregion

    #region Methods
    public virtual void HandleInput(InputHelper inputHelper)
    {
    }
    
    public virtual void Update(GameTime gameTime)
    {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public virtual void Reset()
    {
        Visible = true;
    }

    public virtual Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0);
        }
    }
    #endregion
}