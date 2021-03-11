using UnityEngine;
using System.Collections.Generic;
using Hell.Display;
using System.Collections;

namespace Hell
{
    [RequireComponent(typeof(Pawn))]
    public class AnvilStatic : MonoBehaviour
    {
        public Pawn myPawn;
        public GameObject chockPrefab;
        public int damage;

        public ParticleSystem electricPod;

        void Start()
        {
            myPawn = GetComponent<Pawn>();
//SolveThis 
            Board.s.MoveTimeline.OnMove += UnleashStatic;
            //myPawn.OnMove += UnleashStatic;
        }
        void OnDestroy()
        {
//SolveThis
            Board.s.MoveTimeline.OnMove -= UnleashStatic;
            //myPawn.OnMove -= UnleashStatic;
        }
        public void UnleashStatic(MoveToken token)
        {
            if (myPawn == token.Owner)
            {
                electricPod.Play(true);
                // Coordinate.FromVector3(token.finalPosition)
                //Destroy(Instantiate(cernterStaticPrefab, token.Owner.transform.position, staticPrefab.transform.rotation), 5.0f);

                Coordinate baseCoordinate = Coordinate.FromVector3(token.finalPosition);

                InstantiateParticle(baseCoordinate + Direction.north, token.End);
                InstantiateParticle(baseCoordinate + Direction.west , token.End);
                InstantiateParticle(baseCoordinate + Direction.east , token.End);
                InstantiateParticle(baseCoordinate + Direction.south, token.End);
            }
        }
        public void UnleashStatic(Tile Source, Tile target)
        {
            electricPod.Play(true);
         /*   Destroy(Instantiate(cernterStaticPrefab, target.transform.position, staticPrefab.transform.rotation), 5.0f);
            InstantiateParticle(target.coord + Direction.north);
            InstantiateParticle(target.coord + Direction.west);
            InstantiateParticle(target.coord + Direction.east);
            InstantiateParticle(target.coord + Direction.south);*/
        }

        public IEnumerator ChockTarget(Pawn target, float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(Instantiate(chockPrefab, target.transform.position, chockPrefab.transform.rotation), 1.0f);
            target.WipeLifeBuffer();
        }

        public void InstantiateParticle(Coordinate coordinate, float end)
        {
            if(Board.s.IsInsideBoard(coordinate) && Board.s[coordinate].Pawn!= null)
            {
                Board.s[coordinate].Pawn.currentLife -= damage;
                StartCoroutine(ChockTarget(Board.s[coordinate].Pawn, end));

            }
    /*        if (Board.s.IsInsideBoard(coordinate))
            {
                Destroy(Instantiate(chockPrefab, Board.s[coordinate].transform.position, chockPrefab.transform.rotation), 5.0f);
                if (Board.s[coordinate].Pawn != null)
            }*/
        }
        /*
        public void InstantiateParticle(Coordinate coordinate)
        {
            if (Board.s.IsInsideBoard(coordinate)) {
                Destroy(Instantiate(chockPrefab, Board.s[coordinate].transform.position, chockPrefab.transform.rotation), 5.0f);
                if (Board.s[coordinate].Pawn != null)
                    Board.s[coordinate].Pawn.currentLife -= damage;
            }
        }*/
    }
}