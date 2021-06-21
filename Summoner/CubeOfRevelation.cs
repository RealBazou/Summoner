using SolastaModApi;
using SolastaModApi.Extensions;

namespace Summoner
{
    internal class CubeOfRevelationsMonsterBuilder : BaseDefinitionBuilder<MonsterDefinition>
    {
        const string CubeOfRevelationsMonsterName = "CubeOfRevelations";
        const string CubeOfRevelationsMonsterNameGuid = "0a3e6a7d-4628-4189-b91d-d7146d774bb7";

        protected CubeOfRevelationsMonsterBuilder(string name, string guid) : base(DatabaseHelper.MonsterDefinitions.CubeOfLight, name, guid)
        {
            Definition.GuiPresentation.Title = "CubeOfRevelations";
            Definition.GuiPresentation.Description = "You are about to face death or ultimate enlightenment.";

            int[] abilityScores = new int[] { 20, 20, 20, 20, 20, 20 };
            Definition.SetAbilityScores(abilityScores);
            Definition.SetAlignment("neutral");
            Definition.SetArmorClass(20);
            Definition.SizeDefinition = DatabaseHelper.CharacterSizeDefinitions.DragonSize;
            Definition.SetDefaultBattleDecisionPackage(DatabaseHelper.DecisionPackageDefinitions.DefaultSupportCasterWithBackupAttacksDecisions);
            Definition.SetInDungeonEditor(true);
            Definition.SetFullyControlledWhenAllied(true);
            Definition.SetDefaultFaction("Party");
            Definition.MonsterPresentation.SetMaleModelScale(3);
        }

        public static MonsterDefinition CreateAndAddToDB(string name, string guid)
            => new CubeOfRevelationsMonsterBuilder(name, guid).AddToDB();

        public static MonsterDefinition CubeOfRevelationsMonster = CreateAndAddToDB(CubeOfRevelationsMonsterName, CubeOfRevelationsMonsterNameGuid);

        public static void AddToMonsterList()
        {
            var cubeOfRevelationsMonster = CubeOfRevelationsMonsterBuilder.CubeOfRevelationsMonster;//Instantiating it adds to the DB
        }

    }

}