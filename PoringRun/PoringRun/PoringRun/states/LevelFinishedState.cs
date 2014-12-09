using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class LevelFinishedState : GameObjectList
{
    #region Fields

    public IGameLoopObject PlayingState { get; private set; }

    #endregion
    #region Constructor
    public LevelFinishedState()
    {
        PlayingState = GameEnvironment.GameStateManager.Get("PlayingState");
        SpriteGameObject overlay = new SpriteGameObject("Overlays/spr_welldone");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Sprite.SheetCenter;
        this.Add(overlay);
    }

    #endregion
    #region Methods

    public override void HandleInput(InputHelper InputHelper)
    {
        if (!InputHelper.KeyPressed(Keys.Space))
            return;
        GameEnvironment.GameStateManager.SwitchTo("PlayingState");
        (PlayingState as PlayingState).NextLevel();
    }

    public override void Update(GameTime GameTime)
    {
        PlayingState.Update(GameTime);
    }

    public override void Draw(GameTime GameTime, SpriteBatch SpriteBatch)
    {
        PlayingState.Draw(GameTime, SpriteBatch);
        base.Draw(GameTime, SpriteBatch);
    }

    #endregion
}