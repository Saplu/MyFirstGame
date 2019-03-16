using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.NPC
{
    public class Bite : Ability
    {
        public Bite()
        {
            Name = "Bite";
            Description = "Bites off a chunk of his enemys flesh. Tiny bit.";
        }

        public int Action(int strength, int crit, double multiplier, int increase)
        {
            var dmg = 3 + strength;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
