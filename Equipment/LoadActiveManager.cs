using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

public class LoadActiveManager
{
    #region Fields
    public ContentManager ContentManager { get; private set; }
    #endregion

    #region Constructor
    public LoadActiveManager(ContentManager Content)
    {
        this.ContentManager = Content;
    }
    #endregion

    #region Methods
    public Texture2D GetSprite(string contentName)
    {
        return ContentManager.Load<Texture2D>(contentName);
    }

    public void PlaySound(string contentName)
    {
        SoundEffect snd = ContentManager.Load<SoundEffect>(contentName);
        snd.Play();
    }

    public void PlayMusic(string contentName, bool repeat = true)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(ContentManager.Load<Song>(contentName));
    }
    #endregion
}