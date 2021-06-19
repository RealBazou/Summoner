using SolastaModApi;
using SolastaModApi.Extensions;

namespace Summoner
{
    internal class ViciousMockerySpellBuilder : BaseDefinitionBuilder<SpellDefinition>
    {
        const string SpellName = "ViciousMockerySpell";
        const string SpellNameGuid = "88f1fb27-66af-49c6-b038-a38142b11234";

        protected ViciousMockerySpellBuilder(string name, string guid) : base(DatabaseHelper.SpellDefinitions.ShadowDagger, name, guid)
        {
            Definition.GuiPresentation.Title = "Spell/&ViciousMockeryTitle";
            Definition.GuiPresentation.Description = "Spell/&ViciousMockeryDescription";
            Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.HideousLaughter.GuiPresentation.SpriteReference);

            Definition.SetSchoolOfMagic("SchoolEnchantment");
            Definition.SetVerboseComponent(true);
            Definition.SetSomaticComponent(false);
            Definition.SetMaterialComponentType(RuleDefinitions.MaterialComponentType.None);

            EffectDescription effectDescription = Definition.EffectDescription;
            effectDescription.DurationType = RuleDefinitions.DurationType.Round;
            effectDescription.SetRangeParameter(12);
            effectDescription.EffectParticleParameters.Clear();
            effectDescription.SetEffectParticleParameters(DatabaseHelper.SpellDefinitions.HideousLaughter.EffectDescription.EffectParticleParameters);

            EffectForm mainEffectForm = effectDescription.EffectForms[0];
            mainEffectForm.DamageForm.DieType = RuleDefinitions.DieType.D4;

            EffectForm debuffEffectForm = new EffectForm();
            debuffEffectForm.FormType = EffectForm.EffectFormType.Condition;
            debuffEffectForm.SetCreatedByCharacter(true);
            debuffEffectForm.HasSavingThrow = true;
            debuffEffectForm.SavingThrowAffinity = RuleDefinitions.EffectSavingThrowType.Negates;

            ConditionDefinition conditionDefinition = new ConditionDefinition();
            conditionDefinition = ViciousMockeryConditionBuilder.AddToConditionList();

            ConditionForm conditionForm = new ConditionForm();
            conditionForm.ConditionDefinition = conditionDefinition;
            conditionForm.SetConditionDefinitionName(conditionDefinition.Name);

            debuffEffectForm.ConditionForm = conditionForm;
            effectDescription.EffectForms.Add(debuffEffectForm);

//            BanterEventDefinition banterEventDefinition = new BanterEventDefinition();
//            banterEventDefinition = ViciousMockeryBanterEventDefinitionBuilder.AddToBanterEventDefinitionList();

        }

        public static SpellDefinition CreateAndAddToDB(string name, string guid)
            => new ViciousMockerySpellBuilder(name, guid).AddToDB();

        public static SpellDefinition ViciousMockerySpell = CreateAndAddToDB(SpellName, SpellNameGuid);

        public static void AddToSpellList()
        {
            var ViciousMockerySpell = ViciousMockerySpellBuilder.ViciousMockerySpell;//Instantiating it adds to the DB
            DatabaseHelper.SpellListDefinitions.SpellListWizard.SpellsByLevel[0].Spells.Add(ViciousMockerySpell);
        }
    }

    internal class ViciousMockeryConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string ConditionName = "ViciousMockeryCondition";
        const string ConditionNameGuid = "1231fb27-66af-49c6-b038-a38142b11234";

        protected ViciousMockeryConditionBuilder(string name, string guid) : base(DatabaseHelper.ConditionDefinitions.ConditionTargetedByGuidingBolt, name, guid)
        {
            Definition.GuiPresentation.Title = "Condition/&ViciousMockeryTitle";
            Definition.GuiPresentation.Description = "Condition/&ViciousMockeryDescription";

            Definition.SpecialInterruptions.Clear();
            Definition.SpecialInterruptions.Add(RuleDefinitions.ConditionInterruption.Attacks);
            Definition.SetTerminateWhenRemoved(true);
            Definition.Features.Clear();

            FeatureDefinitionCombatAffinity featureDefinitionCombatAffinity = new FeatureDefinitionCombatAffinity();
            featureDefinitionCombatAffinity = ViciousMockeryFeatureDefinitionCombatAffinityBuilder.AddToFeatureDefinitionCombatAffinityList();

            Definition.Features.Add(featureDefinitionCombatAffinity);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new ViciousMockeryConditionBuilder(name, guid).AddToDB();

        public static ConditionDefinition ViciousMockeryCondition = CreateAndAddToDB(ConditionName, ConditionNameGuid);

        public static ConditionDefinition AddToConditionList()
        {
            var ViciousMockeryCondition = ViciousMockeryConditionBuilder.ViciousMockeryCondition;//Instantiating it adds to the DB
            return ViciousMockeryCondition;
        }
    }

    internal class ViciousMockeryFeatureDefinitionCombatAffinityBuilder : BaseDefinitionBuilder<FeatureDefinitionCombatAffinity>
    {
        const string FeatureDefinitionCombatAffinityName = "ViciousMockeryFeatureDefinitionCombatAffinity";
        const string FeatureDefinitionCombatAffinityNameGuid = "123aab27-66af-49c6-b038-a38142b11234";

        protected ViciousMockeryFeatureDefinitionCombatAffinityBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionCombatAffinitys.CombatAffinityPoisoned, name, guid)
        {
            Definition.GuiPresentation.Title = "FeatureDefinitionCombatAffinity/&ViciousMockeryTitle";
            Definition.GuiPresentation.Description = "FeatureDefinitionCombatAffinity/&ViciousMockeryDescription";

            Definition.SetMyAttackAdvantage(RuleDefinitions.AdvantageType.Disadvantage);
        }

        public static FeatureDefinitionCombatAffinity CreateAndAddToDB(string name, string guid)
            => new ViciousMockeryFeatureDefinitionCombatAffinityBuilder(name, guid).AddToDB();

        public static FeatureDefinitionCombatAffinity ViciousMockeryFeatureDefinitionCombatAffinity = CreateAndAddToDB(FeatureDefinitionCombatAffinityName, FeatureDefinitionCombatAffinityNameGuid);

        public static FeatureDefinitionCombatAffinity AddToFeatureDefinitionCombatAffinityList()
        {
            var ViciousMockeryFeatureDefinitionCombatAffinity = ViciousMockeryFeatureDefinitionCombatAffinityBuilder.ViciousMockeryFeatureDefinitionCombatAffinity;//Instantiating it adds to the DB
            return ViciousMockeryFeatureDefinitionCombatAffinity;
        }
    }

/*
    internal class ViciousMockeryBanterEventDefinitionBuilder : BaseDefinitionBuilder<BanterEventDefinition>
    {
        const string BanterDefinitionName = "ViciousMockeryBanterDefinition";
        const string BanterDefinitionNameGuid = "123bbb27-66af-49c6-b038-a38142b11234";

        protected ViciousMockeryBanterEventDefinitionBuilder(string name, string guid) : base(DatabaseHelper.BanterEventDefinitions.BanterEventMissedAttack, name, guid)
        {
            Definition.GuiPresentation.Title = "BanterEventDefinition/&ViciousMockeryTitle";
            Definition.GuiPresentation.Description = "BanterEventDefinition/&ViciousMockeryDescription";

            Definition.SetEventTrigger("CastingViciousMockery");
            Definition.SetSearchKey("Casting_ViciousMockery");

        }

        public static BanterEventDefinition CreateAndAddToDB(string name, string guid)
            => new ViciousMockeryBanterEventDefinitionBuilder(name, guid).AddToDB();

        public static BanterEventDefinition ViciousMockeryBanterEventDefinition = CreateAndAddToDB(BanterDefinitionName, BanterDefinitionNameGuid);

        public static BanterEventDefinition AddToBanterEventDefinitionList()
        {
            var ViciousMockeryBanterEventDefinition = ViciousMockeryBanterEventDefinitionBuilder.ViciousMockeryBanterEventDefinition;//Instantiating it adds to the DB
            return ViciousMockeryBanterEventDefinition;
        }
    }
*/

}