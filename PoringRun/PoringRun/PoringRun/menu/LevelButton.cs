using Microsoft.Xna.Framework;

class LevelButton : GameObjectList
{
    #region Fields

    public bool Pressed { get; private set; }
    public int LevelIndex { get; private set; }
    public Level Level { get; private set; }

    public SpriteGameObject SolvedLevel { get; private set; }
    public SpriteGameObject unSolvedLevel { get; private set; }
    public SpriteGameObject LockedLevel { get; private set; }

    #endregion

    #region Constructor

    public LevelButton(int LevelIndex, Level Level, int Layer = 0, string ID = "")
        : base(Layer, ID)
    {
        this.LevelIndex = LevelIndex;
        this.Level = Level;

        SolvedLevel = new SpriteGameObject("Sprites/spr_level_solved", 0, "", LevelIndex - 1);
        this.Add(SolvedLevel);

        unSolvedLevel = new SpriteGameObject("Sprites/spr_level_unsolved");
        this.Add(unSolvedLevel);

        LockedLevel = new SpriteGameObject("Sprites/spr_level_locked", 2);
        this.Add(LockedLevel); 
    }

    #endregion

    #region Methods

    public override void HandleInput(InputHelper inputHelper)
    {
        Pressed = inputHelper.MouseLeftButtonPressed() && !Level.Locked &&
            SolvedLevel.BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        LockedLevel.Visible = Level.Locked;
        SolvedLevel.Visible = Level.Solved;
        unSolvedLevel.Visible = !Level.Solved;
    }

    #endregion
}
