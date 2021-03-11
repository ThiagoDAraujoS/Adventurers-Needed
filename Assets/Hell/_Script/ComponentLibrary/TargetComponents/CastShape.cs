using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Hell.Display;

namespace Hell.Rune.Target
{
    public class Random : AreaOfEffect
    {
        [SerializeField]
        public int range;

        [SerializeField]
        public int amount;

        [SerializeField]
        public bool canRepeatTarget;

        private Coordinate GetRandomCoordinate(Coordinate center)
        {
            Vector2 vector2 = UnityEngine.Random.insideUnitCircle * range;
            return new Coordinate(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y))+center;
        }

        public override List<Tile> GetTiles(Token token, Coordinate center, Direction dir)
        {
            List<Tile> result = base.GetTiles(token, center, dir);

            result = (canRepeatTarget) ? GetRandom(result): ExtractRandom(result);

            return result;
        }

        private List<Tile> ExtractRandom(List<Tile> possibilities)
        {
            List<Tile> result = new List<Tile>();
            Tile candidate;
            for (int i = 0; i < amount && possibilities.Count > 0; i++)
            {
                candidate = possibilities.Random();
                possibilities.Remove(candidate);
                result.Add(candidate);
            }
            return result;
        }

        private List<Tile> GetRandom(List<Tile> possibilities)
        {
            List<Tile> result = new List<Tile>();
            for (int i = 0; i < amount && possibilities.Count > 0; i++)
                result.Add(possibilities.Random());
            return result;
        }
    }
}