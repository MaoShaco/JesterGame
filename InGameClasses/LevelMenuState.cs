using System.Collections.Generic;
using Microsoft.Xna.Framework;

class LevelMenuState : GameObjectList
{
    #region Fields

    public Button BackButton { get; private set; }

    public int LevelSelected
    {
        get
        {
            foreach (GameObject obj in this.gameObjects)
            {
                LevelButton levelButton = obj as LevelButton;
                if (levelButton != null && levelButton.Pressed)
                    return levelButton.LevelIndex;
            }
            return -1;
        }
    }

    #endregion
    #region Constructor

    public LevelMenuState()
    {
        PlayingState playingState = GameEnvironment.GameStateManager.Get("PlayingState") as PlayingState;
        List<Level> levels = playingState.Levels;

        SpriteGameObject background = new SpriteGameObject("Backgrounds/spr_levelselect", 0, "background");
        this.Add(background);

        for (int i = 0; i < 10; i++)
        {
            int row = i / 4;
            int column = i % 4;
            LevelButton level = new LevelButton(i + 1, levels[i], 1);
            level.Position = new Vector2(column * (level.LockedLevel.Sprite.SheetWidth + 20), row * (level.LockedLevel.Sprite.SheetHeight + 20)) + new Vector2(390, 180);
            this.Add(level);
        }

        BackButton = new Button("Sprites/spr_button_back", 1);
        BackButton.Position = new Vector2((GameEnvironment.Screen.X - BackButton.Sprite.SheetWidth) / 2, 750);
        this.Add(BackButton);
    }

    #endregion
    #region Methods

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (LevelSelected != -1)
        {
            PlayingState PlayingState = GameEnvironment.GameStateManager.Get("PlayingState") as PlayingState;
            PlayingState.CurrentLevelIndex = LevelSelected - 1;
            GameEnvironment.GameStateManager.SwitchTo("PlayingState");
        }
        else if (BackButton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("TitleMenu");
    }
    #endregion
}

