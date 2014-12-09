using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    #region Fields

    public bool Locked { get; set; }
    public bool Solved { get; set; }
    private Button QuitButton;

    public bool GameOver
    {
        get
        {
            Player player = this.Find("player") as Player;
            return !player.IsAlive;
        }
    }

    #endregion
    #region Constructor

    public Level(int levelIndex)
    {
        GameObjectList backgrounds = new GameObjectList(0, "backgrounds");
        SpriteGameObject background_main = new SpriteGameObject("Backgrounds/spr_sky");
        background_main.Position = new Vector2(0, GameEnvironment.Screen.Y - background_main.Sprite.SheetHeight);
        backgrounds.Add(background_main);

        for (int i = 0; i < 5; i++)
        {
            SpriteGameObject mountain = new SpriteGameObject("Backgrounds/spr_mountain_" + (GameEnvironment.Random.Next(2) + 1), 1);
            mountain.Position = new Vector2((float)GameEnvironment.Random.NextDouble() * GameEnvironment.Screen.X - mountain.Sprite.SheetWidth / 2, GameEnvironment.Screen.Y - mountain.Sprite.SheetHeight);
            backgrounds.Add(mountain);
        }

        Clouds clouds = new Clouds(2);
        backgrounds.Add(clouds);
        this.Add(backgrounds);

        QuitButton = new Button("Sprites/spr_button_quit", 100);
        QuitButton.Position = new Vector2(GameEnvironment.Screen.X - QuitButton.Sprite.SheetWidth - 10, GameEnvironment.Screen.Y - QuitButton.Sprite.SheetHeight- 10);
        this.Add(QuitButton);


        this.Add(new GameObjectList(1, "ZenyS"));
        this.Add(new GameObjectList(2, "enemies"));

        this.LoadTiles("Content/Levels/" + levelIndex + ".txt");
    }

    #endregion
    #region Methods

    public bool Completed
    {
        get
        {
            SpriteGameObject exitObj = this.Find("exit") as SpriteGameObject;
            Player player = this.Find("player") as Player;
            if (!exitObj.CollidesWith(player))
                return false;
            GameObjectList ZenyS = this.Find("ZenyS") as GameObjectList;
            foreach (GameObject d in ZenyS.gameObjects)
                if (d.Visible)
                    return false;
            return true;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (QuitButton.Pressed)
        {
            this.Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Player player = this.Find("player") as Player;

        if (this.Completed)
        {
            player.LevelFinished();
        }
    }

    public override void Reset()
    {
        base.Reset();
    }

    #endregion
}

