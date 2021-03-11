using UnityEngine;
using System.Collections;
using Hell.Display;
using Hell.Rune.Target;
using Hell.Rune;
using System.Collections.Generic;

namespace Hell
{
	/// <summary>
	/// Master Act Component targeted to Offensive acts
	/// </summary>
	public class MasterAttack : MasterAct
	{
		/// <summary>
		/// The range
		/// </summary>
		public int range;

        public bool canTargetSelf = false;

        //---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// All targets affected by this act
        /// </summary>
        [HideInInspector]
        public List<Tile> TargetTiles;

        /// <summary>
        /// The epicenter of the attack
        /// </summary>
        [HideInInspector]
		public virtual Coordinate TargetCenter { get; protected set; }

		/// <summary>
		/// starting/ending world positions
		/// </summary>
		[HideInInspector]
		public Vector3 StartingPosition { get; protected set; }

		[HideInInspector]
		public virtual Vector3 TargetCenterPosition{ get; protected set; }

        [HideInInspector]
        public List<Pawn> TargetPawns { get; protected set; }
		//---------------------------------------------------------------------------------------------------------

		/// <summary>
		/// The targeting system this act is using
		/// </summary>
		public TargetSystem targetSystem { get; protected set; }

        /// <summary>
        /// initialization method
        /// </summary>
        protected override void Initialize ()
		{
			base.Initialize ();
			targetSystem = GetComponent<TargetSystem>();
			if (targetSystem == null)
				targetSystem = gameObject.AddComponent<BasicTargetSystem> ();
            gameObject.InitializeComponent<TargetSystem>();
		}

		/// <summary>
		/// happens on each act's start
		/// </summary>
		/// <param name="token">Action Activation token</param>
		protected override void OnResolve (ActionToken token)
		{
			StartingPosition = token.Owner.transform.position;

            TargetCenter = Board.s.GetRangedCoord (token.Owner.Coord, token.Direction, range);

            TargetCenterPosition = Board.s.TheoreticalTilePosition (TargetCenter, token.Owner.transform.position.y);

            TargetTiles = targetSystem.GetTiles (token, TargetCenter, token.Direction);
            if (!canTargetSelf)
                TargetTiles.Remove(token.Owner.Tile);

            TargetPawns = targetSystem.GetTargetPawns(token, ref TargetTiles, TargetCenter, token.Direction);
            if(!canTargetSelf)
                TargetPawns.Remove(token.Owner);
		}
	}
}