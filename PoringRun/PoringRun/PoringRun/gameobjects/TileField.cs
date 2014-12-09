class TileField : GameObjectGrid
{
    #region Constructor

    public TileField(int Rows, int Columns, int Layer = 0, string ID = "")
        : base(Rows, Columns, Layer, ID)
    {
    }

    #endregion
    #region Methods

    public TileType GetTileType(int x, int y)
    {
        if (x < 0 || x >= Columns)
            return TileType.Normal;
        if (y < 0 || y >= Rows)
            return TileType.Background;
        Tile current = this.Grid[x, y] as Tile;
        return current.Type;
    }

    #endregion
}