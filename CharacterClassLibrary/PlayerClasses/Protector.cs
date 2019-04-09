using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Protector;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Protector : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        public Protector(string name)
        {
            Health = 120;
            MaxHealth = Health;
            Strength = 8;
            Crit = 0;
            SpellPower = 0;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Items = new List<Item>();
            Name = name;
            ClassName = Enums.ClassName.Protector;
            ItemTypes = new List<Enums.ItemType>
            { Enums.ItemType.Cloth, Enums.ItemType.Leather, Enums.ItemType.Mail, Enums.ItemType.Plate };
            var stat = new CombatLogicClassLibrary.Statuses.TakenDmgMultiplier(Int32.MaxValue, new List<int>(), .85);
            Statuses = new List<CombatLogicClassLibrary.Status>() { stat };
            Cooldowns = new int[4] { 0, 0, 0, 4 };
        }

        private int tauntingBlow()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var taunt = new TauntingBlow();
            Cooldowns[0] = taunt.Cooldown;
            return taunt.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var taunt = new TauntingBlow();
            return taunt.Info();
        }

        private int devastatingStrike()
        {
            var increase = getAttackModifier();
            var multi = getAttackMultiplier();
            var devastate = new DevastatingStrike();
            Cooldowns[1] = devastate.Cooldown;
            return devastate.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability2()
        {
            var devastate = new DevastatingStrike();
            return devastate.Info();
        }

        private int sweep()
        {
            var increase = getAttackModifier();
            var multi = getAttackMultiplier();
            var sweep = new Sweep();
            Cooldowns[2] = sweep.Cooldown;
            return sweep.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability3()
        {
            var sweep = new Sweep();
            return sweep.Info();
        }

        private int challengingShout()
        {
            var increase = getAttackModifier();
            var multi = getAttackMultiplier();
            var challenge = new ChallengingShout();
            Cooldowns[3] = challenge.Cooldown;
            Statuses.Add(challenge.applySelfStatus());
            return challenge.Action(Strength, Crit, multi, increase);
        }

        public override string[] Ability4()
        {
            var challenge = new ChallengingShout();
            return challenge.Info();
        }

        public override int UseAbility(string id)
        {
            if (id == "Taunting Blow")
                return tauntingBlow();
            else if (id == "Devastating Strike")
                return devastatingStrike();
            else if (id == "Sweep")
                return sweep();
            else if (id == "Challenging Shout")
                return challengingShout();
            else return 1;
        }

        public override int GetTargets(string id)
        {
            if (id == "Taunting Blow")
                return 1;
            if (id == "Devastating Strike")
                return 1;
            else if (id == "Sweep")
                return 4;
            else if (id == "Challenging Shout")
                return 4;
            else return 1;
        }

        public override string setPic()
        {
            return "Pictures\\Tankki.jpg";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            if (id == "Taunting Blow")
            {
                var util = new Utils.TargetSetter();
                list = util.setTargets(targetPosition, 1, enemyCount);
            }
            if (id == "Challenging Shout")
            {
                var util = new Utils.TargetSetter();
                list = util.setTargets(targetPosition, 4, enemyCount);
            }
            return list;
        }

        public override double setStatusEffect(string id, int targetPosition)
        {
            if (id == "Taunting Blow")
                return this.Position;
            if (id == "Challenging Shout")
                return this.Position;
            else return 0;
        }

        public override int GetThreat(string id)
        {
            if (id == "Taunting Blow")
                return 3;
            if (id == "Devastating Strike")
                return 12;
            if (id == "Sweep")
                return 6;
            if (id == "Challenging Shout")
                return 6;
            else return 0;
        }
    }
}
