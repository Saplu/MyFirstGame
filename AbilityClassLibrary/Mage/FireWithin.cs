using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Mage
{
    public class FireWithin : Ability
    {
        public FireWithin()
        {
            Name = "Fire Within";
            Description = "Burn your target from the inside. Damage cannot be resisted";
        }

        public int Action(int spellpower, int crit, double multiplier, int increase)
        {
            var dmg = 10 + spellpower;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
