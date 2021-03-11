using UnityEngine;
using System.Collections.Generic;
using Hell.Display;
using System;
using System.Collections;
using System.Reflection;
namespace Hell
{
    /// <summary>
    /// This class describes everything that can be placed in the board
    /// </summary>
    public class Pawn : MonoBehaviour, IPawn
    {
        public string[] deathSounds;

        public float PawnRotationSpeed = 20.0f;

      //  public Renderer[] characterModel;

        public bool destroyOnDeath = false;

        /// <summary>
        /// On move event, Tile from, Tile to
        /// </summary>
        public event Action<Tile, Tile> OnMove = delegate { };

        public bool isIndestructible = false;
        //public GameObject grave;

        private static List<Pawn> pawns;
        public static List<Pawn> Pawns
        {
            get
            {
                if (pawns == null)
                    pawns = new List<Pawn>();
                return pawns;
            }
        }

        public event Action<int> OnHeal;
        public event Action<int> OnDamage;
        public event Action OnDeath;

        public AnimTrigger
            damageAnim,
            deathAnim,
            healAnim;

        public GameObject deathParticlePrefab;

        private IEnumerator Death()
        {
            if (OnDeath != null)
                OnDeath();
            IsAlive = false;

            foreach (string sound in deathSounds)
                AkSoundEngine.PostEvent(sound, gameObject);

            //     AkSoundEngine.PostEvent("Play_VO_AN_Fallen", gameObject);
            //    AkSoundEngine.PostEvent("Play_Gravestone_02", gameObject);

            if (deathParticlePrefab != null)
                Destroy(Instantiate(deathParticlePrefab, transform.position, transform.rotation), 5.0f);

            if (destroyOnDeath)
                Destroy(gameObject);

            else
                foreach (Transform t in transform)
                {
                    if (t.name == "Body")
                        t.gameObject.SetActive(false);
                    if (t.name == "GraveStone_2")
                        t.gameObject.SetActive(true);
                }

            yield return new WaitForSeconds(1.0f);
        }
        private IEnumerator Heal(int amount)
        {
            if (OnHeal != null)
                OnHeal(amount);
            //     SetAnim(healAnim);
            yield return new WaitForSeconds(1.0f);
        }
        private IEnumerator Damage(int amount)
        {

            if (OnDamage != null)
                OnDamage(amount);

            //     SetAnim(damageAnim);
            yield return new WaitForSeconds(1.0f);
        }

        [HideInInspector]
        public int currentLife;

        [SerializeField]
        private int maxLife;

        [HideInInspector]
        private int life;

        public int MaxLife
        {
            get { return maxLife; }
            private set { maxLife = value; }
        }

        public IEnumerator WipeLifeBuffer()
        {
            //@TODO = player life cannot be bigger then max life
            if (IsAlive)
            {
                int originalLife = life;
                int lifeChanged = currentLife - life;
                life = currentLife;
                if (lifeChanged < 0)
                {
                    yield return Damage(lifeChanged * -1);
                    if (life <= 0 && !isIndestructible)
                        yield return Death();
                }
                else if (lifeChanged > 0)
                    yield return Heal(lifeChanged);
            }
        }

        public static IEnumerator WipeEachLifeBuffer()
        {
            yield return RoomManager.s.StartMultiCoroutine(Pawns.Select(o => o.WipeLifeBuffer));
        }

        public bool IsAlive { get; set; }

        public void SetAnim(AnimTrigger state)
        {
            if (Anim != null)
                Anim.SetTrigger(state.ToString());
        }

        private Animator Anim { get; set; }

        protected virtual void Initialize(Color color, Transform parent)
        {
            IsAlive = true;
            currentLife = maxLife;
            life = maxLife;
            Anim = GetComponentInChildren<Animator>();
            currentLife = life;
            Pawns.Add(this);
        }

        public Coordinate Coord
        {
            get
            {
                if (Tile == null)
                    return Coordinate.zero;
                return Tile.coord;
            }
        }

        public Tile tile;

        public Tile Tile
        {
            get { return tile; }
            set
            {
                //If its going to a new tile
                if (tile != value)
                {

                    //trigger move event if i am not there yet
                    if (value != null && transform.position != value.transform.position)
                        OnMove(tile, value);

                    //if the holded tile is not null
                    if (tile != null)

                        //erase the pawn inside the holded tile
                        tile.Pawn = null;

                    //swap tiles
                    tile = value;

                    //if the new tile is not null
                    if (tile != null)

                        //save info inside the new tile
                        tile.Pawn = this;
                }
            }
        }

        protected IEnumerator LookAtRoutine(Quaternion direction)
        {
            yield return new WaitWhile(() => {
                transform.rotation = Quaternion.Lerp(transform.rotation, direction, Time.deltaTime * PawnRotationSpeed);
                return Quaternion.Angle(transform.rotation, direction) > 1.0f;
            });
        }

        public void LookAt(Direction direction)
        {
            StartCoroutine(LookAtRoutine(
                Quaternion.LookRotation(direction.toCoord().ToVector3(0.0f))));
        }

        public void FireTileEffects(float start, Direction direction, Predicate<MasterTile> validation = null)
        {
            foreach (MasterTile effect in Tile.effects)
                if (validation == null || validation(effect))
                {
                    //craft
                    TileToken tileToken = new TileToken(direction,
                        new TileToken.TileSettings(effect, Tile, effect.padding), start);

                    //resolve
                    effect.RunResolution(tileToken);

                    //stash
                    Board.s.MoveTimeline.AddToken(tileToken);
                }
        }

        public void FireTileEffects(MoveToken token, Predicate<MasterTile> validation = null)
        {
            FireTileEffects(token.End, token.Direction, validation);
        }

        public float Move(Direction direction, MoveToken.Pair walk, MoveToken.Pair push = null, float start = 0.0f, Predicate<MasterTile> validation = null)
        {
            //get direction
            Coordinate destination = Coord + direction;

            //Auxiliar token reference
            BoardToken auxToken;

            if (walk != null)
                walk.owner = this;

            if (push != null)
                push.owner = this;

            //if pawn can walk
            if (Board.s.IsWalkable(destination))
            {
                //set pawn's tile
                Tile = Board.s[destination];

                //craft move token
                auxToken = walk.GetMoveToken(Tile, start);

                //stash tile effect tokens
                FireTileEffects(auxToken.End, direction, validation);

                //stash move token
                Board.s.MoveTimeline.AddToken(auxToken);
            }
            else
            {
                //craft animation token
                auxToken = walk.GetAnimToken(start);

                //stash animation token
                Board.s.MoveTimeline.AddToken(auxToken);

                //if theres a push settings, and is inside board, and the tile hosts a pawn
                if (push != null && Board.s.IsInsideBoard(destination) && Board.s[destination].Pawn != null)

                    //issue a move order to that pawn
                    Board.s[destination].Pawn.Move(direction, push, null, auxToken.End);
            }
            return auxToken.End;
        }

        void OnDestroy()
        {
            Tile = null;
            Pawns.Remove(this);
        }

        public static implicit operator bool(Pawn p)
        {
            return (p != null);
        }
    }

    public enum AnimTrigger
    {
        BombKB,
        Heal,
        Idle,
        Walk,
        Skill1,
        Death,
        Bump,
        Slide,
        BumpBump,
        Casting1,
        Damage,
        Drop,
        PortIn,
        PortOut,
        Jump,
        Dash,
    }
}