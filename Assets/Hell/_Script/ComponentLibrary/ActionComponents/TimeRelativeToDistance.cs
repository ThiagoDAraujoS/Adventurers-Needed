using Hell.Display;

namespace Hell.Rune
{
    public class TimeRelativeToDistance : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public bool aimTargetPawn = true;
        private float? actDuration = null;

        public void TimelineEnd(ActionToken token) { }

        public void TimelineStart(ActionToken token) {
            if (actDuration == null)
                actDuration = Master.actDuration;

            if(aimTargetPawn && Master.TargetPawns.Count > 0)
                Master.actDuration = Master.TargetPawns[0].Coord.Distance(token.Owner.Coord) * actDuration.Value;

            else
                Master.actDuration = Master.TargetCenter.Distance(token.Owner.Coord) * actDuration.Value;
        }

        public void TimelineUpdate(ActionToken token, float time) { }
    }
}
