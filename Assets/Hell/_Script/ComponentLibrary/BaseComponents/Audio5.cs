using Hell;
using Hell.Display;

namespace Hell.Rune
{
    public class Audio5 : TokenDrivenBehaviour<MasterAct>, IDisplay<Token>//ActVisualisation
    {
        public string audioStart;
        public string audioStop;

        public void TimelineStart(Token token)
        {
            if (!string.IsNullOrEmpty(audioStart))
            {
                AkSoundEngine.PostEvent(audioStart, gameObject);
            }
        }

        public void TimelineUpdate(Token token, float time)
        { }

        public void TimelineEnd(Token token)
        {
            if (!string.IsNullOrEmpty(audioStop))
            {
                AkSoundEngine.PostEvent(audioStop, gameObject);
            }
        }

    }
}