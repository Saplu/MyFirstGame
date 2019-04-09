using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Shaman
{
    public class ChainLightning : Ability
    {
        public ChainLightning()
        {
            Name = "Chain Lightning";
            Description = "Strike up to 3 enemies with lightning.";
            Cooldown = 3;
        }

        public int Action(int SpellPower, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(SpellPower * 1.7);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
