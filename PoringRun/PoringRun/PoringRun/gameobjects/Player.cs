using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;

class Player : AnimatedGameObject
{
    #region Fields
    public Vector2 StartPosition { get; private set; }
    public bool IsOnTheGround { get; private set; }
    public float PreviousYPosition { get; private set; }
    public bool IsAlive { get; private set; }
    public bool Finished { get; private set; }
    public bool WalkingOnIce { get; private set; }

    #endregion

    #region Constructor

    public Player(Vector2 start) : base(2, "player")
    {
        this.LoadAnimation("Sprites/Player/spr_idle@4", "idle", true); 
        this.LoadAnimation("Sprites/Player/spr_run@8", "run", true, 0.05f);
        this.LoadAnimation("Sprites/Player/spr_jump@8", "jump", false, 0.05f);
        this.LoadAnimation("Sprites/Player/spr_celebrate@6", "celebrate", false, 0.05f);
        this.LoadAnimation("Sprites/Player/spr_die@3", "die", false);

        StartPosition = start;
        Reset();
    }

    #endregion

    #region Methods

    public void Jump(float speed = 1100)
    {
        velocity.Y = -speed;
        GameEnvironment.LoadManager.PlaySound("Sounds/snd_player_jump");
    }

    private void DoPhysics()
    {
        velocity.Y += 55;
        if (IsAlive)
            HandleCollisions();
    }

    private void HandleCollisions()
    {
        IsOnTheGround = false;
        WalkingOnIce = false;

        TileField tiles = GameWorld.Find("tiles") as TileField;
        int x_floor = (int)position.X / tiles.CellWidth;
        int y_floor = (int)position.Y / tiles.CellHeight;

        for (int y = y_floor - 2; y <= y_floor + 1; ++y)
            for (int x = x_floor - 1; x <= x_floor + 1; ++x)
            {
                TileType tileType = tiles.GetTileType(x, y);
                if (tileType == TileType.Background)
                    continue;
                Tile currentTile = tiles.findAt(x, y) as Tile;
                Rectangle tileBounds = new Rectangle(x * tiles.CellWidth, y * tiles.CellHeight,
                                                        tiles.CellWidth, tiles.CellHeight);
                Rectangle boundingBox = this.BoundingBox;
                boundingBox.Height += 1;
                if (((currentTile != null && !currentTile.CollidesWith(this)) || currentTile == null) && !tileBounds.Intersects(boundingBox))
                    continue;
                Vector2 depth = Collision.CalculateIntersectionDepth(boundingBox, tileBounds);
                if (Math.Abs(depth.X) < Math.Abs(depth.Y))
                {
                    if (tileType == TileType.Normal)
                        position.X += depth.X;
                    continue;
                }
                if (PreviousYPosition <= tileBounds.Top && tileType != TileType.Background)
                {
                    IsOnTheGround = true;
                    velocity.Y = 0;
                    if (currentTile != null)
                    {
                        WalkingOnIce = WalkingOnIce || currentTile.Ice;
                    }
                }
                if (tileType == TileType.Normal || IsOnTheGround)
                    position.Y += depth.Y + 1;
            }
        position = new Vector2((float)Math.Floor(position.X), (float)Math.Floor(position.Y));
        PreviousYPosition = position.Y;
    }

    public override void Reset()
    {
        this.position = StartPosition;
        this.velocity = Vector2.Zero;
        IsOnTheGround = true;
        IsAlive = true;
        Finished = false;
        WalkingOnIce = false;;
        this.PlayAnimation("idle");
        PreviousYPosition = BoundingBox.Bottom;
    }

    public override void HandleInput(InputHelper InputHelper)
    {
        float walkingSpeed = 400;
        if (WalkingOnIce)
            walkingSpeed *= 1.5f;
        if (!IsAlive)
            return;
        if (InputHelper.IsKeyDown(Keys.Left))
            velocity.X = -walkingSpeed;
        else if (InputHelper.IsKeyDown(Keys.Right))
            velocity.X = walkingSpeed;
        else if (!WalkingOnIce && IsOnTheGround)
            velocity.X = 0.0f;
        if (velocity.X != 0.0f)
            Sprite.Mirror = velocity.X < 0;
        if ((InputHelper.KeyPressed(Keys.Space) || InputHelper.KeyPressed(Keys.Up)) && IsOnTheGround)
            Jump();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!Finished && IsAlive)
        {
            if (IsOnTheGround)
                if (velocity.X == 0)
                    this.PlayAnimation("idle");
                else
                    this.PlayAnimation("run");
            else if (velocity.Y < 0)
                this.PlayAnimation("jump");

            TileField tiles = GameWorld.Find("tiles") as TileField;
            if (BoundingBox.Top >= tiles.Rows * tiles.CellHeight)
                this.Die(true);
        }

        DoPhysics();
    }

    public void Die(bool falling)
    {
        IsAlive = !falling;
        velocity.X = 0.0f;
        if (falling)
        {
            GameEnvironment.LoadManager.PlaySound("Sounds/snd_player_fall");
            velocity.Y = -1000;
        }
        this.PlayAnimation("die");
    }

    public void LevelFinished()
    {
        Finished = true;
        velocity.X = 0.0f;
        this.PlayAnimation("celebrate");
    }
    #endregion
}

