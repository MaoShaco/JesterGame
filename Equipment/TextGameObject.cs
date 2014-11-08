using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TextGameObject : GameObject
{
    #region Fields
    private SpriteFont spriteFont;

    public Color Color { get; set; }
    public string Text { get; set; }
    #endregion

    #region Constructor
    public TextGameObject(string contentName, int Layer = 0, string ID = "")
        : base(Layer, ID)
    {
        spriteFont = GameEnvironment.LoadManager.ContentManager.Load<SpriteFont>(contentName);
        Color = Color.White;
    }
    #endregion

    #region
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(spriteFont, Text, this.GlobalPosition, Color);
    }
    #endregion
}

