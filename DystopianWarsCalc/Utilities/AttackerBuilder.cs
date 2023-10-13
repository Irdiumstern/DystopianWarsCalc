using DystopianWarsCalc.Model.DiceRoller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Utilities
{
    internal static class AttackerBuilder
    {
        internal static Attacker GetTriRail8Singles()
        {
            Attacker attacker = new Attacker(new List<int> { 8, 8, 8, 8, 8, 8, 8, 8 }, false, false, false, true, true);
            attacker.PointCost = (100 + 2 * 5) * 4;
            attacker.Attacks[0].ContributingWeapons = 1;
            attacker.Name = "8x1 TriRails";
            return attacker;
        }

        internal static Attacker GetTriRailOcta()
        {
            Attacker attacker = new Attacker(new List<int> { 8 + 3 * 7 }, false, false, false, true, true);
            attacker.PointCost = (100 + 2 * 5) * 4;
            attacker.Attacks[0].ContributingWeapons = 8;
            attacker.Name = "8 TriRails Combined";
            return attacker;
        }
        internal static Attacker GetTriRail4Doubles()
        {
            Attacker attacker = new Attacker(new List<int> { 11, 11, 11, 11 }, false, false, false, true, true);
            attacker.Attacks[0].ContributingWeapons = 2;
            attacker.PointCost = (100 + 2 * 5) * 4;
            attacker.Name = "4x2 TriRails";
            return attacker;
        }

        internal static Attacker Get4JaegersCombinedPointBlank()
        {
            Attacker attacker = new Attacker();
            Attack sturm = new Attack();
            sturm.Gunnery = true;
            sturm.Devastating = true;
            sturm.Arc = true;
            sturm.DicePools.Add(22);
            attacker.Attacks.Add(sturm);
            Attack autocannons = new Attack();
            autocannons.Gunnery = true;
            autocannons.Sustained = true;
            autocannons.Voltaic = true;
            autocannons.DicePools.Add((4 + 3 * 7 + 4));
            attacker.Attacks.Add(autocannons);
            attacker.Name = "4 Jaegers Combined (PackHunter Rudigers) Point Blank";
            attacker.PointCost = 4 * 41;
            return attacker;
        }

        internal static Attacker Get4JaegersSplitPointBlank()
        {
            Attacker attacker = new Attacker();
            Attack sturm = new Attack();
            sturm.Gunnery = true;
            sturm.Devastating = true;
            sturm.Arc = true;
            sturm.DicePools.Add(12);
            sturm.DicePools.Add(12);
            attacker.Attacks.Add(sturm);
            Attack autocannons = new Attack();
            autocannons.Gunnery = true;
            autocannons.Sustained = true;
            autocannons.Voltaic = true;
            autocannons.DicePools.Add(11);
            autocannons.DicePools.Add(10);
            autocannons.DicePools.Add(10);
            attacker.Attacks.Add(autocannons);
            attacker.Name = "4 Jaegers 2x2 (PackHunter Rudigers) Point Blank";
            attacker.PointCost = 4 * 41;
            return attacker;
        }

        internal static Attacker Get4JaegersCombinedClosing()
        {
            Attacker attacker = new Attacker();
            Attack sturm = new Attack();
            sturm.Gunnery = true;
            sturm.Devastating = true;
            sturm.Arc = true;
            sturm.DicePools.Add((5 + 3 * 3 + 4));
            attacker.Attacks.Add(sturm);
            Attack autocannons = new Attack();
            autocannons.Gunnery = true;
            autocannons.Sustained = true;
            autocannons.Voltaic = true;
            autocannons.DicePools.Add((3 + 7));
            attacker.Attacks.Add(autocannons);
            attacker.Name = "4 Jaegers Combined (PackHunter Sturm) Closing";
            attacker.PointCost = 4 * 41;
            return attacker;
        }

        internal static Attacker Get4JaegersCombinedClosingPHRudiger()
        {
            Attacker attacker = new Attacker();
            Attack sturm = new Attack();
            sturm.Gunnery = true;
            sturm.Devastating = true;
            sturm.Arc = true;
            sturm.DicePools.Add((5 + 3 * 3));
            attacker.Attacks.Add(sturm);
            Attack autocannons = new Attack();
            autocannons.Gunnery = true;
            autocannons.Sustained = true;
            autocannons.Voltaic = true;
            autocannons.DicePools.Add((3 + 7 + 4));
            attacker.Attacks.Add(autocannons);
            attacker.Name = "4 Jaegers Combined (PackHunter Rudiger) Closing";
            attacker.PointCost = 4 * 41;
            return attacker;
        }
        internal static Attacker Get4JaegersSplitClosing()
        {
            Attacker attacker = new Attacker();
            Attack sturm = new Attack();
            sturm.Gunnery = true;
            sturm.Devastating = true;
            sturm.Arc = true;
            sturm.DicePools.Add(8);
            sturm.DicePools.Add(8);
            attacker.Attacks.Add(sturm);
            Attack autocannons = new Attack();
            autocannons.Gunnery = true;
            autocannons.Sustained = true;
            autocannons.Voltaic = true;
            autocannons.DicePools.Add(14);
            attacker.Attacks.Add(autocannons);
            attacker.Name = "4 Jaegers 2x2 (PackHunter Rudigers) Closing";
            attacker.PointCost = 4 * 41;
            return attacker;
        }

        internal static Attacker GetKriegsturmUberVoltSplitClosing()
        {
            Attacker attacker = new Attacker();
            Attack uberVolt = new Attack();
            uberVolt.Sustained = true;
            uberVolt.Voltaic = true;
            uberVolt.DicePools.Add(15);
            uberVolt.DicePools.Add(15);
            attacker.Attacks.Add(uberVolt);

            attacker.Name = "2 Kriegsturm Split Uber Volt Closing";
            attacker.PointCost = 120 * 2;
            return attacker;
        }

        internal static Attacker GetKriegsturmUberVoltCombinedClosing()
        {
            Attacker attacker = new Attacker();
            Attack uberVolt = new Attack();
            uberVolt.Sustained = true;
            uberVolt.Voltaic = true;
            uberVolt.DicePools.Add(21);
            attacker.Attacks.Add(uberVolt);

            attacker.Name = "2 Kriegsturm Combined Uber Volt Closing";
            attacker.PointCost = 120 * 2;
            return attacker;
        }

        internal static IList<Attacker> Get3Bluchers()
        {
            IList<Attacker> attackers = new List<Attacker>();
            Attacker solo = new Attacker();
            Attack gb = new Attack();
            gb.Voltaic = true;
            gb.Gunnery = true;
            gb.DicePools.Add(8);
            gb.DicePools.Add(8);
            gb.DicePools.Add(8);
            gb.DicePools.Add(8);
            gb.DicePools.Add(8);
            gb.DicePools.Add(8);
            solo.Attacks.Add(gb);
            solo.Name = "3 Volt Gun Battery Bluchers Separate Closing";
            solo.PointCost = 300;
            attackers.Add(solo);

            Attacker duo = new Attacker();
            Attack gb2 = new Attack();
            gb2.Voltaic = true;
            gb2.Gunnery = true;
            gb2.DicePools.Add(11);
            gb2.DicePools.Add(11);
            gb2.DicePools.Add(11);
            duo.Attacks.Add(gb2);
            duo.Name = "3 Volt Gun Battery Bluchers 2x2x2 Closing";
            duo.PointCost = 300;
            attackers.Add(duo);

            Attacker trio = new Attacker();
            Attack gb3 = new Attack();
            gb3.Voltaic = true;
            gb3.Gunnery = true;
            gb3.DicePools.Add(14);
            gb3.DicePools.Add(14);
            trio.Attacks.Add(gb3);
            trio.Name = "3 Volt Gun Battery Bluchers 3x3 Closing";
            trio.PointCost = 300;
            attackers.Add(trio);

            Attacker full = new Attacker();
            Attack gb6 = new Attack();
            gb6.Voltaic = true;
            gb6.Gunnery = true;
            gb6.DicePools.Add((8 + 3 * 5));
            full.Attacks.Add(gb6);
            full.Name = "3 Volt Gun Battery Bluchers Together Closing";
            full.PointCost = 300;
            attackers.Add(full);

            Attacker soloRockets = new Attacker();
            Attack rockets = new Attack();
            rockets.Voltaic = true;
            rockets.Aerial = true;
            rockets.DicePools.Add(8);
            rockets.DicePools.Add(8);
            rockets.DicePools.Add(8);
            rockets.DicePools.Add(8);
            rockets.DicePools.Add(8);
            rockets.DicePools.Add(8);
            soloRockets.Attacks.Add(rockets);
            soloRockets.Name = "3 Shock Rocket Bluchers Separate Closing";
            soloRockets.PointCost = 106 * 3;
            attackers.Add(soloRockets);

            Attacker duoRockets = new Attacker();
            Attack rockets2 = new Attack();
            rockets2.Voltaic = true;
            rockets2.Aerial = true;
            rockets2.DicePools.Add(12);
            rockets2.DicePools.Add(12);
            duoRockets.Attacks.Add(rockets2);
            duoRockets.Name = "3 Shock Rocket Bluchers 2x2x2 Closing";
            duoRockets.PointCost = 106 * 3;
            attackers.Add(duoRockets);

            Attacker trioRockets = new Attacker();
            Attack rockets3 = new Attack();
            rockets3.Voltaic = true;
            rockets3.Aerial = true;
            rockets3.DicePools.Add(16);
            rockets3.DicePools.Add(16);
            trioRockets.Attacks.Add(rockets3);
            trioRockets.Name = "3 Shock Rocket Bluchers 3x3 Closing";
            trioRockets.PointCost = 106 * 3;
            attackers.Add(trioRockets);

            Attacker fullRockets = new Attacker();
            Attack rockets6 = new Attack();
            rockets6.Voltaic = true;
            rockets6.Aerial = true;
            rockets6.DicePools.Add((8 + 4 * 5));
            fullRockets.Attacks.Add(rockets6);
            fullRockets.Name = "3 Shock Rocket Bluchers Together Closing";
            fullRockets.PointCost = 300;
            attackers.Add(fullRockets);

            return attackers;
        }

        internal static IList<Attacker> GetChasseurs()
        {
            IList<Attacker> attackers = new List<Attacker>();
            Attacker atk = new Attacker();
            Attack gb = new Attack();
            gb.Gunnery = true;
            gb.DicePools.Add(9 + 2 * 4 + 3 * 9);
            atk.Attacks.Add(gb);
            atk.Name = "3 Gun Chasseurs Combined Closing";
            atk.PointCost = (123 + 5) * 3;
            attackers.Add(atk);
            atk = null;
            gb = null;

            atk = new Attacker();
            gb = new Attack();
            gb.Gunnery = true;
            gb.DicePools.Add(9 + 3 * 3);
            gb.DicePools.Add(9 + 3 * 3);
            gb.DicePools.Add(9 + 3 * 3);
            atk.Name = "3 Gun Chasseurs 18x3 Closing";
            atk.PointCost = (123 + 5) * 3;
            atk.Attacks.Add(gb);
            attackers.Add(atk);
            atk = null;
            gb = null;

            //atk = new Attacker();
            //gb = new Attack();
            //gb.Gunnery = true;
            //gb.DicePools.Add(9 + 3 * 2);
            //gb.DicePools.Add(9 + 3 * 2);
            //gb.DicePools.Add(9 + 3 * 2);
            //gb.DicePools.Add(5 + 3 * 2);
            //atk.Name = "3 Gun Chasseurs 15x3 11 Closing";
            //atk.PointCost = (123 + 5) * 3;
            //atk.Attacks.Add(gb);
            //attackers.Add(atk);
            //atk = null;
            //gb = null;

            //atk = new Attacker();
            //gb = new Attack();
            //gb.Gunnery = true;
            //gb.DicePools.Add(9 + 3);
            //gb.DicePools.Add(9 + 3);
            //gb.DicePools.Add(9 + 3);
            //gb.DicePools.Add(5 + 3);
            //gb.DicePools.Add(5 + 3);
            //gb.DicePools.Add(5 + 3);
            //atk.Name = "3 Gun Chasseurs 12x3 8x3 Closing";
            //atk.PointCost = (123 + 5) * 3;
            //atk.Attacks.Add(gb);
            //attackers.Add(atk);
            //atk = null;
            //gb = null;

            //atk = new Attacker();
            //gb = new Attack();
            //gb.Gunnery = true;
            //gb.DicePools.Add(9);
            //gb.DicePools.Add(9);
            //gb.DicePools.Add(9);
            //gb.DicePools.Add(5 + 3 * 2);
            //gb.DicePools.Add(5 + 3);
            //gb.DicePools.Add(5 + 3);
            //gb.DicePools.Add(5 + 3);
            //atk.Name = "3 Gun Chasseurs 9x3 11 8x3 Closing";
            //atk.PointCost = (123 + 5) * 3;
            //atk.Attacks.Add(gb);
            //attackers.Add(atk);
            //atk = null;
            //gb = null;

            atk = new Attacker();
            gb = new Attack();
            gb.Gunnery = true;
            gb.Sustained = true;
            gb.Devastating = true;
            gb.Hazardous = true;
            gb.DicePools.Add(5 + 3 * 8);
            atk.Name = "3 Lancette/Solex Chasseurs Combined Closing";
            atk.PointCost = (123 - 5 + 7 * 3) * 3;
            atk.Attacks.Add(gb);
            attackers.Add(atk);
            atk = null;
            gb = null;

            atk = new Attacker();
            gb = new Attack();
            gb.Gunnery = true;
            gb.Sustained = true;
            gb.Devastating = true;
            gb.Hazardous = true;
            gb.DicePools.Add(5 + 3 * 11);
            atk.Name = "3 Lancette/Solex Chasseurs Combined Valour Closing";
            atk.PointCost = (123 - 5 + 7 * 3) * 3;
            atk.Attacks.Add(gb);
            attackers.Add(atk);
            atk = null;
            gb = null;

            atk = new Attacker();
            gb = new Attack();
            gb.Gunnery = true;
            gb.Sustained = true;
            gb.Devastating = true;
            gb.Hazardous = true;
            gb.DicePools.Add(5 + 3 * 2);
            gb.DicePools.Add(5 + 3 * 2);
            gb.DicePools.Add(5 + 3 * 2);
            atk.Name = "3 Lancette/Solex Chasseurs 3 Lancettes  Closing";
            atk.PointCost = (123 - 5 + 7 * 3) * 3;
            atk.Attacks.Add(gb);
            attackers.Add(atk);
            atk = null;
            gb = null;

            atk = new Attacker();
            gb = new Attack();
            gb.Gunnery = true;
            gb.Sustained = true;
            gb.Devastating = true;
            gb.Hazardous = true;
            gb.DicePools.Add(5 + 3 * 2);
            gb.DicePools.Add(5 + 3);
            gb.DicePools.Add(5 + 3);
            gb.DicePools.Add(5 + 3);
            atk.Name = "3 Lancette/Solex Chasseurs 3 2 2 2 Lancettes Closing";
            atk.PointCost = (123 - 5 + 7 * 3) * 3;
            atk.Attacks.Add(gb);
            attackers.Add(atk);
            atk = null;
            gb = null;

            atk = new Attacker();
            gb = new Attack();
            gb.Gunnery = true;
            gb.Sustained = true;
            gb.Devastating = true;
            gb.Hazardous = true;
            gb.DicePools.Add(5 + 3);
            gb.DicePools.Add(5 + 3);
            gb.DicePools.Add(5 + 3);
            gb.DicePools.Add(5 + 3);
            gb.DicePools.Add(5 + 3);
            gb.DicePools.Add(5 + 3);
            atk.Name = "3 Lancette/Solex Chasseurs 2 Lancettes Valour Closing";
            atk.PointCost = (123 - 5 + 7 * 3) * 3;
            atk.Attacks.Add(gb);
            attackers.Add(atk);
            atk = null;
            gb = null;
            return attackers;
        }

        internal static IList<Attacker> GetEgyptians_Mandjet()
        {
            IList<Attacker> attackers = new List<Attacker>();
            Attacker attacker;
            Attack att;
            attacker = new Attacker();
            attacker.Name = "Mandjet x3 Heavy Broadside Combined Beamer + Lances Combined Point Blank";
            attacker.PointCost = (138 + 2 * 5 + 7) * 3;
            att = new Attack();
            att.Broadside = true;
            att.Fusilade = true;
            att.DicePools.Add(10 + 2 * 5);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.RerollBlanks = true;
            att.Sustained = true;
            att.DicePools.Add(2 + 7 + 5 * 2 + 3 * 6);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mandjet x3 Heavy Broadside Split Beamer + Lances Combined Point Blank";
            attacker.PointCost = (138 + 2 * 5 + 7) * 3;
            att = new Attack();
            att.Broadside = true;
            att.Fusilade = true;
            att.DicePools.Add(10);
            att.DicePools.Add(10);
            att.DicePools.Add(10);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.RerollBlanks = true;
            att.Sustained = true;
            att.DicePools.Add(2 + 7 + 5 * 2 + 3 * 6);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mandjet x3 Heavy Broadside Split All Guns Combined Point Blank";
            attacker.PointCost = (138) * 3;
            att = new Attack();
            att.Broadside = true;
            att.Fusilade = true;
            att.DicePools.Add(10);
            att.DicePools.Add(10);
            att.DicePools.Add(10);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.RerollBlanks = true;
            att.DicePools.Add(2 + 6 + 3 * 2 + 2 * 6);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mandjet x3 Mortars Separate All Guns Combined";
            attacker.PointCost = (138) * 3;
            att = new Attack();
            att.Aerial = true;
            att.Magnetic = true;
            att.DicePools.Add(8);
            att.DicePools.Add(8);
            att.DicePools.Add(8);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.RerollBlanks = true;
            att.DicePools.Add(2 + 9 + 4 * 2 + 3 * 6);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mandjet x3 All Guns Combined";
            attacker.PointCost = (138) * 3;
            att = new Attack();
            att.Aerial = true;
            att.Magnetic = true;
            att.DicePools.Add(8 + 2 * 4);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.RerollBlanks = true;
            att.DicePools.Add(2 + 9 + 4 * 2 + 3 * 6);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mandjet x3 Beamer + Lances Combined Closing";
            attacker.PointCost = (138 + 2 * 5 + 7) * 3;
            att = new Attack();
            att.Aerial = true;
            att.Magnetic = true;
            att.DicePools.Add(8 + 2 * 4);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.RerollBlanks = true;
            att.Sustained = true;
            att.DicePools.Add(2 + 6 + 4 * 2 + 3 * 6);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mandjet x3 Guns Split per boat";
            attacker.PointCost = (138) * 3;
            att = new Attack();
            att.Aerial = true;
            att.Magnetic = true;
            att.DicePools.Add(8 + 2 * 4);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.RerollBlanks = true;
            att.DicePools.Add(2 + 9 + 3 * 2);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(9 + 3 * 2);
            att.DicePools.Add(9 + 3 * 2);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mandjet x3 DCannons Indirect All Guns Combined Closing";
            attacker.PointCost = (138 + 4) * 3;
            att = new Attack();
            att.Aerial = true;
            att.Magnetic = true;
            att.DicePools.Add(8 + 2 * 4);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Indirect = true;
            att.Piercing = true;
            att.DicePools.Add(8 + 2 * 4);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.RerollBlanks = true;
            att.DicePools.Add(2 + 3 * 6);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mandjet x3 DCannons Devastating All Guns Combined Closing";
            attacker.PointCost = (138 + 4) * 3;
            att = new Attack();
            att.Aerial = true;
            att.Magnetic = true;
            att.DicePools.Add(8 + 2 * 4);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Devastating = true;
            att.Piercing = true;
            att.DicePools.Add(8 + 2 * 4);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.RerollBlanks = true;
            att.DicePools.Add(2 + 3 * 6);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            return attackers;
        }

        internal static IList<Attacker> GetEgyptians_Mesketets()
        {
            IList<Attacker> attackers = new List<Attacker>();
            Attacker attacker;
            Attack att;

            // attacker = new Attacker();
            // attacker.Name = "Mesketet x3 Torps Separate All Guns Combined";
            // attacker.PointCost = (122) * 3;
            // att = new Attack();
            // att.Submerged = true;
            // att.Torpedo = true;
            // att.DicePools.Add(12);
            // att.DicePools.Add(12);
            // att.DicePools.Add(12);
            // attacker.Attacks.Add(att);
            // att = new Attack();
            // att.Gunnery = true;
            // att.DicePools.Add(5 + 8 * 3);
            // attacker.Attacks.Add(att);
            // attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mesketet x3 Torps Combined All Guns Combined";
            attacker.PointCost = (122) * 3;
            att = new Attack();
            att.Submerged = true;
            att.Torpedo = true;
            att.DicePools.Add(12 + 7 * 2);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(5 + 8 * 3);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            // attacker = new Attacker();
            // attacker.Name = "Mesketet x3 Torps Separate Lances Combined Point Blank";
            // attacker.PointCost = (122 + 5 * 3) * 3;
            // att = new Attack();
            // att.Submerged = true;
            // att.Torpedo = true;
            // att.DicePools.Add(12);
            // att.DicePools.Add(12);
            // att.DicePools.Add(12);
            // attacker.Attacks.Add(att);
            // att = new Attack();
            // att.Gunnery = true;
            // att.Sustained = true;
            // att.DicePools.Add(6 + 8 * 3);
            // attacker.Attacks.Add(att);
            // attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mesketet x3 Torps Combined All Guns Combined Khepri";
            attacker.PointCost = (122 + 20) * 3;
            att = new Attack();
            att.Homing = true;
            att.Devastating = true;
            att.Piercing = true;
            att.AllowIntercept = true;
            att.ShotsPerSRS = 3;
            att.DicePools.Add(3 * 2 * 3);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Submerged = true;
            att.Torpedo = true;
            att.DicePools.Add(12 + 7 * 2);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(5 + 8 * 3);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            return attackers;
        }

        internal static IList<Attacker> GetEgyptians_Sobek()
        {
            IList<Attacker> attackers = new List<Attacker>();
            Attacker attacker;
            Attack att;

            attacker = new Attacker();
            attacker.Name = "Sobek x3 All Guns Combined";
            attacker.PointCost = (118) * 3;
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(9 + 4 * 2 + 6 * 3);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Sobek x3 Guns Split Per Boat";
            attacker.PointCost = (118) * 3;
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(9 + 2 * 3);
            att.DicePools.Add(9 + 2 * 3); 
            att.DicePools.Add(9 + 2 * 3);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);
            attacker = new Attacker();

            attacker = new Attacker();
            attacker.Name = "Mesketet x3 Torps Combined All Guns Combined";
            attacker.PointCost = (122) * 3;
            att = new Attack();
            att.Submerged = true;
            att.Torpedo = true;
            att.DicePools.Add(12 + 7 * 2);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(5 + 8 * 3);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Sobek x3 All Guns Combined Khepri";
            attacker.PointCost = (118+20) * 3;
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(9 + 4 * 2 + 6 * 3);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Homing = true;
            att.Devastating = true;
            att.Piercing = true;
            att.AllowIntercept = true;
            att.ShotsPerSRS = 3;
            att.DicePools.Add(3 * 2 * 3);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Sobek x3 Guns Split Per Boat Khepri";
            attacker.PointCost = (118+20) * 3;
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(9 + 2 * 3);
            att.DicePools.Add(9 + 2 * 3);
            att.DicePools.Add(9 + 2 * 3);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Homing = true;
            att.Devastating = true;
            att.Piercing = true;
            att.AllowIntercept = true;
            att.ShotsPerSRS = 3;
            att.DicePools.Add(3 * 2 * 3);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Mesketet x3 Torps Combined All Guns Combined Khepri";
            attacker.PointCost = (122 + 20) * 3;
            att = new Attack();
            att.Homing = true;
            att.Devastating = true;
            att.Piercing = true;
            att.AllowIntercept = true;
            att.ShotsPerSRS = 3;
            att.DicePools.Add(3 * 2 * 3);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Submerged = true;
            att.Torpedo = true;
            att.DicePools.Add(12 + 7 * 2);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(5 + 8 * 3);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            return attackers;
        }

        internal static IList<Attacker> GetEgyptians_Sabah()
        {
            IList<Attacker> attackers = new List<Attacker>();
            Attacker attacker;
            Attack att;

            attacker = new Attacker();
            attacker.Name = "Sabah Singleton Salvo Missile Blast";
            attacker.PointCost = 120;
            att = new Attack();
            att.Aerial = true;
            att.RerollBlanks = true;
            att.Sustained = true;
            att.DicePools.Add(10 + 2);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "2x Sabah Salvo Missile Blast";
            attacker.PointCost = 120 * 2;
            att = new Attack();
            att.Aerial = true;
            att.RerollBlanks = true;
            att.Sustained = true;
            att.DicePools.Add(10 + 6 + 2);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "3x Sabah Salvo Missile Blast";
            attacker.PointCost = 120 * 3;
            att = new Attack();
            att.Aerial = true;
            att.RerollBlanks = true;
            att.Sustained = true;
            att.DicePools.Add(10 + 6 * 2 + 2);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            return attackers;
        }
        internal static IList<Attacker> GetEgyptians_M1()
        {
            IList<Attacker> attackers = new List<Attacker>();
            Attacker attacker;
            Attack att;

            attacker = new Attacker();
            attacker.Name = "Hashashin Torps Gun Batteries GR Closing";
            attacker.PointCost = (40) * 5;
            att = new Attack();
            att.Submerged = true;
            att.Torpedo = true;
            att.Homing = true;
            att.RerollBlanks = true;
            att.DicePools.Add(5 + 3 * 4);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(5 + 3 * (2 * 5 - 1) + 5);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);
            attacker = new Attacker();

            attacker = new Attacker();
            attacker.Name = "Hashashin Gun Batteries GR Broadside Point Blank";
            attacker.PointCost = (40) * 5;
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(3 + 2 * (2 * 5 - 1) + 5);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Broadside = true;
            att.Fusilade = true;
            att.DicePools.Add(4 + 3 * 4);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Hashashin Aetheric Lances GR Broadside Point Blank";
            attacker.PointCost = (40 + 2 * 5) * 5;
            att = new Attack();
            att.Gunnery = true;
            att.Sustained = true;
            att.DicePools.Add(6 + 3 * (2 * 5 - 1) + 5);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Broadside = true;
            att.Fusilade = true;
            att.DicePools.Add(4 + 3 * 4);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            // attacker = new Attacker();
            // attacker.Name = "Hashashin Gun Batteries Broadside Crossing the T Point Blank";
            // attacker.PointCost = (40) * 5;
            // att = new Attack();
            // att.Sustained = true;
            // att.DicePools.Add(4 + 3 * 4 + 2*5);
            // attacker.Attacks.Add(att);
            // attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Kopesh Gun Batteries Broadside Point Blank";
            attacker.PointCost = (38) * 5;
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(3 + 2 * (2 * 5 - 1));
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Broadside = true;
            att.Fusilade = true;
            att.DicePools.Add(4 + 3 * 4);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            // attacker = new Attacker();
            // attacker.Name = "Kopesh Gun Batteries Broadside Crossing the T Point Blank";
            // attacker.PointCost = (38) * 5;
            // att = new Attack();
            // att.Sustained = true;
            // att.DicePools.Add(4 + 3 * 4 + 2 * 5);
            // attacker.Attacks.Add(att);
            // attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Kopesh Aetheric Lances Broadside Point Blank";
            attacker.PointCost = (38 + 2 * 5) * 5;
            att = new Attack();
            att.Gunnery = true;
            att.Sustained = true;
            att.DicePools.Add(6 + 3 * (2 * 5 - 1));
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Broadside = true;
            att.Fusilade = true;
            att.DicePools.Add(4 + 3 * 4);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            return attackers;
        }

        internal static IList<Attacker> GetEgyptians_Flagships()
        {
            IList<Attacker> attackers = new List<Attacker>();
            Attacker attacker;
            Attack att;

            attacker = new Attacker();
            attacker.Name = "Abydos Gun Batteries Closing";
            attacker.PointCost = 320;
            att = new Attack();
            att.Aerial = true;
            att.DicePools.Add(9);
            attacker.Attacks.Add(att);
            att = new Attack();
            att.Gunnery = true;
            att.DicePools.Add(5+3*5);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Abydos Aetheric Lances Point Blank";
            attacker.PointCost = 320+5*6;
            att = new Attack();
            att.Gunnery = true;
            att.Sustained = true;
            att.DicePools.Add(6+3*5);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            attacker = new Attacker();
            attacker.Name = "Pharos D-Cannon ";
            attacker.PointCost = 120 * 3;
            att = new Attack();
            att.Aerial = true;
            att.RerollBlanks = true;
            att.Sustained = true;
            att.DicePools.Add(10 + 6 * 2 + 2);
            attacker.Attacks.Add(att);
            attackers.Add(attacker);

            return attackers;
        }

    }
}
