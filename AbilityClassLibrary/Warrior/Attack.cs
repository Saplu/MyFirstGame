using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Warrior
{
    public class Attack : Ability
    {
        public Attack()
        {
            Name = "Attack";
            Description = "Fierce attack against a single target.";
        }

        public int Action(int strength, int crit, double multiplier, int increase)
        {
            var dmg = strength * 2;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
