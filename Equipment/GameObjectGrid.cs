using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObjectGrid : GameObject
{
    #region Fields
    public GameObject[,] Grid { get; private set; }
    public int CellWidth { get; set; }
    public int CellHeight { get; set; }
    public int Columns
    {
        get { return Grid.GetLength(0); }
    }
    public int Rows
    {
        get { return Grid.GetLength(1); }
    }

    #endregion

    #region Constructor
    public GameObjectGrid(int rows, int columns, int Layer = 0, string ID = "")
        : base(Layer, ID)
    {
        Grid = new GameObject[columns, rows];
        for (int x = 0; x < columns; x++)
            for (int y = 0; y < rows; y++)
                Grid[x, y] = null;
    }
    #endregion

    #region Methods
    public void Add(GameObject obj, int x, int y)
    {
        Grid[x, y] = obj;
        obj.Parent = this;
        obj.Position = new Vector2(x * CellWidth, y * CellHeight);
    }

    public GameObject findAt(int x, int y)
    {
        if (x >= 0 && x < Columns && y >= 0 && y < Rows)
            return Grid[x, y];
        else
            return null;
    }

    public Vector2 findObjectPosition(GameObject s)
    {
        for (int x = 0; x < Columns; x++)
            for (int y = 0; y < Rows; y++)
                if (Grid[x, y] == s)
                    return new Vector2(x * CellWidth, y * CellHeight);
        return Vector2.Zero;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        foreach (GameObject obj in Grid)
            obj.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in Grid)
            obj.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in Grid)
            obj.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in Grid)
            obj.Reset();
    }

    #endregion
}
