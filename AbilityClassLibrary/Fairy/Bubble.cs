using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Fairy
{
    public class Bubble : Ability
    {
        public Bubble()
        {
            Name = "Bubble";
            Description = "Create a protecting bubble on a friendly player that absorbs some damage. Lasts maximum of 3 turns.";
            Cooldown = 3;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var value = spellPower * 2;
            return AttackLogic.CalculateAttackDamage(value, crit, multiplier, increase);
        }
    }
}
