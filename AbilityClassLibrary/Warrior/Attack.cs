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
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = strength * 2;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
