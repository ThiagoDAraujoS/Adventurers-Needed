using UnityEngine;
using System.Collections.Generic;
using System;
using Hell;

namespace Hell.Display
{
    public static class PlanGen
    {
        public static Plan BuildPlan(Character character)
        {
            Plan result = new Plan(character);

            do result.AddAction(CreateActionMessage(character, character.Runes, Timeline.AP_LIMIT - result.PlanSize));
            while (result.PlanSize < Timeline.AP_LIMIT);

            return result;
        }

        private static ActionToken CreateActionMessage(Character character, List<PlayerAction> runes, int apLimit)
        {
            int runeId;

            do runeId = UnityEngine.Random.Range(0, runes.Count + 2);
            while (runeId < runes.Count && apLimit < runes[runeId].Count);

            int size = 1;
            if (runeId < runes.Count && runes[runeId] != null)
                size = runes[runeId].Count;

            ActionToken result = new ActionToken(
                character,
                runeId,
                (Direction)UnityEngine.Random.Range(0, 4));

            return result;
        }

        public static Plan CreateWalk(Character c)
        {
            Plan result = new Plan(c);
            result.AddAction(c.Runes.Count, Direction.east);
            result.AddAction(c.Runes.Count, Direction.east);
            result.AddAction(c.Runes.Count, Direction.east);
            result.AddAction(c.Runes.Count, Direction.east);
            return result;
        }
    }
}