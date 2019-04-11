using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Rogue
{
    public class BladeFlurry : Ability
    {
        public BladeFlurry()
        {
            Name = "Blade Flurry";
            Description = "Cost: 40 energy. Consume all your combo points and deal damage for each point consumed." +
                "Applies poison. With 5 points applies also one stack to every other enemy.";
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multi, int increase, int combo)
        {
            var dmg = Convert.ToInt32(strength * combo * .8);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
