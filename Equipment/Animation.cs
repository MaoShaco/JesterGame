using System;
using Microsoft.Xna.Framework;

public class Animation : SpriteSheet
{
    #region Fields
    public float FrameTime { get; private set; }
    public bool IsLooping { get; private set; }
    public float time { get;private set; }
    #endregion

    #region Constuctor
    public Animation(string contentName, bool IsLooping, float Frametime = 0.1f)
        : base(contentName)
    {
        this.FrameTime = Frametime;
        this.IsLooping = IsLooping;
    }
    #endregion

    #region Methods
    public void Play()
    {
        this.SheetIndex = 0;
        this.time = 0.0f;
    }

    public void Update(GameTime gameTime)
    {
        time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        while (time > FrameTime)
        {
            time -= FrameTime;
            if (IsLooping)
                SheetIndex = (SheetIndex + 1) % this.SheetsAmount;
            else
                SheetIndex = Math.Min(SheetIndex + 1, this.SheetsAmount - 1);
        }
    }

    public bool AnimationEnded
    {
        get { return !this.IsLooping && SheetIndex >= SheetsAmount - 1; }
    }
    #endregion
}

