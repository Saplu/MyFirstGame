using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Protector
{
    public class TauntingBlow : Ability
    {
        public TauntingBlow()
        {
            Name = "Taunting Blow";
            Description = "Hurts your targets feelings enough to force it target all abilities on you for two turns." +
                "Also permanently increases chance target will attack you for a little amount.";
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            return AttackLogic.CalculateAttackDamage(strength, crit, multiplier, increase);
        }
    }
}
