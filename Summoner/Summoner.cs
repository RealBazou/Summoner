using SolastaModApi;
using SolastaModApi.Extensions;

namespace Summoner
{
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

            EffectForm effectForm = new EffectForm();
            effectForm.FormType = EffectForm.EffectFormType.Summon;
            effectForm.SetCreatedByCharacter(true);

            SummonForm summonForm = new SummonForm();
            effectForm.SetSummonForm(summonForm);

            MonsterDefinition monsterDefinition = new MonsterDefinition();
            monsterDefinition = PetBuilder.AddToMonsterList();
                        
            effectForm.SummonForm.SetMonsterDefinitionName(monsterDefinition.Name);
            effectForm.SummonForm.SetDecisionPackage(null);

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(DatabaseHelper.SpellDefinitions.ConjureAnimalsOneBeast.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(effectForm);
            newEffectDescription.DurationType = RuleDefinitions.DurationType.UntilLongRest;
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
            newEffectDescription.SetRangeParameter(24);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new SummonPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower SummonPower = CreateAndAddToDB(SummonPowerName, SummonPowerNameGuid);
    }

    internal class PetBuilder : BaseDefinitionBuilder<MonsterDefinition>
    {
        const string PetName = "Pet";
        const string PetNameGuid = "99f1fb27-66af-49c6-b038-a38142b10831";

        protected PetBuilder(string name, string guid) : base(DatabaseHelper.MonsterDefinitions.StarvingWolf, name, guid)
        {
            Definition.GuiPresentation.Title = "Monster/&PetTitle";
            Definition.GuiPresentation.Description = "Monster/&PetDescription";

            Definition.SetDefaultFaction("Party");
            Definition.SetFullyControlledWhenAllied(true);
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

}