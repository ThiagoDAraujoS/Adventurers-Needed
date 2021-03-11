using System;
using UnityEngine;

namespace Hell.Rune
{
    public class TurnTimer : MonoBehaviour
    {
        public TimeFrame timeframe = TimeFrame.act;

        bool isStart;

        Character character;

        float destructionDelay;

        int duration;

        public virtual void Effect() { }

        private void Wrapper(Character character) {
            if (this.character == character)
                Subtract();
        }

        public void Subtract() {
            if (--duration <= 0) {
                End();
            }
        }

        public void End() {
            Effect();
            Unsubscribe();
            Destroy(gameObject, destructionDelay);
        }

        public void Initialize(bool isStart, Character character, int duration, float destructionDelay) {
            this.destructionDelay = destructionDelay;
            this.isStart = isStart;
            this.duration = duration;
            this.character = character;
            Subscribe();
        }

        private void Subscribe() {
            character.OnDeath += End;

            switch (timeframe)
            {
                case TimeFrame.turn:
                    if (isStart)
                        RoomManager.s.TurnEngine.Timeline.StartingTurn += Subtract;
                    else
                        RoomManager.s.TurnEngine.Timeline.EndingTurn += Subtract;
                    break;

                case TimeFrame.act:
                    if(isStart)                              
                        RoomManager.s.TurnEngine.Timeline.StartingPlayerAct += Wrapper;
                    else                                                                
                        RoomManager.s.TurnEngine.Timeline.EndingPlayerAct += Wrapper;
                    break;

                case TimeFrame.phase:
                    if (isStart)
                        RoomManager.s.TurnEngine.Timeline.StartingPhase += Subtract;
                    else
                        RoomManager.s.TurnEngine.Timeline.EndingPhase += Subtract;
                    break;
            }
        }

        private void Unsubscribe() {
            character.OnDeath -= End;

            switch (timeframe)
            {
                case TimeFrame.turn:
                    if (isStart)
                        RoomManager.s.TurnEngine.Timeline.StartingTurn -= Subtract;
                    else
                        RoomManager.s.TurnEngine.Timeline.EndingTurn -= Subtract;
                    break;

                case TimeFrame.act:
                    if (isStart)
                        RoomManager.s.TurnEngine.Timeline.StartingPlayerAct -= Wrapper;
                    else
                        RoomManager.s.TurnEngine.Timeline.EndingPlayerAct -= Wrapper;
                    break;

                case TimeFrame.phase:
                    if (isStart)
                        RoomManager.s.TurnEngine.Timeline.StartingPhase -= Subtract;
                    else
                        RoomManager.s.TurnEngine.Timeline.EndingPhase -= Subtract;
                    break;
            }


/*
            if (isStart)
                RoomManager.s.TurnEngine.Timeline.StartingPlayerAct -= Wrapper;
            else    
                RoomManager.s.TurnEngine.Timeline.EndingPlayerAct -= Wrapper;*/
        }
    }
}
