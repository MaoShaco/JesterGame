using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Zeny : SpriteGameObject
{
    #region Fields

    public float Bounce { get; private set; }

    #endregion
    #region Constructor

    public Zeny(int Layer = 0, string ID = "")
        : base("Sprites/spr_zeny", Layer, ID)
    { }

    #endregion
    #region Methods

    public override void Update(GameTime gameTime)
    {
        double t = gameTime.TotalGameTime.TotalSeconds * 3.0f + Position.X;
        Bounce = (float)Math.Sin(t) * 0.2f;
        position.Y += Bounce;
        Player player = GameWorld.Find("player") as Player;
        if (this.Visible && this.CollidesWith(player))
        {
            this.Visible = false;
            GameEnvironment.LoadManager.PlaySound("Sounds/snd_zeny_collected");
        }
    }

    #endregion
}
