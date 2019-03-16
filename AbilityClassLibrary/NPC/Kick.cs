using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.NPC
{
    public class Kick : Ability
    {
        public Kick()
        {
            Name = "Kick";
            Description = "Kicks the enemy with his tiny paws.";
        }

        public int Action(int strength, int crit, double multiplier, int increase)
        {
            var dmg = 5 + Convert.ToInt32(strength * 1.2);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
