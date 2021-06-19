using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityModManagerNet;
using HarmonyLib;
using I2.Loc;
using SolastaModApi;
using ModMaker;
using ModMaker.Utility;

namespace Summoner
{
    public class Main
    {
        public static readonly string MOD_FOLDER = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [Conditional("DEBUG")]
        internal static void Log(string msg) => Logger.Log(msg);
        internal static void Error(Exception ex) => Logger?.Error(ex.ToString());
        internal static void Error(string msg) => Logger?.Error(msg);
        internal static void Warning(string msg) => Logger?.Warning(msg);
        internal static UnityModManager.ModEntry.ModLogger Logger { get; private set; }

        internal static void LoadTranslations()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(MOD_FOLDER);
            FileInfo[] files = directoryInfo.GetFiles($"Translations-??.txt");

            foreach (var file in files)
            {
                var filename = Path.Combine(MOD_FOLDER, file.Name);
                var code = file.Name.Substring(13, 2);
                var languageSourceData = LocalizationManager.Sources[0];
                var languageIndex = languageSourceData.GetLanguageIndexFromCode(code);

                if (languageIndex < 0)
                    Error($"language {code} not currently loaded.");
                else
                    using (var sr = new StreamReader(filename))
                    {
                        String line, term, text;
                        while ((line = sr.ReadLine()) != null)
                        {
                            try
                            {
                                var splitted = line.Split(new[] { '\t', ' ' }, 2);
                                term = splitted[0];
                                text = splitted[1];
                            } catch
                            {
                                Error($"invalid translation line \"{line}\".");
                                continue;
                            }
                            if (languageSourceData.ContainsTerm(term))
                            {
                                languageSourceData.RemoveTerm(term);
                                Warning($"official game term {term} was overwritten with \"{text}\"");
                            }
                            languageSourceData.AddTerm(term).Languages[languageIndex] = text;
                        }
                    }
            }
        }

        internal static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Logger = modEntry.Logger;

                LoadTranslations();

                modEntry.OnGUI = OnGUI;

                var harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Error(ex);
                throw;
            }

            return true;
        }

        internal static void ModEntryPoint()
        {
            //CubeOfRevelationsMonsterBuilder.AddToMonsterList();
            SummonFeatBuilder.AddToFeatList();
            //SummonPetSpellBuilder.AddToSpellList();
            //SummonWolfPetSpellBuilder.AddToSpellList();
            ViciousMockerySpellBuilder.AddToSpellList();

        }

        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            // example: basic UI
            // read https://github.com/cabarius/ToyBox for better examples
            // 
            UI.Section("Summoner", () =>
            {
                UI.HStack("Sample Stack A", 4,
                    () => { UI.ActionButton("AdjustAllegiances", () => {

                            var party = ServiceRepository.GetService<IGameLocationCharacterService>()?.PartyCharacters;
                            var guests = ServiceRepository.GetService<IGameLocationCharacterService>()?.GuestCharacters;
                            var validEntities = ServiceRepository.GetService<IGameLocationCharacterService>()?.AllValidEntities;
                            var playerContenders = ServiceRepository.GetService<IGameLocationBattleService>()?.Battle.PlayerContenders;
                            var enemyContenders = ServiceRepository.GetService<IGameLocationBattleService>()?.Battle.EnemyContenders;

                            for (var index = 0; index < validEntities.Count; index++)
                            {
                                if (validEntities[index].Name == "Monster/&PetTitle")
                                {
                                    if (!guests.Contains(validEntities[index]))
                                        guests.Add(validEntities[index]);

                                    if (!playerContenders.Contains(validEntities[index]))
                                        playerContenders.Add(validEntities[index]);

                                    if (enemyContenders.Contains(validEntities[index]))
                                        enemyContenders.Remove(validEntities[index]);

                                    validEntities[index].ControllerId = 0;
                                    validEntities[index].ChangeSide(RuleDefinitions.Side.Ally);
//                                    RulesetCharacterHero rulesetCharacterHero = new RulesetCharacterHero();
//                                    validEntities[index].SetRuleset(rulesetCharacterHero);
                            }

                            }

                            ServiceRepository.GetService<IGameLocationCharacterService>()?.RefreshAllCharacters();

                    }); },
                    () => { UI.ActionButton("Button 2", () => { /* do something here */ }); }
                 );
                UI.Space(10);
                UI.HStack("Sample Stack B", 4,
                    () => { UI.ActionButton("Button 3", () => { /* do something here */ }); },
                    () => { UI.ActionButton("Button 4", () => { /* do something here */ }); }
                 );
            });
        }
    }
}
