using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary
{
    public static class AttackLogic
    {
        public static int CalculateAttackDamage(int baseDmg, int crit, double multiplier, int increase, Random random)
        {
            var dmg = (baseDmg * Convert.ToInt32(multiplier)) + increase;
            dmg = random.Next(Convert.ToInt32(dmg * .8), Convert.ToInt32(dmg * 1.2));
            if (isCrit(crit, random))
                return dmg * 2;
            else return dmg;
        }

        private static bool isCrit(int crit, Random random)
        {
            if (crit >= random.Next(1, 100))
                return true;
            else return false;
        }
    }
}
