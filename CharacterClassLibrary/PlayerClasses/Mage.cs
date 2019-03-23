using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Mage;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Mage : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        public Mage(string name)
        {
            Health = 80;
            MaxHealth = Health;
            Strength = 5;
            Crit = 0;
            SpellPower = 20;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Name = name;
            ClassName = Enums.ClassName.Mage;
            Items = new List<Item>();
            ItemTypes = new List<Enums.ItemType> { Enums.ItemType.Cloth };
            Statuses = new List<CombatLogicClassLibrary.Status>();
            Cooldowns = new int[4] { 0, 0, 0, 5 };
        }

        private int fireball()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var fireball = new Fireball();
            Cooldowns[0] = fireball.Cooldown;
            return fireball.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability1()
        {
            var fireball = new Fireball();
            return fireball.Info();
        }

        private int fireWithin()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var fire = new FireWithin();
            Cooldowns[1] = fire.Cooldown;
            return fire.Action(SpellPower, Crit, multi, increase);
        }

        public override string[] Ability2()
        {
            var fire = new FireWithin();
            return fire.Info();
        }

        public override string[] Ability3()
        {
            var fire = new FireWithin();
            return fire.Info();
        }

        public override string[] Ability4()
        {
            var fire = new FireWithin();
            return fire.Info();
        }

        public override void Defend(int incomingDmg)
        {
            var dmg = incomingDmg - (Armor / 4);
            if (dmg > 1)
                Health -= dmg;
            else Health -= 1;
        }

        public override int UseAbility(string id)
        {
            if (id == "Fireball")
                return fireball();
            else if (id == "Fire Within")
                return fireWithin();
            else return 1;
        }

        public override int GetTargets(string id)
        {
            if (id == "Fireball")
                return 1;
            else if (id == "Fire Within")
                return 1;
            else return 1;
        }

        public override string setPic()
        {
            return "Pictures\\welho.jpg";
        }

        public override List<int> setStatusTargets(string id)
        {
            var list = new List<int>();
            return list;
        }
    }
}
