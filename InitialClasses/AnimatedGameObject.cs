using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class AnimatedGameObject : SpriteGameObject
{
    #region Fields
    public Dictionary<string, Animation> Animations { get; private set; }

    public Animation Current
    {
        get { return Sprite as Animation; }
    }
    #endregion

    #region Constructor
    public AnimatedGameObject(int Layer = 0, string ID = "")
        : base("", Layer, ID)
    {
        Animations = new Dictionary<string, Animation>();
    }
    #endregion

    #region Methods
    public void LoadAnimation(string contentName, string ID, bool looping, float frameTime = 0.1f)
    {
        Animation animation = new Animation(contentName, looping, frameTime);
        Animations[ID] = animation;
    }

    public void PlayAnimation(string ID)
    {
        if (Sprite == Animations[ID])
            return;
        if (Sprite != null)
            Animations[ID].Mirror = Sprite.Mirror;

        Animations[ID].Play();
        Sprite = Animations[ID];
        Origin = new Vector2(Sprite.SheetWidth, Sprite.SheetHeight);
    }

    public override void Update(GameTime gameTime)
    {
        if (Sprite == null)
            return;
        Current.Update(gameTime);
        base.Update(gameTime);
    }
    #endregion
}