using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Protector
{
    public class Sweep : Ability
    {
        public Sweep()
        {
            Name = "Sweep";
            Description = "Hit every enemy anywhere like a rusty tin can. Increase your threat on everyone for a moderate amount.";
            Cooldown = 0;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(strength * 1.1);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
