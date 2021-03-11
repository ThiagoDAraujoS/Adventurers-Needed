using UnityEngine;
using System.Collections;
using Hell;
using Hell.Display;
namespace Hell.Rune
{
    public class StartTemporaryObject : StartTemporaryObjectBase<MasterAct>, IDisplay<ActionToken>
    {
        public float trackingSpeed;

        public void TimelineStart(ActionToken token) {
            GameObject go = SpawnAndLatchTimer(token, token.Owner.transform.position);
            go.gameObject.AddComponent<Follow>().Initialize(Follow.Setting.position, token.Owner.transform, trackingSpeed, 0.0f);
        }

        public void TimelineEnd(ActionToken token){}

        public void TimelineUpdate(ActionToken token, float time){}
    }

    public enum TimeFrame
    {
        turn,
        act,
        phase
    }

    public abstract class StartTemporaryObjectBase<MasterType> : TokenDrivenBehaviour<MasterType>
        where MasterType : MasterAct
    {
        public TimeFrame timeframe = TimeFrame.act;

        public GameObject prefab;
        public int duration;
        public bool isStart;
        public float destructionDelay;
        protected TurnTimer timer;

        public GameObject SpawnAndLatchTimer(Token token, Vector3 position)
        {
            timer = (Instantiate(prefab) as GameObject).AddComponent<TurnTimer>();
            timer.Initialize(isStart, token.Owner as Character, duration, destructionDelay);
            timer.transform.position = position;
            return timer.gameObject;
        }
    }
}



