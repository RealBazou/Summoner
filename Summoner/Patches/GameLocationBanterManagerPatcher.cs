using System;
using HarmonyLib;

namespace Summoner.Patches
{

    public class ViciousMockeryInsults
    {
        public static string[] Insults = new string[12]
        {
            "What smells worse than a goblin? Oh yeah, you!",
            "Your mother takes up more tiles than a gelatinous cube!",
            "Your mother was a kobold and your father smelled of elderberry!",
            "Some day you'll go far and I hope you stay there!",
            "You're lucky to be born beautiful, unlike me, who was born to be a big liar!",
            "I'd like to leave you with one thought...but I'm not sure you have anywhere to put it!",
            "Would you like me to remove that curse? Oh my mistake, you were just born that way!",
            "There is no beholder's eye in which you are beautiful!",
            "I wish I still had that blindness spell, then I wouldn't have to endure that face anymore!",
            "I was thinking of casting feeblemind, but I doubt it would work on you!",
            "Your fighting stance looks like an unfolded lawn chair!",
            "It takes you 45 days to cook minute rice!"
        };
    }

        class GameLocationBanterManagerPatcher
    {

        [HarmonyPatch(typeof(GameLocationBanterManager), "SpellCast")]
        internal static class GameLocationBanterManager_SpellCast_Patch
        {
            internal static void Postfix(RulesetCharacter rulesetCaster, RulesetEffectSpell activeSpell)
            {
                if (rulesetCaster == null)
                    return;
                GameLocationCharacter fromActor = GameLocationCharacter.GetFromActor((RulesetActor)rulesetCaster);
                if (fromActor == null)
                    return;
                if (activeSpell.SpellDefinition.Name == "ViciousMockerySpell")
                {
                    var service = ServiceRepository.GetService<IGameLocationBanterService>();

                    // Instantiate random number generator.  
                    Random random = new Random();

                    string randomInsult = ViciousMockeryInsults.Insults[random.Next(0, 11)];

//                    service.TriggerEvent("MissedAttack", fromActor);
                    service.ForceBanterLine(ViciousMockeryInsults.Insults[random.Next(0,11)], fromActor);

                }

            }
        }
    }
}