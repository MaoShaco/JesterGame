using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteGameObject : GameObject
{
    #region Fields
    public SpriteSheet Sprite { get; protected set; }
    public Vector2 Origin { get; set; }
    #endregion

    #region Constructor
    public SpriteGameObject(string contentName, int Layer = 0, string ID = "", int sheetIndex = 0)
        : base(Layer, ID)
    {
        if (contentName != "")
            Sprite = new SpriteSheet(contentName, sheetIndex);
        else
            Sprite = null;
    }
    #endregion

    #region Methods
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, this.GlobalPosition, Origin);
    }

    public Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - Origin.X);
            int top = (int)(GlobalPosition.Y - Origin.Y);
            return new Rectangle(left, top, Sprite.SheetWidth, Sprite.SheetHeight);
        }
    }
    #endregion
}

