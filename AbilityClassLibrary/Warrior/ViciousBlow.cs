﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Warrior
{
    public class ViciousBlow : Ability
    {
        public ViciousBlow()
        {
            Name = "Vicious Blow";
            Description = "Devastating attack, that is quaranteed to crit.";
            Cooldown = 2;
        }

        public int Action(int strength, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(strength * 1.5);
            return AttackLogic.CalculateAttackDamage(dmg, 100, multiplier, increase);
        }
    }
}
