using DystopianWarsCalc.Data;
using DystopianWarsCalc.Model.DiceRoller;
using DystopianWarsCalc.Model.Enum;
using DystopianWarsCalc.Model.Rules;
using DystopianWarsCalc.Utilities;
using System.Collections.Generic;

namespace DystopianWarsCalc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CalcAttackersAgainstTargets(AttackerBuilder.GetEgyptians_Mandjet(), ModelBuilder.GetTargets());
            CalcAttackersAgainstTargets(AttackerBuilder.GetEgyptians_M1(), ModelBuilder.GetTargets());
            // CalcAttackersAgainstTargets(AttackerBuilder.GetEgyptians_Sobek(), ModelBuilder.GetTargets());

            // CalcBlucher();
            // CalcQualityMultipliers();
            // CalcOptimalTriRailShots();
            // CalcJaegerDamagePointBlank();
            //CalcJaegerDamageClosing();
            //CalcKriegsturm();
            //var files = Directory.GetFiles(Defines.ORBATPath);
            //IList<Orbat> orbats = new List<Orbat>();

            //foreach (var file in files)
            //{
            //    var result = OrbatPDFExtractor.LoadORBAT(file);
            //    orbats.Add(result);
            //}

            //var enlightened = orbats.Where(x => x.FactionName == "Enlightened").FirstOrDefault();

        }

        private static void CalcA5A6()
        {
            DWModel cruiserA5 = new DWModel();
            cruiserA5.Armor = 5;
            cruiserA5.Citadel = 11;
        }

        private static void CalcBlucher()
        {
            DWModel frigateHonneur = ModelBuilder.BuildFrigateToten();
            DWModel cruiserChasseur = ModelBuilder.BuildCruiserBlucher();

            var attackers = AttackerBuilder.Get3Bluchers();
            foreach(var attacker in attackers)
            {
                TestRun test1 = new TestRun(attacker, frigateHonneur, null, 200000);
                test1.Run();
                Console.Write(test1.GetResults());
                TestRun test2 = new TestRun(attacker, cruiserChasseur, null, 200000);
                test2.Run();
                Console.Write(test2.GetResults());
            }
        }

        private static void CalcAttackersAgainstTargets(IList<Attacker> attackers, IList<DWModel> targets)
        {
            foreach (var target in targets)
            {
                foreach (var attacker in attackers)
                {
                    TestRun test1 = new TestRun(attacker, target, null, 200000);
                    test1.Run();
                    Console.Write(test1.GetResults());
                }

            }
        }


        private static void CalcKriegsturm()
        {
            DWModel frigateHonneur = ModelBuilder.BuildFrigateToten();
            DWModel cruiserChasseur = ModelBuilder.BuildCruiserBlucher();

            var attackers = new List<Attacker>();
            attackers.Add(AttackerBuilder.GetKriegsturmUberVoltCombinedClosing());
            attackers.Add(AttackerBuilder.GetKriegsturmUberVoltSplitClosing());

            foreach (var attacker in attackers)
            {
                TestRun test1 = new TestRun(attacker, frigateHonneur, null, 200000);
                test1.Run();
                Console.Write(test1.GetResults());
                TestRun test2 = new TestRun(attacker, cruiserChasseur, null, 200000);
                test2.Run();
                Console.Write(test2.GetResults());
            }
        }
        private static void CalcJaegerDamagePointBlank()
        {
            DWModel frigateHonneur = ModelBuilder.BuildFrigateToten();
            DWModel cruiserChasseur = ModelBuilder.BuildCruiserBlucher();
            DWModel battleshipElector = ModelBuilder.BuildBattleshipElector();

            Attacker combined = AttackerBuilder.Get4JaegersCombinedPointBlank();
            Attacker split = AttackerBuilder.Get4JaegersSplitPointBlank();

            TestRun frigCombined = new TestRun(combined, frigateHonneur);
            TestRun frigSplit = new TestRun(split, frigateHonneur);
            TestRun cruiserCombined = new TestRun(combined, cruiserChasseur);
            TestRun cruiserSplit = new TestRun(split, cruiserChasseur);
            TestRun battleshipCombined = new TestRun(combined, battleshipElector);
            TestRun battleshipSplit = new TestRun(split, battleshipElector);

            frigCombined.Run();
            frigSplit.Run();
            cruiserCombined.Run();
            cruiserSplit.Run();
            battleshipCombined.Run();
            battleshipSplit.Run();

            Console.Write(frigCombined.GetResults());
            Console.Write(frigSplit.GetResults());
            Console.Write(cruiserCombined.GetResults());
            Console.Write(cruiserSplit.GetResults());
            Console.Write(battleshipCombined.GetResults());
            Console.Write(battleshipSplit.GetResults());
        }

        private static void CalcJaegerDamageClosing()
        {
            DWModel frigateHonneur = ModelBuilder.BuildFrigateToten();
            DWModel cruiserChasseur = ModelBuilder.BuildCruiserBlucher();
            DWModel battleshipElector = ModelBuilder.BuildBattleshipElector();

            Attacker combined = AttackerBuilder.Get4JaegersCombinedClosing();
            Attacker combinedPHr = AttackerBuilder.Get4JaegersCombinedClosingPHRudiger();
            Attacker split = AttackerBuilder.Get4JaegersSplitClosing();

            TestRun frigCombined = new TestRun(combined, frigateHonneur);
            TestRun frigCombinedPHr = new TestRun(combinedPHr, frigateHonneur);
            TestRun frigSplit = new TestRun(split, frigateHonneur);
            TestRun cruiserCombined = new TestRun(combined, cruiserChasseur);
            TestRun cruiserCombinedPHr = new TestRun(combinedPHr, cruiserChasseur);
            TestRun cruiserSplit = new TestRun(split, cruiserChasseur);
            TestRun battleshipCombined = new TestRun(combined, battleshipElector);
            TestRun battleshipCombinedPHr = new TestRun(combinedPHr, battleshipElector);
            TestRun battleshipSplit = new TestRun(split, battleshipElector);

            frigCombined.Run();
            frigCombinedPHr.Run();
            frigSplit.Run();
            cruiserCombined.Run();
            cruiserCombinedPHr.Run();
            cruiserSplit.Run();
            battleshipCombined.Run();
            battleshipCombinedPHr.Run();
            battleshipSplit.Run();

            Console.Write(frigCombined.GetResults());
            Console.Write(frigCombinedPHr.GetResults());
            Console.Write(frigSplit.GetResults());
            Console.Write(cruiserCombined.GetResults());
            Console.Write(cruiserCombinedPHr.GetResults());
            Console.Write(cruiserSplit.GetResults());
            Console.Write(battleshipCombined.GetResults());
            Console.Write(battleshipCombinedPHr.GetResults());
            Console.Write(battleshipSplit.GetResults());
        }


        private static void CalcOptimalTriRailShots()
        {
            IList<Attacker> attackers = new List<Attacker>();
            attackers.Add(AttackerBuilder.GetTriRail8Singles());
            attackers.Add(AttackerBuilder.GetTriRail4Doubles());
            attackers.Add(AttackerBuilder.GetTriRailOcta());
            CalcAttackersAgainstTargets(attackers, ModelBuilder.GetTargets());
        }

        private static void CalcQualityMultipliers()
        {
            int dicePool = 50;
            Attacker normal = new Attacker(new List<int> { dicePool }, false, false, false);
            Attacker dev = new Attacker(new List<int> { dicePool }, false, false, true);
            Attacker blanks = new Attacker(new List<int> { dicePool }, false, false, false);
            blanks.Attacks[0].RerollBlanks = true;
            Attacker fusilade = new Attacker(new List<int> { dicePool }, true, false, false);
            Attacker sustained = new Attacker(new List<int> { dicePool }, false, true, false);
            Attacker sustainedFusilade = new Attacker(new List<int> { dicePool }, true, true, false);
            Attacker blanksSustained10 = new Attacker(new List<int> { 10 }, false, true, false);
            blanksSustained10.Attacks[0].RerollBlanks = true;
            Attacker blanksSustained50 = new Attacker(new List<int> { 50 }, false, true, false);
            blanksSustained50.Attacks[0].RerollBlanks = true;

            TestRun trNormal = new TestRun(normal, null);
            TestRun trDev = new TestRun(dev, null);
            TestRun trBlanks = new TestRun(blanks, null);
            TestRun trFusilade = new TestRun(fusilade, null);
            TestRun trSustained = new TestRun(sustained, null);
            TestRun trSustainedFusilade = new TestRun(sustainedFusilade, null);
            TestRun trBlanksSustained10 = new TestRun(blanksSustained10, null);
            TestRun trBlanksSustained50 = new TestRun(blanksSustained50, null);


            trNormal.Run();
            // trDev.Run();
            trBlanks.Run();
            // trSustained.Run();
            // trFusilade.Run();
            // trSustainedFusilade.Run();
            // trBlanksSustained10.Run();
            // trBlanksSustained50.Run();

            int x = 0;
        }
    }
}