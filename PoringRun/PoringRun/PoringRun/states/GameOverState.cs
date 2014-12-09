using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameOverState : GameObjectList
{
    #region Fields

    public IGameLoopObject PlayingState { get; private set; }

    #endregion
    #region Constructor

    public GameOverState()
    {
        PlayingState = GameEnvironment.GameStateManager.Get("PlayingState");
        SpriteGameObject Overlay = new SpriteGameObject("Overlays/spr_gameover");
        Overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - Overlay.Sprite.SheetCenter;
        this.Add(Overlay);
    }

    #endregion
    #region Methods

    public override void HandleInput(InputHelper InputHelper)
    {
        if (!InputHelper.KeyPressed(Keys.Space))
            return;
        PlayingState.Reset();
        GameEnvironment.GameStateManager.SwitchTo("PlayingState");
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