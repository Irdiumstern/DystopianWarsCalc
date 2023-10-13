using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using DystopianWarsCalc.Model.Enum;
using DystopianWarsCalc.Utilities.Enum;
using DystopianWarsCalc.Utilities;
using static System.Net.Mime.MediaTypeNames;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using UglyToad.PdfPig.DocumentLayoutAnalysis;
using DystopianWarsCalc.Utilities.Logging;
using System.Numerics;
using DystopianWarsCalc.Model.Rules;

namespace DystopianWarsCalc.Data
{
    public static class OrbatPDFExtractor
    {
        public static Orbat LoadORBAT(string path)
        {
            Orbat orbat = new Orbat(path);
            int nameStartIndex = path.IndexOf('_') + 1;
            int nameEndIndex = path.IndexOf('-');
            orbat.FactionName = path.Substring(nameStartIndex, nameEndIndex - nameStartIndex);

            int weaponRefPage = 0;
            IDictionary<int, Page> pages = new Dictionary<int, Page>();
            int pageCount = 0;
            using (PdfDocument document = PdfDocument.Open(path))
            {
                foreach (Page page in document.GetPages())
                {
                    string pageText = page.Text;
                    pageCount++;
                    pages.Add(pageCount, page);
                    if (pageText.Contains(Defines.WeaponRefStartText))
                    {
                        weaponRefPage = pageCount;
                    }
                }
            }

            ParseWeapons(orbat, weaponRefPage, pages);

            foreach (var page in pages.Where(x => x.Key > weaponRefPage))
            {
                var words = NearestNeighbourWordExtractor.Instance.GetWords(page.Value.Letters);
                var blocks = DocstrumBoundingBoxes.Instance.GetBlocks(words);

                IList<string> textSegments = new List<string>();
                // StringBuilder assembledTextSegments = new StringBuilder();

                IList<Datasheet> datasheetsText = new List<Datasheet>();
                List<string> datasheetKeywords = Defines.DatasheetKeywords.ToList();
                var positionTraits = new List<PositionTrait>((IEnumerable<PositionTrait>)Enum.GetValues(typeof(PositionTrait))).ToDictionary(x => Enum.GetName(typeof(PositionTrait), x).Replace(Defines.BlankSpaceReplacement, " "), x => x);

                if (!string.IsNullOrWhiteSpace(page.Value.Text))
                {
                    string currentKeyword = null;
                    Datasheet datasheet = null;
                    foreach (var token in page.Value.Text.Split(" "))
                    {
                        string newKeyword;
                        if (datasheetKeywords.Contains(token)
                            || (textSegments.Count() > 0 && datasheetKeywords.Contains(textSegments.Last() + " " + token))
                            || (token == Defines.DatasheetMassMarker && !string.IsNullOrWhiteSpace(currentKeyword)))
                        {
                            if (token == Defines.DatasheetMassMarker && !string.IsNullOrWhiteSpace(currentKeyword))
                            {
                                newKeyword = null;
                            }
                            else
                            {
                                newKeyword = Defines.DatasheetKeywords.Contains(token) ? token : textSegments.Last() + " " + token;
                                if (!Defines.DatasheetKeywords.Contains(token))
                                {
                                    textSegments.RemoveAt(textSegments.Count - 1);
                                }
                            }

                            switch (currentKeyword)
                            {
                                case Defines.DatasheetBattleReady:
                                    datasheet = new Datasheet();
                                    datasheetsText.Add(datasheet);
                                    datasheet.profiles[ModelStatus.Battle_Ready].Mass = int.Parse(textSegments[0]);
                                    if (datasheet.profiles[ModelStatus.Battle_Ready].Mass == Defines.NoCrippledMass)
                                    {
                                        datasheetKeywords.Remove(Defines.DatasheetCrippled);
                                    }
                                    datasheet.profiles[ModelStatus.Battle_Ready].Speed = int.Parse(textSegments[1]);
                                    datasheet.profiles[ModelStatus.Battle_Ready].TurnLimit = int.Parse(textSegments[2]);
                                    datasheet.profiles[ModelStatus.Battle_Ready].Armor = int.Parse(textSegments[3]);
                                    datasheet.profiles[ModelStatus.Battle_Ready].Citadel = int.Parse(textSegments[4]);
                                    datasheet.profiles[ModelStatus.Battle_Ready].AerialDefence = int.Parse(textSegments[5]);
                                    datasheet.profiles[ModelStatus.Battle_Ready].SubmergedDefence = int.Parse(textSegments[6]);
                                    datasheet.profiles[ModelStatus.Battle_Ready].Fray = int.Parse(textSegments[7]);
                                    datasheet.profiles[ModelStatus.Battle_Ready].Hull = int.Parse(textSegments[8]);
                                    datasheetKeywords.Remove(currentKeyword);
                                    break;
                                case Defines.DatasheetCrippled:
                                    datasheet.profiles[ModelStatus.Crippled].Mass = int.Parse(textSegments[0]);
                                    datasheet.profiles[ModelStatus.Crippled].Speed = int.Parse(textSegments[1]);
                                    datasheet.profiles[ModelStatus.Crippled].TurnLimit = int.Parse(textSegments[2]);
                                    datasheet.profiles[ModelStatus.Crippled].Armor = int.Parse(textSegments[3]);
                                    datasheet.profiles[ModelStatus.Crippled].Citadel = int.Parse(textSegments[4]);
                                    datasheet.profiles[ModelStatus.Crippled].AerialDefence = int.Parse(textSegments[5]);
                                    datasheet.profiles[ModelStatus.Crippled].SubmergedDefence = int.Parse(textSegments[6]);
                                    datasheet.profiles[ModelStatus.Crippled].Fray = int.Parse(textSegments[7]);
                                    datasheet.profiles[ModelStatus.Crippled].Hull = int.Parse(textSegments[8]);
                                    datasheetKeywords.Remove(currentKeyword);
                                    break;
                                case Defines.DatasheetUnitComposition:
                                    var tokens = textSegments.Aggregate((x, y) => x + " " + y).Replace(Defines.BulletPoint, String.Empty).Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                                    datasheet.Name = tokens.Skip(1).Aggregate((x, y) => x + ' ' + y);
                                    int amount;
                                    if (int.TryParse(tokens[0], out amount))
                                    {
                                        datasheet.MinAmount = amount;
                                    }
                                    else
                                    {
                                        var log = LoggingService.AddLog("ERROR parsing unit composition amount");
                                        log.Items.Add(textSegments);
                                        log.Items.Add(page.Value.Text);
                                        log.Items.Add(tokens);
                                    }
                                    datasheetKeywords.Remove(currentKeyword);
                                    break;
                                case Defines.DatasheetTraits:
                                    if (textSegments.Count > 0)
                                    {
                                        foreach (var text in textSegments.Aggregate((x, y) => x + " " + y).Split(Defines.BulletPoint, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                                        {
                                            var trait = orbat.GetTrait(text);
                                            datasheet.traits.Add(trait);
                                            if (positionTraits.ContainsKey(text))
                                            {
                                                trait.PositionTrait = positionTraits[text];
                                            }
                                        }
                                    }
                                    datasheetKeywords.Remove(currentKeyword);
                                    break;
                                case Defines.DatasheetSpecialRules:
                                    foreach (var text in textSegments.Aggregate((x, y) => x + " " + y).Split(Defines.BulletPoint, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                                    {
                                        var specialRule = orbat.GetSpecialRule(text); // new SpecialRule(text);
                                        datasheet.specialRules.Add(specialRule);
                                    }
                                    datasheetKeywords.Remove(currentKeyword);
                                    break;
                                case Defines.DatasheetSquadron:
                                    if (textSegments.Contains(Defines.SquadronTagAttached))
                                    {
                                        datasheet.CanAttach = true;
                                    }
                                    var pointsToken = textSegments.Where(x => x.Contains(Defines.SquadronCostTag)).First().Replace(Defines.SquadronCostTag, string.Empty).Replace("+", string.Empty);
                                    int pointCost;
                                    if (int.TryParse(pointsToken, out pointCost))
                                    {
                                        datasheet.PointCostPerModel = pointCost;
                                    }
                                    else
                                    {
                                        var log = LoggingService.AddLog("ERROR parsing point cost");
                                        log.Items.Add(page.Value.Text);
                                        log.Items.Add(textSegments);
                                        log.Items.Add(pointsToken);
                                    }

                                    var squadronAmount = Defines.SquadronNumbers.Keys.Intersect(textSegments).ToList();
                                    if (squadronAmount.Count == 1)
                                    {
                                        datasheet.MaxAmount = datasheet.MinAmount + Defines.SquadronNumbers[squadronAmount.First()];
                                    }
                                    else
                                    {
                                        var log = LoggingService.AddLog("ERROR parsing max amount");
                                        log.Items.Add(page.Value.Text);
                                        log.Items.Add(squadronAmount);
                                        datasheet.MaxAmount = Defines.InvalidAmount;
                                    }
                                    datasheetKeywords.Remove(currentKeyword);
                                    break;
                                case Defines.DatasheetWeapons:
                                    StringBuilder weaponName = new StringBuilder();
                                    string weaponCompText = null;

                                    bool nextIsArcs = false;
                                    foreach (var text in textSegments)
                                    {
                                        if (text == Defines.BulletPoint)
                                        {
                                            weaponName.Clear();
                                        }
                                        else if (text == "-F")
                                        {
                                            // ERROR ORBAT
                                            weaponCompText = weaponName.ToString().Trim();
                                            var weapon = orbat.Weapons.Where(x => x.Name == weaponCompText).FirstOrDefault();

                                            if (weapon != null)
                                            {
                                                datasheet.weapons.Add(new Tuple<Weapon, FireArc>(weapon, FireArc.Fore));
                                                weapon = null;
                                            }
                                            else
                                            {
                                                var log = LoggingService.AddLog("ERROR weapon import");
                                                log.Items.Add(page.Value.Text);
                                                log.Items.Add(weapon);
                                                log.Items.Add(weaponName);
                                                log.Items.Add(text);
                                                log.Items.Add(datasheetsText);
                                            }

                                            weaponName.Clear();
                                            weaponCompText = string.Empty;
                                        }
                                        else if (Defines.WeaponDividerTexts.Contains(text))
                                        {
                                            weaponCompText = weaponName.ToString().Trim();
                                            // ERROR ORBAT
                                            if (weaponCompText == "Katyusha Salvo")
                                            {
                                                weaponCompText = "Katyusha Rocket Salvo";
                                            }
                                            else if (weaponCompText == "Tri-Rail Gun Battery")
                                            {
                                                weaponCompText = "Tri-Railgun";
                                            }
                                            var weapon = orbat.Weapons.Where(x => x.Name == weaponCompText).FirstOrDefault();
                                            if (weapon != null || weaponCompText.Contains("Huoqiang"))
                                            {
                                                nextIsArcs = true;
                                                weaponName.Clear();
                                            }
                                        }
                                        else if (nextIsArcs)
                                        {
                                            FireArc arc = FireArc.None;
                                            switch (text)
                                            {
                                                case "F":
                                                    arc = FireArc.Fore;
                                                    break;
                                                case "A":
                                                    arc = FireArc.Aft;
                                                    break;
                                                case "P":
                                                    arc = FireArc.Port;
                                                    break;
                                                case "S":
                                                    arc = FireArc.Starboard;
                                                    break;
                                                case "F/P/S":
                                                    arc = FireArc.Forward;
                                                    break;
                                                case "F/P/A":
                                                    arc = FireArc.PortSide;
                                                    break;
                                                case "F/S/A":
                                                    arc = FireArc.Starboard;
                                                    break;
                                                case "360o":
                                                case "360":
                                                    // ERROR ORBAT
                                                    arc = FireArc.All;
                                                    break;
                                                case "A/P/S":
                                                    arc = FireArc.Rear;
                                                    break;
                                                case "P&S":
                                                    arc = FireArc.Broadside;
                                                    break;
                                                case "F&A":
                                                    arc = FireArc.Fore | FireArc.Aft;
                                                    break;
                                                case "F/P":
                                                    arc = FireArc.Fore | FireArc.Port;
                                                    break;
                                                case "F/S":
                                                    arc = FireArc.Fore | FireArc.Starboard;
                                                    break;
                                                case "A/P":
                                                    arc = FireArc.Aft | FireArc.Port;
                                                    break;
                                                case "A/S":
                                                    arc = FireArc.Aft | FireArc.Starboard;
                                                    break;
                                                default:
                                                    break;
                                            }

                                            if (weaponCompText == "Heavy Huoqiang")
                                            {
                                                foreach (var weapon in orbat.Weapons.Where(x => x.Name.StartsWith(weaponCompText)))
                                                {
                                                    datasheet.weapons.Add(new Tuple<Weapon, FireArc>(weapon, arc));
                                                }
                                            }
                                            else
                                            {
                                                var weapon = orbat.Weapons.Where(x => x.Name == weaponCompText).FirstOrDefault();
                                                if (arc != FireArc.None && weapon != null)
                                                {
                                                    datasheet.weapons.Add(new Tuple<Weapon, FireArc>(weapon, arc));
                                                }
                                                else
                                                {
                                                    var log = LoggingService.AddLog("ERROR weapon import");
                                                    log.Items.Add(page.Value.Text);
                                                    log.Items.Add(weapon);
                                                    log.Items.Add(weaponName);
                                                    log.Items.Add(text);
                                                    log.Items.Add(arc);
                                                    log.Items.Add(datasheetsText);
                                                    log.Items.Add(textSegments);
                                                    break;
                                                }

                                                nextIsArcs = false;
                                                weaponCompText = string.Empty;
                                            }
                                        }
                                        else
                                        {
                                            weaponName.Append(text + " ");
                                        }
                                    }
                                    int asd = 0;
                                    break;
                            }

                            textSegments.Clear();
                            // assembledTextSegments.Clear();
                            currentKeyword = newKeyword;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(token))
                            {
                                textSegments.Add(token);
                            }

                            // assembledTextSegments.Append(" " + token);
                        }

                        if (token == Defines.DatasheetMassMarker)
                        {
                            datasheetKeywords = Defines.DatasheetKeywords.ToList();
                        }
                    }

                    IList<Datasheet> datasheetsBlock = new List<Datasheet>();

                    ParseDatasheetUnitComposition(orbat, blocks, datasheetsBlock);
                    ParseDatasheetTraits(blocks, datasheetsBlock, orbat);
                    ParseDatasheetSpecialRules(blocks, datasheetsBlock, orbat);
                    ParseDatasheetSquadron(blocks, datasheetsBlock);

                    if (datasheetsText.Count != datasheetsBlock.Count)
                    {
                        var log = LoggingService.AddLog("ERROR datasheet amount");
                        log.Items.Add(page.Value.Text);
                        log.Items.Add(datasheetsBlock);
                        log.Items.Add(datasheetsText);
                    }

                    for (int i = 0; i < datasheetsText.Count; i++)
                    {
                        var textDS = datasheetsText[i];
                        var blockDS = datasheetsBlock[i];
                        if (textDS.HasProfileErrors)
                        {
                            var log = LoggingService.AddLog("ERROR datasheet profile");
                            log.Items.Add(page.Value.Text);
                            log.Items.Add(textDS);
                            log.Items.Add(blockDS);
                        }

                        if (textDS.HasErrors && blockDS.HasErrors)
                        {
                            var log = LoggingService.AddLog("ERROR both datasheets wrong");
                            log.Items.Add(page.Value.Text);
                            log.Items.Add(textDS);
                            log.Items.Add(blockDS);
                        }
                        else if (blockDS.HasErrors)
                        {
                            orbat.datasheets.Add(textDS);
                        }
                        else
                        {
                            blockDS.profiles = textDS.profiles;
                            blockDS.weapons = textDS.weapons;
                            orbat.datasheets.Add(blockDS);
                        }


                    }

                    int x = 0;
                }
            }

            //foreach (var datasheet in orbat.Datasheets)
            //{
            //    foreach (var trait in datasheet.Traits)
            //    {
            //        if (orbat.traits.Where(x => x.Name == trait.Name)?.Count() == 0)
            //        {
            //            orbat.traits.Add(trait);
            //        }
            //    }
            //    foreach (var specialRule in datasheet.SpecialRules)
            //    {
            //        if (orbat.specialRules.Where(x => x.Name == specialRule.Name)?.Count() == 0)
            //        {
            //            orbat.specialRules.Add(specialRule);
            //        }
            //    }
            //}

            return orbat;
        }

        private static void ParseDatasheetSquadron(IReadOnlyList<TextBlock> blocks, IList<Datasheet> datasheets)
        {
            var squadronBlocks = blocks.Where(x => x.Text.Contains(Defines.DatasheetSquadron)).ToList();
            foreach (var block in squadronBlocks)
            {
                var squadronBlockIndex = (blocks as List<TextBlock>).FindIndex(x => x == block);
                var datasheet = datasheets.Where(x => x.startingBlockIndex <= squadronBlockIndex && x.endingBlockIndex >= squadronBlockIndex).First();
                var tokens = block.Text.Replace('\n', ' ').Split(' ');
                if (tokens.Contains(Defines.SquadronTagAttached))
                {
                    datasheet.CanAttach = true;
                }
                var pointsToken = tokens.Where(x => x.Contains(Defines.SquadronCostTag)).ToList();
                if (pointsToken.Count == 0)
                {
                    pointsToken = blocks.Select(x => x.Text).Where(x => x.Contains(Defines.SquadronCostTag) && x.StartsWith("+")).ToList();
                }

                int pointCost;
                if (int.TryParse(pointsToken.First().Replace(Defines.SquadronCostTag, string.Empty).Replace("+", string.Empty), out pointCost))
                {
                    datasheet.PointCostPerModel = pointCost;
                }
                else
                {
                    var log = LoggingService.AddLog("ERROR parsing point cost");
                    log.Items.Add(block);
                    log.Items.Add(pointsToken);
                }

                var amount = Defines.SquadronNumbers.Keys.Intersect(tokens).ToList();
                if (amount.Count == 1)
                {
                    datasheet.MaxAmount = datasheet.MinAmount + Defines.SquadronNumbers[amount.First()];
                }
                else
                {
                    var log = LoggingService.AddLog("ERROR parsing max amount");
                    log.Items.Add(block);
                    log.Items.Add(amount);
                    datasheet.MaxAmount = Defines.InvalidAmount;
                }
            }
        }

        private static void ParseDatasheetSpecialRules(IReadOnlyList<TextBlock> blocks, IList<Datasheet> datasheets, Orbat orbat)
        {
            var specialRulesBlocks = blocks.Where(x => x.Text.StartsWith(Defines.DatasheetSpecialRules)).ToList();
            foreach (var block in specialRulesBlocks)
            {
                var specialRulesBlockIndex = (blocks as List<TextBlock>).FindIndex(x => x == block);
                var datasheet = datasheets.Where(x => x.startingBlockIndex < specialRulesBlockIndex && x.endingBlockIndex > specialRulesBlockIndex).First();
                foreach (var text in block.TextLines.Skip(1).Select(x => x.Text.Replace(Defines.BulletPoint, string.Empty).Trim()))
                {
                    var specialRule = orbat.GetSpecialRule(text); // new SpecialRule(text);
                    datasheet.specialRules.Add(specialRule);
                }
            }

            foreach (var datasheet in datasheets.Where(x => x.traitBlockForSpecialRules != null))
            {
                var block = datasheet.traitBlockForSpecialRules;
                bool inSpecialRules = false;
                foreach (var text in block.TextLines.Skip(1).Select(x => x.Text.Replace(Defines.BulletPoint, string.Empty).Trim()))
                {
                    if (inSpecialRules)
                    {
                        var specialRule = orbat.GetSpecialRule(text); // new SpecialRule(text);
                        datasheet.specialRules.Add(specialRule);
                    }
                    else if (text == Defines.DatasheetSpecialRules)
                    {
                        inSpecialRules = true;
                    }
                }
            }
        }

        private static void ParseDatasheetTraits(IReadOnlyList<TextBlock> blocks, IList<Datasheet> datasheets, Orbat orbat)
        {
            var positionTraits = new List<PositionTrait>((IEnumerable<PositionTrait>)Enum.GetValues(typeof(PositionTrait))).ToDictionary(x => Enum.GetName(typeof(PositionTrait), x).Replace(Defines.BlankSpaceReplacement, " "), x => x);
            var traitBlocks = blocks.Where(x => x.Text.StartsWith(Defines.DatasheetTraits)).ToList();
            foreach (var block in traitBlocks)
            {
                var traitBlockIndex = (blocks as List<TextBlock>).FindIndex(x => x == block);
                var datasheet = datasheets.Where(x => x.startingBlockIndex < traitBlockIndex && x.endingBlockIndex > traitBlockIndex).First();
                foreach (var text in block.TextLines.Skip(1).Select(x => x.Text.Replace(Defines.BulletPoint, string.Empty).Trim()))
                {
                    if (text.StartsWith(Defines.DatasheetSpecialRules))
                    {
                        datasheet.traitBlockForSpecialRules = block;
                        break;
                    }

                    var trait = orbat.GetTrait(text); // new Trait(text);
                    datasheet.traits.Add(trait);
                    if (positionTraits.ContainsKey(text))
                    {
                        trait.PositionTrait = positionTraits[text];
                    }
                }
            }
        }

        private static void ParseDatasheetUnitComposition(Orbat orbat, IReadOnlyList<TextBlock> blocks, IList<Datasheet> datasheets)
        {
            var unitCompBlocks = blocks.Where(x => x.Text.StartsWith(Defines.DatasheetUnitComposition)).ToList();
            Datasheet datasheet = null;
            Datasheet prev = null;
            foreach (var block in unitCompBlocks)
            {
                datasheet = new Datasheet();
                datasheets.Add(datasheet);
                datasheet.startingBlockIndex = (blocks as List<TextBlock>).FindIndex(x => x == block);
                datasheet.endingBlockIndex = blocks.Count() - 1;
                if (prev != null)
                {
                    prev.endingBlockIndex = datasheet.startingBlockIndex - 1;
                }
                prev = datasheet;

                if (block.Text != Defines.DatasheetUnitComposition)
                {
                    var toParse = block.TextLines.Where(x => x.Text != Defines.DatasheetUnitComposition)?.Select(x => x.Text.Replace(Defines.BulletPoint, String.Empty)?.Trim()).Aggregate((x, y) => x + ' ' + y);
                    if (!string.IsNullOrEmpty(toParse))
                    {
                        var tokens = toParse.Split(' ');

                        datasheet.Name = tokens.Skip(1).Aggregate((x, y) => x + ' ' + y);
                        int amount;
                        if (int.TryParse(tokens[0], out amount))
                        {
                            datasheet.MinAmount = amount;
                        }
                        else
                        {
                            var log = LoggingService.AddLog("ERROR parsing unit composition amount");
                            log.Items.Add(block);
                            log.Items.Add(tokens);
                        }
                    }
                }
            }
        }

        private static void ParseWeapons(Orbat orbat, int weaponRefPage, IDictionary<int, Page> pages)
        {
            var targetPage = pages[weaponRefPage];
            var words = NearestNeighbourWordExtractor.Instance.GetWords(targetPage.Letters);
            var blocks = DocstrumBoundingBoxes.Instance.GetBlocks(words);
            List<string> weaponNames = ParseWeaponsFromBlocks(blocks);
            var weaponQualitySets = GetWeaponQualitySets(blocks);
            var weaponQualities = new List<CommonWeaponQuality>((IEnumerable<CommonWeaponQuality>)Enum.GetValues(typeof(CommonWeaponQuality))).ToDictionary(x => Enum.GetName(typeof(CommonWeaponQuality), x).Replace(Defines.BlankSpaceReplacement, " "), x => x);

            Weapon weapon = null;
            WeaponParsingStep parsingStep = WeaponParsingStep.None;
            WeaponRange targetWeaponRange = WeaponRange.None;
            string previousText = null;
            IList<string> textSegments = new List<string>();
            StringBuilder assembledTextSegments = new StringBuilder();
            foreach (var text in targetPage.Text.Split(' '))
            {
                if (text == Defines.WeaponLastTableHeader)
                {
                    // Begin parsing on next word
                    // weapon = new Weapon();
                    // orbat.weapons.Add(weapon);
                    parsingStep = WeaponParsingStep.Name;
                    targetWeaponRange = WeaponRange.Point_Blank;
                }
                else if (parsingStep != WeaponParsingStep.None)
                {
                    switch (parsingStep)
                    {
                        case WeaponParsingStep.Name:
                            if (text.Any(char.IsDigit) || IsTextWeaponEmpty(text) || text == Defines.WeaponDiceAgitation)
                            {
                                if (weaponNames.Contains(assembledTextSegments.ToString().Trim()))
                                {
                                    weapon = new Weapon();
                                    weapon.Name = assembledTextSegments.ToString().Trim();
                                    orbat.weapons.Add(weapon);
                                    parsingStep = WeaponParsingStep.BRDice;
                                    targetWeaponRange = WeaponRange.Point_Blank;
                                    parsingStep = ParseWeaponDice(text, ref targetWeaponRange, parsingStep, weapon);
                                    textSegments.Clear();
                                    assembledTextSegments.Clear();
                                }
                                else
                                {
                                    var log = LoggingService.AddLog("ERROR parsing name");
                                    log.Items.Add(text);
                                    log.Items.Add(textSegments);
                                }
                            }
                            else
                            {
                                textSegments.Add(text);
                                assembledTextSegments.Append(text + " ");
                            }
                            break;
                        case WeaponParsingStep.BRDice:
                        case WeaponParsingStep.CrippledDice:
                            parsingStep = ParseWeaponDice(text, ref targetWeaponRange, parsingStep, weapon);
                            break;
                        case WeaponParsingStep.BRQualities:
                            if ((text.Any(char.IsDigit) && !text.Contains(",")) || IsTextWeaponEmpty(text))
                            {
                                if (weaponQualitySets.Contains(assembledTextSegments.ToString().Trim()))
                                {
                                    ParseWeaponQualities(assembledTextSegments.ToString().Trim(), parsingStep, weapon, weaponQualities);
                                    parsingStep = WeaponParsingStep.CrippledDice;
                                    targetWeaponRange = WeaponRange.Point_Blank;
                                    ParseWeaponDice(text, ref targetWeaponRange, parsingStep, weapon);
                                    textSegments.Clear();
                                    assembledTextSegments.Clear();
                                }
                                else
                                {
                                    textSegments.Add(text);
                                    assembledTextSegments.Append(text + " ");
                                    if (weaponQualitySets.Contains(assembledTextSegments.ToString().Trim()))
                                    {
                                        ParseWeaponQualities(assembledTextSegments.ToString().Trim(), parsingStep, weapon, weaponQualities);
                                        parsingStep = WeaponParsingStep.CrippledDice;
                                        targetWeaponRange = WeaponRange.Point_Blank;
                                        parsingStep = ParseWeaponDice(text, ref targetWeaponRange, parsingStep, weapon);
                                        textSegments.Clear();
                                        assembledTextSegments.Clear();
                                    }
                                    else
                                    {
                                        var log = LoggingService.AddLog("ERROR parsing qualities");
                                        log.Items.Add(text);
                                        log.Items.Add(textSegments);
                                    }
                                }
                            }
                            else
                            {
                                textSegments.Add(text);
                                assembledTextSegments.Append(text + " ");
                            }
                            break;
                        case WeaponParsingStep.CrippledQualities:
                            if (text == String.Empty)
                            {
                                int asde = 0;
                            }
                            var textSegmentsMatchQualities = weaponQualitySets.Contains(assembledTextSegments.ToString().Trim());
                            var nextTextSegment = assembledTextSegments.ToString() + text;
                            if ((textSegmentsMatchQualities && !weaponQualitySets.Contains(nextTextSegment) || text == String.Empty))
                            {
                                // First part of new name found
                                ParseWeaponQualities(assembledTextSegments.ToString().Trim(), parsingStep, weapon, weaponQualities);
                                textSegments.Clear();
                                assembledTextSegments.Clear();
                                parsingStep = WeaponParsingStep.Name;
                                textSegments.Add(text);
                                assembledTextSegments.Append(text + " ");
                            }
                            else
                            {
                                textSegments.Add(text);
                                assembledTextSegments.Append(text + " ");
                            }
                            break;
                    }
                }

                previousText = text;
            }
        }

        private static bool IsTextWeaponEmpty(string text)
        {
            return text.Replace("(", string.Empty).Replace(")", string.Empty) == Defines.WeaponEmpty;
        }

        private static bool IsTextSupportDice(string text)
        {
            return text.Contains('(') && text.Contains(')');
        }

        private static WeaponParsingStep ParseWeaponDice(string text, ref WeaponRange targetWeaponRange, WeaponParsingStep currentStep, Weapon weapon)
        {
            ModelStatus tarStatus = currentStep == WeaponParsingStep.BRDice ? ModelStatus.Battle_Ready : ModelStatus.Crippled;
            int parsedNumber = 0;
            if (text.StartsWith(Defines.WeaponDiceAgitation))
            {
                string number = new String(text.Where(Char.IsDigit).ToArray());
                if (int.TryParse(number, out parsedNumber))
                {
                    weapon.actionDice[targetWeaponRange].SetActionDice(tarStatus, WeaponType.Lead, parsedNumber);
                    weapon.actionDice[targetWeaponRange].SetActionDice(tarStatus, WeaponType.Support, Defines.AgitationSupportDice);
                }
                else
                {
                    weapon.actionDice[targetWeaponRange].SetActionDice(tarStatus, WeaponType.Lead, Defines.AgitationDefaultAmount);
                    weapon.actionDice[targetWeaponRange].SetActionDice(tarStatus, WeaponType.Support, Defines.AgitationSupportDice);
                }

                targetWeaponRange = targetWeaponRange.Next();
            }
            else if (int.TryParse(text.Replace("(", string.Empty).Replace(")", string.Empty), out parsedNumber))
            {
                if (IsTextSupportDice(text))
                {
                    weapon.actionDice[targetWeaponRange].SetActionDice(tarStatus, WeaponType.Support, parsedNumber);
                    targetWeaponRange = targetWeaponRange.Next();
                }
                else
                {
                    weapon.actionDice[targetWeaponRange].SetActionDice(tarStatus, WeaponType.Lead, parsedNumber);
                }
            }
            else if (text.Replace("(", string.Empty).Replace(")", string.Empty) == Defines.WeaponEmpty)
            {
                if (IsTextSupportDice(text))
                {
                    weapon.actionDice[targetWeaponRange].SetActionDice(tarStatus, WeaponType.Support, Defines.WeaponActionDiceEmptyDefaultValue);
                }
                else
                {
                    weapon.actionDice[targetWeaponRange].SetActionDice(tarStatus, WeaponType.Support, Defines.WeaponActionDiceEmptyDefaultValue);
                    weapon.actionDice[targetWeaponRange].SetActionDice(tarStatus, WeaponType.Lead, Defines.WeaponActionDiceEmptyDefaultValue);
                }

                targetWeaponRange = targetWeaponRange.Next();
            }

            // If done, exit dice parsing
            if (targetWeaponRange == WeaponRange.None)
            {
                if (currentStep == WeaponParsingStep.CrippledDice && weapon.IsDisabledWhenCrippled)
                {
                    return currentStep.Next();
                }
                else
                {
                    return currentStep.Next();
                }
            }
            else
            {
                return currentStep;
            }
        }

        private static void ParseWeaponQualities(string weaponQualitiesText, WeaponParsingStep currentStep, Weapon weapon, Dictionary<string?, CommonWeaponQuality> weaponQualities)
        {
            ModelStatus tarStatus = currentStep == WeaponParsingStep.BRQualities ? ModelStatus.Battle_Ready : ModelStatus.Crippled;
            var weaponQualityTokens = weaponQualitiesText.Split(',', StringSplitOptions.TrimEntries);

            weapon.qualities.Add(tarStatus, new List<WeaponQuality>());
            if (weaponQualitiesText != Defines.WeaponEmpty && !string.IsNullOrWhiteSpace(weaponQualitiesText))
            {
                foreach (var token in weaponQualityTokens)
                {
                    if (weaponQualities.ContainsKey(token))
                    {
                        weapon.qualities[tarStatus].Add(new WeaponQuality(weaponQualities[token]));
                    }
                    else
                    {
                        var matchedQuality = weaponQualities.Where(x => token.StartsWith(x.Key)).ToList();
                        if (matchedQuality.Count == 1)
                        {
                            var newQuality = new WeaponQuality(matchedQuality.First().Value, token);
                            weapon.qualities[tarStatus].Add(newQuality);
                        }
                        else if (matchedQuality.Count == 0)
                        {
                            var newQuality = new WeaponQuality(null, token);
                        }
                        else
                        {
                            int x = 0;
                        }
                    }
                }
            }
        }

        private static List<string> ParseWeaponsFromBlocks(IReadOnlyList<TextBlock> blocks)
        {
            return blocks.Where(x => !x.Text.Contains(Defines.WeaponLastTableHeader) && x.Text != Defines.WeaponEmpty
                && !x.Text.Contains(Defines.WeaponRefStartText) && !x.Text.Contains(Defines.WeaponText) && !x.Text.Contains(Defines.WeaponPointBlank)
                 && !x.Text.Contains(Defines.WeaponClosing) && !x.Text.Contains(Defines.WeaponLong) && !x.Text.Contains('\n')).Select(x => x.Text).ToList();
        }

        private static IList<string> GetWeaponQualitySets(IReadOnlyList<TextBlock> blocks)
        {
            IList<string> results = new List<string>();
            var weaponQualities = Enum.GetNames(typeof(CommonWeaponQuality));
            foreach (var block in blocks)
            {
                foreach (var qual in weaponQualities.Select(x => x.Replace("_", " ")))
                {
                    if (block.Text.Contains(qual))
                    {
                        // Any quality matches, add the segment to the list
                        foreach (var line in block.TextLines)
                        {
                            if (!results.Contains(line.Text))
                                results.Add(line.Text);
                        }

                        break;
                    }
                }
            }
            return results;
        }

    }
}
