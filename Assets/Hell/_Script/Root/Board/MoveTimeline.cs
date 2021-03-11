using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Hell.Display
{
    public class MoveTimeline : MonoBehaviour
    {
        public Action<MoveToken> OnMove = delegate { };

        [HideInInspector]
        public bool IsRunning { get; private set; }

        private List<BoardToken> boardTokens;

        public void AddToken(BoardToken token){
            boardTokens.Add(token);
        }

        private void Initialize(){
            boardTokens = new List<BoardToken>();
        }

        public IEnumerator PlayBoardTimeline()
        {
            IsRunning = true;

            yield return this.StartMultiCoroutine(boardTokens.Select(o => o.PlayCoroutine));

            boardTokens = new List<BoardToken>();

            IsRunning = false;
        }
    }
}
