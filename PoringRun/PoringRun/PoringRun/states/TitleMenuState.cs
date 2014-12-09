using Microsoft.Xna.Framework;

class TitleMenuState : GameObjectList
{
    #region Fields

    public Button PlayButton { get; private set; }

    #endregion
    #region Constructor

    public TitleMenuState()
    {
        SpriteGameObject title_screen = new SpriteGameObject("Backgrounds/spr_title", 0, "background");
        this.Add(title_screen);

        PlayButton = new Button("Sprites/spr_button_play", 1);
        PlayButton.Position = new Vector2((GameEnvironment.Screen.X - PlayButton.Sprite.SheetWidth) / 2, 650);
        this.Add(PlayButton);
    }

    #endregion
    #region Methods

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (PlayButton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("LevelMenu");
    }
    #endregion
}
