using DystopianWarsCalc.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.DiceRoller
{
    public class TestRun
    {
        public TestRun(Attacker attacker, DWModel target, string name = null, int runs = 500000)
        {
            this.attacker = attacker;
            this.target = target;
            if (string.IsNullOrEmpty(name))
            {
                this.Name = attacker.Name + " vs. " + target.Name;
            }
            else
            {
                this.Name = name;
            }

            Runs = runs;
        }

        public string Name { get; set; }

        public Attacker attacker { get; set; }
        public DWModel target { get; set; }

        public int Runs { get; set; }

        public double AverageHitsPerDice { get; set; }
        public double AverageHits { get; set; }
        public double AverageDamage { get; set; }
        public double AveragePointPerDamage { get; set; }
        public double AverageCrits { get; set; }
        public double AverageDisorder { get; set; }
        public double AverageCitadelBreached { get; set; }

        public IList<SalvoResult> Results = new List<SalvoResult>();

        public override string ToString()
        {
            return this.AverageHitsPerDice.ToString();
        }

        public string GetResults()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------");
            sb.AppendLine(Name);
            sb.AppendLine("-------------------------------------");
            sb.AppendLine("Avg Dmg: " + this.AverageDamage);
            sb.AppendLine("Avg Point/Damage:" + this.AveragePointPerDamage);
            sb.AppendLine("Avg Crits: " + this.AverageCrits);
            sb.AppendLine("Avg Disorder: " + this.AverageDisorder);
            sb.AppendLine("Avg Hits: " + this.AverageHits);
            // sb.AppendLine("Avg Hits/Dice: " + this.AverageHitsPerDice);
            sb.AppendLine("Avg Citadel Breached: " + this.AverageCitadelBreached);

            IDictionary<int, int> resultsAtDamageAmount = new Dictionary<int, int>();
            foreach (var res in this.Results)
            {
                if (!resultsAtDamageAmount.ContainsKey(res.TotalDamageDone))
                {
                    resultsAtDamageAmount.Add(res.TotalDamageDone, 1);
                }
                else
                {
                    resultsAtDamageAmount[res.TotalDamageDone]++;
                }
            }

            sb.AppendLine();
            sb.AppendLine("Damage Distribution (Chance of given Damage or higher)");
            IDictionary<int, double> percentageAtDamageAmount = new Dictionary<int, double>();
            foreach (var resDmg in resultsAtDamageAmount.ToList().OrderBy(x => x.Key))
            {
                double percentage = ((double)resDmg.Value / (double)this.Results.Count) * 100;
                percentageAtDamageAmount[resDmg.Key] = percentage;
                // sb.AppendLine(resDmg.Key + ": " + percentage);
            }
            IDictionary<int, double> percentageOrHigherAtDamageAmount = new Dictionary<int, double>(percentageAtDamageAmount);

            double percentageOrHigher = 0;
            for (int i = percentageOrHigherAtDamageAmount.Keys.Last(); i >= percentageOrHigherAtDamageAmount.Keys.First(); i--)
            {
                if (percentageOrHigherAtDamageAmount.ContainsKey(i))
                {
                    percentageOrHigher += percentageOrHigherAtDamageAmount[i];
                    percentageOrHigherAtDamageAmount[i] = percentageOrHigher;
                }
            }
            foreach (var resDmg in percentageOrHigherAtDamageAmount.ToList())
            {
                sb.AppendLine(resDmg.Key + ": " + resDmg.Value + "%");
            }

            sb.AppendLine();
            var result = sb.ToString();
            return result;
        }

        public void Run()
        {
            for (int i = 0; i < Runs; i++)
            {
                var salvoResult = new SalvoResult();
                this.Results.Add(salvoResult);
                foreach (var attack in this.attacker.Attacks)
                {
                    foreach (var dicePool in attack.DicePools)
                    {
                        int finalDicePool = dicePool;
                        if (attack.Rail && target != null)
                        {
                            if (target.PositionTrait == PositionTrait.Skimming_Unit || target.PositionTrait == PositionTrait.Aerial_Unit)
                            {
                                finalDicePool += Math.Min(attack.ContributingWeapons, 3);
                            }
                            else if (target.PositionTrait == PositionTrait.Submerged_Unit)
                            {
                                finalDicePool -= Math.Min(attack.ContributingWeapons, 3);
                            }
                        }

                        if (target.ShieldGenerator && !attack.Arc && !attack.Bomb && !attack.Submerged)
                        {
                            finalDicePool -= target.Shield;
                        }
                        else if (!target.ShieldGenerator && target.Shield > 0)
                        {
                            finalDicePool -= target.Shield;
                        }

                        if (attack.AllowIntercept)
                        {
                            int interceptDice = target.AerialDefence;

                            var interceptRolls = DiceResultGenerator.GetDiceResults(interceptDice);
                            decimal interceptResults = interceptRolls[DiceResult.Counter] + interceptRolls[DiceResult.HeavyCounter];
                            int interceptedTargets = Convert.ToInt32(Math.Floor(interceptResults / 3));
                            finalDicePool = finalDicePool - interceptedTargets*attack.ShotsPerSRS;
                        }

                        var rolls = DiceResultGenerator.GetDiceResults(finalDicePool);
                        int rerollDice = 0;
                        if (attack.Fusilade)
                        {
                            rerollDice += rolls[DiceResult.Counter];
                            rerollDice += rolls[DiceResult.HeavyCounter];
                            if (attack.Sustained || attack.RerollBlanks)
                            {
                                rerollDice += rolls[DiceResult.Blank];
                            }
                        }
                        else if (attack.RerollBlanks)
                        {
                            rerollDice += rolls[DiceResult.Blank];
                            if (attack.Sustained)
                            {
                                rerollDice += rolls.Where(x => x.Key == DiceResult.Counter || x.Key == DiceResult.HeavyCounter).Select(x => x.Value).Max();
                            }
                        }
                        else if (attack.Sustained)
                        {
                            var counter = rolls[DiceResult.Counter];
                            var heavyCounter = rolls[DiceResult.HeavyCounter];
                            var blank = rolls[DiceResult.Blank];
                            var maxTrad = Math.Max(counter, Math.Max(blank, heavyCounter));
                            // var linq = rolls.Where(x => x.Key == DiceResult.Counter || x.Key == DiceResult.HeavyCounter || x.Key == DiceResult.Blank).Select(x => x.Value).Max();
                            // if (maxTrad != linq)
                            // {
                            //     int asd = 0;
                            // }

                            rerollDice += maxTrad;
                        }

                        DiceResultGenerator.GetDiceResults(rerollDice, rolls);

                        if (!attack.Homing && !attack.Magnetic && !attack.Torpedo && ((target?.Obscured ?? true) || (attack.Gunnery && target.IsMass1) || attack.Indirect))
                        {
                            // No Explosions for you
                            int asdfads = 0;
                        }
                        else
                        {
                            DiceResultGenerator.ExplodeDiceResults(rolls, null);
                        }

                        int hits = rolls[DiceResult.Hit] + rolls[DiceResult.HeavyHit] * 2 + rolls[DiceResult.ExplodingHit] * (attack.Devastating ? 3 : 2);

                        int defenceDice = 0;
                        if (attack.Aerial)
                        {
                            defenceDice = target.AerialDefence;
                        }
                        else if (attack.Submerged)
                        {
                            defenceDice = target.SubmergedDefence;
                        }
                        else if (target.LuminiferousDefences && (attack.Gunnery || attack.Fusilade || attack.Broadside))
                        {
                            defenceDice = target.AerialDefenceCrippled;
                        }

                        var defenceRolls = DiceResultGenerator.GetDiceResults(defenceDice);
                        int defenceResults = defenceRolls[DiceResult.Counter] + defenceRolls[DiceResult.HeavyCounter] * (attack.HighVelocity ? 1 : 2 );

                        var result = new DicePoolResult { Hits = hits, InitialDicePool = dicePool, Rerolls = rerollDice, Result = rolls, Defences = defenceResults, FinalHits = (hits - defenceResults) };
                        salvoResult.Results.Add(result);

                        // Finished getting hits, now figuring out how much damage is done
                        if (target != null)
                        {
                            result.MaxDamage = target.Hull + target.HullCrippled;
                            result.BaseDamageDone = (int)Math.Floor((double)((hits - defenceResults) / target.Armor));
                            result.CitadelBreached = (hits - defenceResults) >= (target.Citadel - (attack.Rail ? 1 : 0) - (attack.Piercing && target.IsMass1 ? 3 : 0));
                            if (target.IsMass1 && result.CitadelBreached)
                            {
                                result.BaseDamageDone = target.Hull;
                                result.CatastrophicExplosion = false;
                            }
                            else
                            {
                                result.CatastrophicExplosion = target.IgnoreCatastrophic ? false : (hits - defenceResults) >= 2 * (target.Citadel - (attack.Rail ? 1 : 0));
                                if (result.CatastrophicExplosion)
                                {
                                    result.DisorderDone++;
                                }
                                if (result.CitadelBreached)
                                {
                                    result.CriticalDamageDone++;
                                }
                            }

                            if (attack.Hazardous && result.BaseDamageDone > 0)
                            {
                                result.DisorderDone++;
                            }
                            if (attack.Arc && result.CitadelBreached)
                            {
                                result.DisorderDone++;
                            }
                            if (attack.Voltaic && result.CitadelBreached)
                            {
                                result.DisorderDone++;
                            }
                            if (attack.Piercing && result.BaseDamageDone > 0)
                            {
                                result.CriticalDamageDone++;
                            }
                            if (attack.Magnetic && result.BaseDamageDone > 0)
                            {
                                result.CriticalDamageDone++;
                            }
                        }
                    }
                }
            }

            this.AverageHitsPerDice = this.Results.Select(x => x.AverageHitsPerDice).Sum() / this.Runs;
            this.AverageHits = this.Results.Select(x => x.AverageHits).Sum() / this.Runs;
            this.AverageDamage = this.Results.Select(x => (double)x.TotalDamageDone).Sum() / (double)this.Runs;
            this.AverageCrits = this.Results.Select(x => (double)x.TotalCritsDone).Sum() / (double)this.Runs;
            this.AverageDisorder = this.Results.Select(x => (double)x.TotalDisorderDone).Sum() / (double)this.Runs;
            this.AveragePointPerDamage =  (double)this.attacker.PointCost / (double)this.AverageDamage;
            this.AverageCitadelBreached = (double)this.Results.Select(x => x.TotalCitadelBreached).Sum() / (double)this.Runs;
        }
    }
}
