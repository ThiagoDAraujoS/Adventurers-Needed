using UnityEngine;
using Hell.Display;

namespace Hell
{
    /// <summary>
    /// Master Act Component targeted to Offensive acts
    /// </summary>
    public class MasterThresholdAttack : MasterAttack
    {
        [SerializeField]
        private int maxRange;

        public Tile CenterTile{ get; set; }

        /// <summary>
        /// initialization method
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// happens on each act's start
        /// </summary>
        /// <param name="token">Action Activation token</param>
        protected override void OnResolve(ActionToken token)
        {
            range = maxRange;

            Coordinate c;

            for (c = token.Owner.Coord + (token.Direction.toCoord() * maxRange); !Board.s.IsValidTile(c); c -= token.Direction)
                range--;
            CenterTile = Board.s[c];

            base.OnResolve(token);

            TargetCenterPosition = CenterTile.transform.position;
        }
    }
}