using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

class PoringRun : GameEnvironment
{
    static void Main()
    {
        PoringRun game = new PoringRun();
        game.Run();
    }

    public PoringRun()
    {
        Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        Screen = new Point(1440, 825);
        this.SetFullScreen(true);

        GameStateManager.Add("TitleMenu", new TitleMenuState());
        GameStateManager.Add("PlayingState", new PlayingState(Content));
        GameStateManager.Add("LevelMenu", new LevelMenuState());
        GameStateManager.Add("GameOverState", new GameOverState());
        GameStateManager.Add("LevelFinishedState", new LevelFinishedState());
        GameStateManager.SwitchTo("TitleMenu");

        LoadManager.PlayMusic("Sounds/snd_music");
    }
}