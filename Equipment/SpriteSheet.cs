using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteSheet
{
    #region Fields
    public Texture2D Sprite { get; private set; }
    public int SheetWidth
    {
        get
        {
            return Sprite.Width / SheetColumns;
        }
    }
    public int SheetHeight
    {
        get
        {
            return Sprite.Height / SheetRows;
        }
    }
    public Vector2 SheetCenter
    {
        get { return new Vector2(SheetWidth, SheetHeight) / 2; }
    }

    public int SheetIndex { get; set; }
    public int SheetColumns { get; private set; }
    public int SheetRows { get; private set; }
    public int SheetsAmount
    {
        get
        {
            return SheetColumns * SheetRows;
        }
    }
    public bool Mirror { get; set; }
    #endregion

    #region Constructor
    public SpriteSheet(string ContentName, int SheetIndex = 0)
    {
        Sprite = GameEnvironment.LoadManager.GetSprite(ContentName);
        this.SheetIndex = SheetIndex;
        this.SheetColumns = 1;
        this.SheetRows = 1;

        string[] assetSplit = ContentName.Split('@');
        if (assetSplit.Length <= 1)
            return;

        string sheetNrData = assetSplit[assetSplit.Length - 1];
        string[] colrow = sheetNrData.Split('x');
        this.SheetColumns = int.Parse(colrow[0]);
        if (colrow.Length == 2)
            this.SheetRows = int.Parse(colrow[1]);
    }
    #endregion

    #region Methods
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin)
    {
        int columnIndex = this.SheetIndex % this.SheetColumns;
        int rowIndex = this.SheetIndex / this.SheetColumns % this.SheetRows;
        Rectangle spritePart = new Rectangle(columnIndex * this.SheetWidth, rowIndex * this.SheetHeight, this.SheetWidth, this.SheetHeight);
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (Mirror)
            spriteEffects = SpriteEffects.FlipHorizontally;
        spriteBatch.Draw(Sprite, position, spritePart, Color.White, 0.0f, origin, 1.0f, spriteEffects, 0.0f);
    }
    #endregion
}