using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

enum TileType
{
    Background,
    Normal,
    Platform
}

class Tile : SpriteGameObject
{
    #region Field

    public TileType Type { get; private set; }
    public bool Ice { get; set; }

    #endregion
    #region Constructor

    public Tile(string contentName = "", TileType tileType = TileType.Background, int Layer = 0, string ID = "")
        : base(contentName, Layer, ID)
    {
        Type = tileType;
        Ice = false;
    }

    #endregion
    #region Methods

    public override void Draw(GameTime GameTime, SpriteBatch SpriteBatch)
    {
        if (Type == TileType.Background)
            return;
        base.Draw(GameTime, SpriteBatch);
    }
    #endregion
}
