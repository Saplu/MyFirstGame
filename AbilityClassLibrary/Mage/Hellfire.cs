using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Mage
{
    public class Hellfire : Ability
    {
        public Hellfire()
        {
            Name = "Hellfire";
            Description = "Unleash hell on your enemies, burning them with really hot fire. Leaves a DoT on your main target for 3 turns.";
        }

        public int Action(int sp, double crit, double multiplier, int increase)
        {
            var dmg = sp * 2;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }

        public int DoT(int sp, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(sp * .7);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
