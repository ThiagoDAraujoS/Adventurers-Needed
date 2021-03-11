using UnityEngine;
using System.Collections;
using Hell;
using Hell.Display;
namespace Hell.Rune
{

    public class StartTemporaryObjectsOnTiles : StartTemporaryObjectBase<MasterAttack>, IDisplay<ActionToken>
    {

        public void TimelineStart(ActionToken token)
        {
            foreach (var item in Master.TargetTiles)
                SpawnAndLatchTimer(token, item.transform.position);
        }
        public void TimelineEnd(ActionToken token) { }

        public void TimelineUpdate(ActionToken token, float time) { }
    }
}



