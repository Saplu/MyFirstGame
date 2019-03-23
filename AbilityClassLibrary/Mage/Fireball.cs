using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Mage
{
    public class Fireball : Ability
    {
        public Fireball()
        {
            Name = "Fireball";
            Description = "Create hot matter out of nowhere to burn your enemy.";
            Cooldown = 1;
        }

        public int Action(int spellpower, double crit, double multiplier, int increase)
        {
            var dmg = spellpower * 2;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
