using UnityEngine;
using System.Collections;
using Hell.Display;
using System.Collections.Generic;
using Hell.Rune.Target;

namespace Hell.Rune
{
    public class Channel : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public GameObject channelPrefab;
        public int range;
        private List<GameObject> channel;

        private Vector3
            destination,
            starting;

        private TargetSystem targetSystem;

        public AnimationCurve[] pathCurves;

        List<Tile> targets;

        public void TimelineStart(ActionToken token)
        {
            if (targetSystem == null)
                targetSystem = GetComponent<TargetSystem>();

            targets = targetSystem.GetTiles(token, token.Owner.Coord + (token.Direction.toCoord() * range));

            channel = new List<GameObject>();

            starting = token.Owner.transform.position;

            for (int i = 0; i < targets.Count; i++)
            {
                GameObject obj = Instantiate(channelPrefab) as GameObject;
                Debug.Log(obj);
                channel.Add(obj);
                obj.transform.position = targets[i].transform.position;
            }
        }

        public void TimelineEnd(ActionToken token)
        {
            foreach (var item in channel)
                Destroy(item.gameObject);
        }

        public void TimelineUpdate(ActionToken token, float time)
        {
            for (int i = 0; i < channel.Count; i++)
                channel[i].transform.position = Vector3.Lerp(token.Owner.transform.position, targets[i].transform.position, pathCurves[i].Evaluate(time));
        }
    }
}