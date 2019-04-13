using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Templar
{
    public class RighteousBane : Ability
    {
        public RighteousBane()
        {
            Name = "Righteous Bane";
            Description = "Remove all evil powers from your target, hurting him and reducing all damage he deals by 50% for 2 turns." +
                "Also increases your threat.";
            Cooldown = 5;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var dmg = spellPower * 3;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
