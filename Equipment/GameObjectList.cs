using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObjectList : GameObject
{
    #region Fields
    public List<GameObject> gameObjects { get; private set; }
    #endregion

    #region Constructor
    public GameObjectList(int Layer = 0, string ID = "")
        : base(Layer, ID)
    {
        gameObjects = new List<GameObject>();
    }
    #endregion

    #region Methods
    public void Add(GameObject obj)
    {
        obj.Previous = this;
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].Layer > obj.Layer)
            {
                gameObjects.Insert(i, obj);
                return;
            }
        }
        gameObjects.Add(obj);
    }

    public void Remove(GameObject obj)
    {
        gameObjects.Remove(obj);
        obj.Previous = null;
    }

    public GameObject Find(string ID)
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj.ID == ID)
                return obj;
        }
        return null;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
            gameObjects[i].HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in gameObjects)
            obj.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        List<GameObject>.Enumerator e = gameObjects.GetEnumerator();
        while (e.MoveNext())
            e.Current.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in gameObjects)
            obj.Reset();
    }
    #endregion
}
