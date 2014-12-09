using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class PlayingState : IGameLoopObject
{
    #region Fields
    const int LevelsCount = 10;//

    public int CurrentLevelIndex { get; set; }
    public List<Level> Levels { get; private set; }
    public ContentManager Content { get; private set; }

    public Level CurrentLevel
    {
        get { return Levels[CurrentLevelIndex]; }
    }

    #endregion
    #region Constructor

    public PlayingState(ContentManager Content)
    {
        this.Content = Content;
        CurrentLevelIndex = -1;
        Levels = new List<Level>();
        LoadLevels();
        LoadLevelsStatus(Content.RootDirectory + "/Levels/levels_status.txt");
    }

    #endregion
    #region Methods

    public virtual void HandleInput(InputHelper InputHelper)
    {
        CurrentLevel.HandleInput(InputHelper);
    }

    public virtual void Update(GameTime GameTime)
    {
        CurrentLevel.Update(GameTime);
        if (CurrentLevel.GameOver)
            GameEnvironment.GameStateManager.SwitchTo("GameOverState");
        else if (CurrentLevel.Completed)
        {
            CurrentLevel.Solved = true;
            GameEnvironment.GameStateManager.SwitchTo("LevelFinishedState");
        }
    }

    public virtual void Draw(GameTime GameTime, SpriteBatch SpriteBatch)
    {
        CurrentLevel.Draw(GameTime, SpriteBatch);
    }

    public virtual void Reset()
    {
        CurrentLevel.Reset();
    }

    public void NextLevel()
    {
        CurrentLevel.Reset();
        if (CurrentLevelIndex >= Levels.Count - 1)
            GameEnvironment.GameStateManager.SwitchTo("LevelMenu");
        else
        {
            CurrentLevelIndex++;
            Levels[CurrentLevelIndex].Locked = false;
        }
        WriteLevelsStatus(Content.RootDirectory + "/Levels/levels_status.txt");
    }

    public void LoadLevels()
    {
        for (int currLevel = 1; currLevel <= LevelsCount; currLevel++)
            Levels.Add(new Level(currLevel));
    }

    public void LoadLevelsStatus(string path)
    {
        List<string> Textlines = new List<string>();
        StreamReader FileReader = new StreamReader(path);

        for (int i = 0; i < Levels.Count; i++)
        {
            string line = FileReader.ReadLine();
            string[] elems = line.Split(',');
            if (elems.Length == 2)
            {
                Levels[i].Locked = bool.Parse(elems[0]);
                Levels[i].Solved = bool.Parse(elems[1]);
            }
        }
        FileReader.Close();
    }

    public void WriteLevelsStatus(string path)
    {
        List<string> textlines = new List<string>();
        StreamWriter fileWriter = new StreamWriter(path, false);
        for (int i = 0; i < Levels.Count; i++)
        {
            string line = Levels[i].Locked.ToString() + "," + Levels[i].Solved.ToString();
            fileWriter.WriteLine(line);
        }
        fileWriter.Close();
    }
    #endregion
}