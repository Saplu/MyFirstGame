using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.Warrior;

namespace CharacterClassLibrary.PlayerClasses
{
    [Serializable]
    public class Warrior : Player, Interfaces.CombatInterface, Interfaces.PlayerInterface
    {
        public Warrior(string name)
        {
            Health = 100;
            MaxHealth = Health;
            Strength = 10;
            Crit = 0;
            SpellPower = 0;
            Armor = 0;
            Level = 1;
            Xp = 0;
            Items = new List<Item>();
            Name = name;
            ClassName = Enums.ClassName.Warrior;
            ItemTypes = new List<Enums.ItemType>
            { Enums.ItemType.Cloth, Enums.ItemType.Leather, Enums.ItemType.Mail, Enums.ItemType.Plate };
        }

        private int attack()
        {
            var attack = new Attack();
            return attack.Action(Strength, Crit, 1, 0);
        }

        public override string[] Ability1()
        {
            var attack = new Attack();
            return attack.Info();
        }

        private int viciousBlow()
        {
            var vicious = new ViciousBlow();
            return vicious.Action(Strength, 1, 0);
        }

        public override string[] Ability2()
        {
            var vicious = new ViciousBlow();
            return vicious.Info();
        }

        public override string[] Ability3()
        {
            var attack = new Attack();
            return attack.Info();
        }

        public override string[] Ability4()
        {
            var attack = new Attack();
            return attack.Info();
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
            if (id == "Attack")
                return attack();
            else if (id == "Vicious Blow")
                return viciousBlow();
            else return 1;
        }
    }
}
