﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatLogicClassLibrary;
using System.Threading.Tasks;

namespace AbilityClassLibrary.Rogue
{
    public class Stab : Ability
    {
        public Stab()
        {
            Name = "Stab";
            Description = "Stab your enemy, apply poison, gain 1 combo point and 20 energy.";
            Cooldown = 1;
        }

        public int Action(int strength, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(strength * 1.2);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }

        public int Poison(int strength)
        {
            var dmg = Convert.ToInt32(strength * .25);
            return AttackLogic.CalculateAttackDamage(dmg, 0, 1, 0);
        }
    }
}
