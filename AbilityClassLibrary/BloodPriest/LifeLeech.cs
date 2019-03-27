using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.BloodPriest
{
    public class LifeLeech : Ability
    {
        public LifeLeech()
        {
            Name = "Life Leech";
            Description = "Steal some life from the enemy to yourself";
            Cooldown = 1;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(spellPower * 1.4);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
