using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.NPC;

namespace CharacterClassLibrary.NPCClasses
{
    [Serializable]
    public class Rabbit : NPC, Interfaces.CombatInterface
    {
        public Rabbit(int level)
        {
            Level = level;
            Health = 80 + (level * 25);
            MaxHealth = Health;
            Strength = 8 + (level * 2);
            Crit = 10;
            SpellPower = 0;
            Armor = 5 + (level * 3);
        }

        private int kick(Random random)
        {
            var kick = new Kick();
            return kick.Action(Strength, Crit, 1, 0, random);
        }

        private int bite(Random random)
        {
            var bite = new Bite();
            return bite.Action(Strength, Crit, 1, 0, random);
        }

        public override void Defend(int incomingDmg)
        {
            var dmg = incomingDmg - (Armor / 4);
            if (dmg > 1)
                Health -= dmg;
            else Health -= 1;
        }

        public override int UseAbility(Random random)
        {
            if (random.Next() > 50)
                return kick(random);
            else return bite(random);
        }
    }
}
