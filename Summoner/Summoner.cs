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

    internal class SummonPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string SummonPowerName = "Summon";
        const string SummonPowerNameGuid = "0a3e6a7d-4628-4189-b91d-d7146d774bb9";

        protected SummonPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerDomainLawForceOfLaw, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&SummonPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&SummonPowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.NoCost);
            Definition.SetShortTitleOverride("Feature/&SummonPowerTitle");
//            Definition.SetSpellcastingFeature(DatabaseHelper.FeatureDefinitionCastSpells.CastSpellWizard);

            EffectForm effectForm = new EffectForm();
            effectForm.FormType = EffectForm.EffectFormType.Summon;

            SummonForm summonForm = new SummonForm();
            effectForm.SetSummonForm(summonForm);

            MonsterDefinition monsterDefinition = new MonsterDefinition();
            monsterDefinition = PetBuilder.AddToMonsterList();
                        
            effectForm.SummonForm.SetMonsterDefinitionName(monsterDefinition.Name);
            //            effectForm.SummonForm.SetDecisionPackage(DatabaseHelper.DecisionPackageDefinitions.DefaultSupportCasterWithBackupAttacksDecisions);
            effectForm.SummonForm.SetDecisionPackage(null);

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(DatabaseHelper.SpellDefinitions.ConjureAnimalsOneBeast.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(effectForm);
            newEffectDescription.DurationType = RuleDefinitions.DurationType.UntilLongRest;
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
//            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Position);
//            newEffectDescription.SetRangeType(RuleDefinitions.RangeType.Distance);
            newEffectDescription.SetRangeParameter(24);
//            newEffectDescription.SetCanBePlacedOnCharacter(true);
//            newEffectDescription.SetCreatedByCharacter(true);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new SummonPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower SummonPower = CreateAndAddToDB(SummonPowerName, SummonPowerNameGuid);
    }

    internal class DominatePowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string DominatePowerName = "Dominate";
        const string DominatePowerNameGuid = "0a3e6a7d-4628-4189-b91d-d7146d774baa";

        protected DominatePowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerDomainLawForceOfLaw, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&DominatePowerTitle";
            Definition.GuiPresentation.Description = "Feature/&DominatePowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.NoCost);
            Definition.SetShortTitleOverride("Feature/&DominatePowerTitle");

            /*            EffectForm effectForm = new EffectForm();
                        effectForm.FormType = EffectForm.EffectFormType.Condition;

                        effectForm.ConditionForm = new ConditionForm();
                        effectForm.ConditionForm.SetConditionDefinitionName(DatabaseHelper.ConditionDefinitions.ConditionMindDominatedByCaster.Name);

                        //Add to our new effect
                        EffectDescription newEffectDescription = new EffectDescription();
                        newEffectDescription.Copy(DatabaseHelper.SpellDefinitions.DominatePerson.EffectDescription);
                        newEffectDescription.EffectForms.Clear();
                        newEffectDescription.EffectForms.Add(effectForm);
                        newEffectDescription.DurationType = RuleDefinitions.DurationType.UntilLongRest;
                        newEffectDescription.SetRangeParameter(24);
                        newEffectDescription.HasSavingThrow = false;
                        newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Individuals);
                        newEffectDescription.SetTargetSide(RuleDefinitions.Side.All);
                        newEffectDescription.RestrictedCreatureFamilies.Clear();

                        Definition.SetEffectDescription(newEffectDescription);
            */
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new DominatePowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower DominatePower = CreateAndAddToDB(DominatePowerName, DominatePowerNameGuid);
    }

    internal class PetBuilder : BaseDefinitionBuilder<MonsterDefinition>
    {
        const string PetName = "Pet";
        const string PetNameGuid = "99f1fb27-66af-49c6-b038-a38142b10831";

        protected PetBuilder(string name, string guid) : base(DatabaseHelper.MonsterDefinitions.GoldDragon_AerElai, name, guid)
        {
            Definition.GuiPresentation.Title = "Monster/&PetTitle";
            Definition.GuiPresentation.Description = "Monster/&PetDescription";

            Definition.Features.Add(DatabaseHelper.FeatureDefinitionFactionChanges.FactionChangeConditionMindDominatedByCaster);
        }

        public static MonsterDefinition CreateAndAddToDB(string name, string guid)
            => new PetBuilder(name, guid).AddToDB();

        public static MonsterDefinition Pet = CreateAndAddToDB(PetName, PetNameGuid);

        public static MonsterDefinition AddToMonsterList()
        {
            var Pet = PetBuilder.Pet;//Instantiating it adds to the DB
            return Pet;
        }


    }


    internal class SummonFeatBuilder : BaseDefinitionBuilder<FeatDefinition>
    {
        const string SummonFeatName = "SummonerFeat";
        const string SummonFeatNameGuid = "88f1fb27-66af-49c6-b038-a38142b10831";

        protected SummonFeatBuilder(string name, string guid) : base(DatabaseHelper.FeatDefinitions.FollowUpStrike, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&SummonFeatTitle";
            Definition.GuiPresentation.Description = "Feat/&SummonFeatDescription";

            Definition.Features.Clear();
            Definition.Features.Add(SummonPowerBuilder.SummonPower);
            Definition.Features.Add(DominatePowerBuilder.DominatePower);
            Definition.SetMinimalAbilityScorePrerequisite(false);
        }

        public static FeatDefinition CreateAndAddToDB(string name, string guid)
            => new SummonFeatBuilder(name, guid).AddToDB();

        public static FeatDefinition SummonFeat = CreateAndAddToDB(SummonFeatName, SummonFeatNameGuid);

        public static void AddToFeatList()
        {
            var SummonFeat = SummonFeatBuilder.SummonFeat;//Instantiating it adds to the DB
        }
    }

    internal class SummonPetSpellBuilder : BaseDefinitionBuilder<SpellDefinition>
    {
        const string SummonPetSpellName = "SummonPetSpell";
        const string SummonPetSpellNameGuid = "88f1fb27-66af-49c6-b038-a38142b10831";

        protected SummonPetSpellBuilder(string name, string guid) : base(DatabaseHelper.SpellDefinitions.ConjureAnimals, name, guid)
        {
            Definition.GuiPresentation.Title = "Spell/&SummonPetSpellTitle";
            Definition.GuiPresentation.Description = "Spell/&SummonPetSpellDescription";

//            Definition.SubspellsList = DatabaseHelper.SpellDefinitions.Summon
        }

        public static SpellDefinition CreateAndAddToDB(string name, string guid)
            => new SummonPetSpellBuilder(name, guid).AddToDB();

        public static SpellDefinition SummonPetSpell = CreateAndAddToDB(SummonPetSpellName, SummonPetSpellNameGuid);

        public static void AddToSpellList()
        {
            var SummonSpell = SummonPetSpellBuilder.SummonPetSpell;//Instantiating it adds to the DB
        }
    }

    internal class SummonWolfPetSpellBuilder : BaseDefinitionBuilder<SpellDefinition>
    {
        const string SummonWolfPetSpellName = "SummonWolfPetSpell";
        const string SummonWolfPetSpellNameGuid = "88f1fb27-66af-49c6-b038-a38142b10844";

        protected SummonWolfPetSpellBuilder(string name, string guid) : base(DatabaseHelper.SpellDefinitions.ConjureAnimalsOneBeast, name, guid)
        {
            Definition.GuiPresentation.Title = "Spell/&SummonWolfPetSpellTitle";
            Definition.GuiPresentation.Description = "Spell/&SummonWolfPetSpellDescription";

            Definition.SetSpellLevel(1);

            //            Definition.IsSubSpellOf(SummonPetSpellBuilder.SummonPetSpell);
            //Definition.EffectDescription.EffectForms.r;

            //Create the summon wolf effect
            EffectForm summonEffect = new EffectForm();
            SummonForm summonForm = new SummonForm();
            summonEffect.SetSummonForm(summonForm);
            summonEffect.FormType = EffectForm.EffectFormType.Summon;
            summonEffect.SummonForm.SetMonsterDefinitionName(DatabaseHelper.MonsterDefinitions.Beryl_Stonebeard.Name);
            summonEffect.SummonForm.SetDecisionPackage(DatabaseHelper.DecisionPackageDefinitions.DefaultSupportCasterWithBackupAttacksDecisions);

        }

        public static SpellDefinition CreateAndAddToDB(string name, string guid)
            => new SummonWolfPetSpellBuilder(name, guid).AddToDB();

        public static SpellDefinition SummonWolfPetSpell = CreateAndAddToDB(SummonWolfPetSpellName, SummonWolfPetSpellNameGuid);

        public static void AddToSpellList()
        {
            var SummonSpell = SummonPetSpellBuilder.SummonPetSpell;//Instantiating it adds to the DB
        }
    }


}