using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Shaman
{
    public class Thunder : Ability
    {
        public Thunder()
        {
            Name = "Thunder";
            Description = "Create a storm on enemy group damaging everyone and stunning the primary target for 1 turn.";
            Cooldown = 5;
        }

        public int Action(int SpellPower, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(SpellPower * 1.2);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
