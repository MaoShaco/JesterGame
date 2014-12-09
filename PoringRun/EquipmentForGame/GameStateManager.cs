using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager : IGameLoopObject
{
    #region fields
    private Dictionary<string, IGameLoopObject> gameStates;
    public IGameLoopObject CurrentGameState { get; private set; }
    #endregion

    #region Constructor
    public GameStateManager()
    {
        gameStates = new Dictionary<string, IGameLoopObject>();
        CurrentGameState = null;
    }
    #endregion

    #region Methods
    public void Add(string name, IGameLoopObject state)
    {
        gameStates[name] = state;
    }

    public IGameLoopObject Get(string name)
    {
        return gameStates[name];
    }

    public void SwitchTo(string name)
    {
        if (gameStates.ContainsKey(name))
            CurrentGameState = gameStates[name];
    }

    public void HandleInput(InputHelper inputHelper)
    {
        CurrentGameState.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        CurrentGameState.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        CurrentGameState.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
        CurrentGameState.Reset();
    }
    #endregion
}
