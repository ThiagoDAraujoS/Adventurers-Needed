using System.Collections;

namespace Hell.Display
{
    /// <summary>
    /// This class wrap all tile tile behaviour in the timeline
    /// </summary>
    public class TileToken : BoardToken, IPlayable
    {
        /// <summary>
        /// The target of the tile
        /// </summary>
        public override Pawn Owner { get { return Tile.Pawn; } }

        /// <summary>
        /// The tile triggerred
        /// </summary>
        public Tile Tile { get; private set; }

        public Tile PrevTile { get; private set; }

        /// <summary>
        /// The tile master event
        /// </summary>
        public MasterTile Master { get; private set; }

        /// <summary>
        /// Iplayable get duration 
        /// </summary>
        /// <returns></returns>
        public float GetDuration() { return Duration; }

        /// <summary>
        /// Ctor
        /// </summary>
         public TileToken(Direction direction, TileSettings settings, float start = 0.0f) : 
            base(settings.tile.Pawn, direction, settings, start){
            Master = settings.master;
            Tile = settings.tile;
        }

        /// <summary>
        /// Play this master resolution
        /// </summary>
        public void PlayResolution(){
            Master.RunResolution(this);
        }

        /// <summary>
        /// Play this master start
        /// </summary>
        public void PlayStart(){
            Master.RunStart(this);
        }

        /// <summary>
        /// play this master update
        /// </summary>
        /// <param name="time"></param>
        public void PlayUpdate(float time){
            Master.RunUpdate(this, time);
        }

        /// <summary>
        /// play this mater end
        /// </summary>
        public void PlayEnd(){
            Master.RunEnd(this);
        }

        /// <summary>
        /// When activate run the display coroutine
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Activate()
        {
            yield return this.DisplayCoroutine();
        }

        /// <summary>
        /// This class wraps all settings needed to build a tile
        /// </summary>
        public class TileSettings : BoardSettings
        {
            public Tile tile;
            public MasterTile master;

            public TileSettings(MasterTile master, Tile tile, float padding = 0.0f) : 
                base(master.actDuration, padding)
            {
                this.master = master;
                this.tile = tile;
            }
        }
    }
}
