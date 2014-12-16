using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Score : SpriteGameObject
{
    public int scoreValue { get; private set; }
    public int MaxScore { get; private set; }
    private SpriteFont ScoreFont;
    private GameObjectList ZenyS;


    public Score(GameObjectList ZenyS, int scoreValue = 0, int Layer = 0, string ID = "Score", int sheetIndex = 0)
        : base(@"Sprites\spr_zeny", Layer, ID, sheetIndex)
    {
        this.ZenyS = ZenyS;
        this.MaxScore = ZenyS.gameObjects.Count;
        this.scoreValue = 0;
        ScoreFont = GameEnvironment.LoadManager.ContentManager.Load<SpriteFont>("Fonts/ScoreFont");
    }

    public override void Reset()
    {
        base.Reset();
        this.scoreValue = 0;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        int count = 0;
        foreach (Zeny zeny in ZenyS.gameObjects)
        {
            if (zeny.Visible.Equals(false))
                count++;
        }
        scoreValue = count;
    }
    public override void Draw(GameTime GameTime, SpriteBatch ScoreText)
    {
        ScoreText.DrawString(ScoreFont, string.Format("{0} / {1}", scoreValue, MaxScore), new Vector2(GameEnvironment.Screen.X - 120, 30), Color.CadetBlue);
        base.Draw(GameTime, ScoreText);
    }
}

