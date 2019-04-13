using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Templar
{
    public class HolyGround : Ability
    {
        public HolyGround()
        {
            Name = "Holy Ground";
            Description = "Bless the battlefield. Deals damage to all enemies each turn for 3 turns and instantly adds a ton of threat.";
            Cooldown = 3;
        }

        public int Action(int spellPower, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(spellPower * .8);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
