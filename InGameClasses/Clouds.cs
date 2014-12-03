using Microsoft.Xna.Framework;

class Clouds : GameObjectList
{
    public Clouds(int Layer = 0, string ID = "")
        : base(Layer, ID)
    {
        for (int i = 0; i < 3; i++)
        {
            SpriteGameObject cloud = new SpriteGameObject("Backgrounds/spr_cloud_" + (GameEnvironment.Random.Next(5) + 1), 2);
            cloud.Position = new Vector2((float)GameEnvironment.Random.NextDouble() * GameEnvironment.Screen.X - cloud.Sprite.SheetWidth / 2, (float)GameEnvironment.Random.NextDouble() * GameEnvironment.Screen.Y - cloud.Sprite.SheetHeight / 2);
            cloud.Velocity = new Vector2((float)((GameEnvironment.Random.NextDouble() * 2) - 1) * 20, 0);
            this.Add(cloud);
        }
    }

    public override void Update(GameTime GameTime)
    {
        base.Update(GameTime);
        foreach (GameObject obj in gameObjects)
        {
            SpriteGameObject c = obj as SpriteGameObject;
            if ((c.Velocity.X < 0 && c.Position.X + c.Sprite.SheetWidth < 0) || (c.Velocity.X > 0 && c.Position.X > GameEnvironment.Screen.X))
            {
                this.Remove(c);
                SpriteGameObject cloud = new SpriteGameObject("Backgrounds/spr_cloud_" + (GameEnvironment.Random.Next(5) + 1));
                cloud.Velocity = new Vector2((float)((GameEnvironment.Random.NextDouble() * 2) - 1) * 20, 0);
                float cloudHeight = (float)GameEnvironment.Random.NextDouble() * GameEnvironment.Screen.Y - cloud.Sprite.SheetHeight / 2;
                if (cloud.Velocity.X < 0)
                    cloud.Position = new Vector2(GameEnvironment.Screen.X, cloudHeight);
                else
                    cloud.Position = new Vector2(-cloud.Sprite.SheetWidth, cloudHeight);
                this.Add(cloud);
                return;
            }
        }
    }
}

