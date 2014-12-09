using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    #region Methods

    public void LoadTiles(string path)
    {
        int width;
        List<string> textlines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        string line = fileReader.ReadLine();
        width = line.Length;
        while (line != null)
        {
            textlines.Add(line);
            line = fileReader.ReadLine();
        }
        TileField tiles = new TileField(textlines.Count - 1, width, 1, "tiles");

        this.Add(tiles);
        tiles.CellWidth = 72;
        tiles.CellHeight = 55;
        for (int x = 0; x < width; ++x)
            for (int y = 0; y < 16 - 1; ++y)
            {
                Tile t = LoadTile(textlines[y][x], x, y);
                tiles.Add(t, x, y);
            }
    }

    private Tile LoadTile(char tileType, int x, int y)
    {
        switch (tileType)
        {
            case '.':
                return new Tile();
            case '-':
                return LoadBasicTile("spr_platform", TileType.Platform);
            case '@':
                return LoadBasicTile("spr_platform_ice", TileType.Platform, false, true);
            case 'X':
                return LoadEndTile(x, y);
            case 'W':
                return LoadZenyTile(x, y);
            case '1':
                return LoadStartTile(x, y);
            case '#':
                return LoadBasicTile("spr_wall", TileType.Normal);
            case '*':
                return LoadBasicTile("spr_wall_ice", TileType.Normal, false, true);
            default:
                return new Tile("");
        }
    }

    private Tile LoadBasicTile(string name, TileType tileType, bool hot = false, bool ice = false)
    {
        Tile t = new Tile("Tiles/" + name, tileType);
        t.Ice = ice;
        return t;
    }

    private Tile LoadStartTile(int x, int y)
    {
        TileField tiles = this.Find("tiles") as TileField;
        Vector2 startPosition = new Vector2(((float)x + 0.5f) * tiles.CellWidth, (y + 1) * tiles.CellHeight);
        Player player = new Player(startPosition);
        this.Add(player);
        return new Tile("", TileType.Background);
    }

    private Tile LoadEndTile(int x, int y)
    {
        TileField tiles = this.Find("tiles") as TileField;
        SpriteGameObject exitObj = new SpriteGameObject("Sprites/spr_goal", 1, "exit");
        exitObj.Position = new Vector2(x * tiles.CellWidth, (y + 1) * tiles.CellHeight);
        exitObj.Origin = new Vector2(0, exitObj.Sprite.SheetHeight);
        this.Add(exitObj);
        return new Tile();
    }

    private Tile LoadZenyTile(int x, int y)
    {
        GameObjectList ZenyS = this.Find("ZenyS") as GameObjectList;
        TileField tiles = this.Find("tiles") as TileField;
        Zeny w = new Zeny();
        w.Origin = w.Center;
        w.Position = new Vector2(x * tiles.CellWidth, y * tiles.CellHeight - 10);
        w.Position += new Vector2(tiles.CellWidth, tiles.CellHeight) / 2;
        ZenyS.Add(w);
        return new Tile();
    }
    #endregion
}