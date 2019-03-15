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
        }

        private int fireball(Random random)
        {
            var fireball = new Fireball();
            return fireball.Action(SpellPower, Crit, 1, 0, random);
        }

        public override string[] Ability1()
        {
            var fireball = new Fireball();
            return fireball.Info();
        }

        private int fireWithin(Random random)
        {
            var fire = new FireWithin();
            return fire.Action(SpellPower, Crit, 1, 0, random);
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

        public override int UseAbility(string id, Random random)
        {
            if (id == "Fireball")
                return fireball(random);
            else if (id == "Fire Within")
                return fireWithin(random);
            else return 1;
        }
    }
}
