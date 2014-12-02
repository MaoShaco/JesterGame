using Microsoft.Xna.Framework;

class Button : SpriteGameObject
{
    #region Fields

    public bool Pressed { get; private set; }

    #endregion

    #region Constructor

    public Button(string imageAsset, int layer = 0, string id = "")
        : base(imageAsset, layer, id)
    {
        Pressed = false;
    }

    #endregion

    #region Methods

    public override void HandleInput(InputHelper inputHelper)
    {
        Pressed = inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public override void Reset()
    {
        base.Reset();
        Pressed = false;
    }
    #endregion
}
