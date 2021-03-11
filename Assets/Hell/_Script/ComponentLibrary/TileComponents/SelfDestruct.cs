using UnityEngine;
using System.Collections;
using Hell.Display;
using System;

namespace Hell.Rune
{
    public class SelfDestruct : TokenDrivenBehaviour<MasterTile>, IRule<TileToken>
    {
        float safetyDelay;
        public void Resolution(TileToken token) {
            token.Tile.effects.Remove(Master);
            Destroy(gameObject, safetyDelay);
        }
    }
}
