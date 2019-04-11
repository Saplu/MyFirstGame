using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Rogue
{
    public class Jawbreaker : Ability
    {
        public Jawbreaker()
        {
            Name = "Jawbreaker";
            Description = "Cost: 50 energy. Direct hit to targets jaw, stunning it for 1 turn. Applies poison and grants 1 combo point.";
            Cooldown = 4;
        }

        public int Action(int Strength, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(Strength * .8);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
