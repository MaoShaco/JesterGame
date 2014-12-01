using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteGameObject : GameObject
{
    #region
    public SpriteSheet Sprite { get; set; }
    public Vector2 Origin { get; set; }
    public Vector2 Center
    {
        get { return new Vector2(Sprite.SheetWidth, Sprite.SheetHeight) / 2; }
    }
    #endregion

    #region Constructor
    public SpriteGameObject(string ContentName, int Layer = 0, string ID = "", int sheetIndex = 0)
        : base(Layer, ID)
    {
        if (ContentName != "")
            Sprite = new SpriteSheet(ContentName, sheetIndex);
        else
            Sprite = null;
    }
    #endregion

    #region Methods
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!Visible || Sprite == null)
            return;
        Sprite.Draw(spriteBatch, this.GlobalPosition, Origin);
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - Origin.X);
            int top = (int)(GlobalPosition.Y - Origin.Y);
            return new Rectangle(left, top, Sprite.SheetWidth, Sprite.SheetHeight);
        }
    }

    public bool CollidesWith(SpriteGameObject obj)
    {
        if (!this.Visible || !obj.Visible || !BoundingBox.Intersects(obj.BoundingBox))
            return false;
        Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
        for (int x = 0; x < b.Width; x++)
            for (int y = 0; y < b.Height; y++)
            {
                int thisx = b.X - (int)(GlobalPosition.X - Origin.X) + x;
                int thisy = b.Y - (int)(GlobalPosition.Y - Origin.Y) + y;
                int objx = b.X - (int)(obj.GlobalPosition.X - obj.Origin.X) + x;
                int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.Origin.Y) + y;
                if (Sprite.GetPixelColor(thisx, thisy).A != 0
                    && obj.Sprite.GetPixelColor(objx, objy).A != 0)
                    return true;
            }
        return false;
    }
    #endregion
}

