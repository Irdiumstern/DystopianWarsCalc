using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.Enum
{
    public enum DiceResult
    {
        Blank = 1,
        Counter = 2,
        HeavyCounter = 3,
        Hit = 4,
        HeavyHit = 5,
        ExplodingHit = 6
    }

    public static class DiceResultGenerator
    {
        public static IDictionary<DiceResult, int> GetDiceResults(int rolls)
        {
            IDictionary<DiceResult, int> results = new Dictionary<DiceResult, int>();
            results[DiceResult.ExplodingHit] = 0;
            results[DiceResult.HeavyHit] = 0;
            results[DiceResult.Hit] = 0;
            results[DiceResult.HeavyCounter] = 0;
            results[DiceResult.Counter] = 0;
            results[DiceResult.Blank] = 0;

            for (int i = 0; i < rolls; i++)
            {
                DiceResult res = (DiceResult) RandomNumberGenerator.GetInt32(1, 7);
                results[res]++;
            }

            return results;
        }

        public static void GetDiceResults(int rolls, IDictionary<DiceResult, int> results)
        {
            for (int i = 0; i < rolls; i++)
            {
                DiceResult res = (DiceResult)RandomNumberGenerator.GetInt32(1, 7);
                results[res]++;
            }
        }

        public static void ExplodeDiceResults(IDictionary<DiceResult, int> results, IDictionary<DiceResult, int>? newResults)
        {
            int rolls = newResults?[DiceResult.ExplodingHit] ?? results[DiceResult.ExplodingHit];
            if (rolls != 0)
            {
                var newRoll = GetDiceResults(rolls);
                results[DiceResult.ExplodingHit] += newRoll[DiceResult.ExplodingHit];
                results[DiceResult.HeavyHit] += newRoll[DiceResult.HeavyHit];
                results[DiceResult.Hit] += newRoll[DiceResult.Hit];
                results[DiceResult.HeavyCounter] += newRoll[DiceResult.HeavyCounter];
                results[DiceResult.Counter] += newRoll[DiceResult.Counter];
                results[DiceResult.Blank] += newRoll[DiceResult.Blank];

                ExplodeDiceResults(results, newRoll);
            }
        }
    }
}
