using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Rogue
{
    public class Mutilate : Ability
    {
        public Mutilate()
        {
            Name = "Mutilate";
            Description = "Cost: 60 energy. Attack for twice the dmg, apply two stacks of poison and gain two combo points.";
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(strength * 2.2);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
