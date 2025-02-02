﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Templar;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Templar : Player, Interfaces.PlayerInterface, Interfaces.CombatInterface
    {
        public Templar(string name)
        {
            Health = 120;
            MaxHealth = Health;
            Strength = 5;
            Crit = 0;
            SpellPower = 15;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Items = new List<Item>();
            Name = name;
            ClassName = Enums.ClassName.Templar;
            ItemTypes = new List<Enums.ItemType>()
            { Enums.ItemType.Cloth, Enums.ItemType.Leather, Enums.ItemType.Mail, Enums.ItemType.Plate};
            var stat = new CombatLogicClassLibrary.Statuses.TakenDmgMultiplier(Int32.MaxValue, new List<int>(), .85);
            Statuses = new List<CombatLogicClassLibrary.Status>() { stat };
            Cooldowns = new int[4] { 0, 0, 0, 4 };
        }

        private int sacredThrust()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var sacred = new SacredThrust();
            Cooldowns[0] = sacred.Cooldown;
            return sacred.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var sacred = new SacredThrust();
            return sacred.Info();
        }

        private int holyGround()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var holy = new HolyGround();
            Cooldowns[1] = holy.Cooldown;
            return holy.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability2()
        {
            var holy = new HolyGround();
            return holy.Info();
        }

        private int holyShock()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var shock = new HolyShock();
            Cooldowns[2] = shock.Cooldown;
            return shock.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability3()
        {
            var shock = new HolyShock();
            return shock.Info();
        }

        private int righteousBane()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var bane = new RighteousBane();
            Cooldowns[3] = bane.Cooldown;
            return bane.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability4()
        {
            var bane = new RighteousBane();
            return bane.Info();
        }

        public override int UseAbility(string id)
        {
            switch(id)
            {
                case "Sacred Thrust": return sacredThrust();
                case "Holy Ground": return holyGround();
                case "Holy Shock": return holyShock();
                case "Righteous Bane": return righteousBane();
                default: return 1;
            }
        }

        public override int GetTargets(string id)
        {
            switch(id)
            {
                case "Sacred Thrust": return 1;
                case "Holy Ground": return 4;
                case "Holy Shock": return 3;
                case "Righteous Bane": return 1;
                default: return 1;
            }
        }

        public override string setPic()
        {
            return "paladin";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var util = new Utils.TargetSetter();
            var list = new List<int>();

            switch(id)
            {
                case "Holy Ground": list = util.setTargets(targetPosition, 4, enemyCount); return list;
                case "Holy Shock": list = util.setTargets(targetPosition, 1, enemyCount); return list;
                case "Righteous Bane": list = util.setTargets(targetPosition, 1, enemyCount); return list;
                default: return list;
            }
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            switch(id)
            {
                case "Holy Ground": return holyGround();
                case "Holy Shock": return this.Position;
                case "Righteous Bane": return .5;
                default: return 0;
            }
        }

        public override int GetThreat(string id)
        {
            switch(id)
            {
                case "Sacred Thrust": return 15;
                case "Holy Ground": return 12;
                case "Holy Shock": return 9;
                case "Righteous Bane": return 12;
                default: return 0;
            }
        }

        public override void AfterCombatReset()
        {
            Health = MaxHealth;
            Cooldowns = new int[4] { 0, 0, 0, 4 };
            var stat = new CombatLogicClassLibrary.Statuses.TakenDmgMultiplier(Int32.MaxValue, new List<int>(), .85);
            Statuses = new List<CombatLogicClassLibrary.Status>() { stat };
        }
    }
}
