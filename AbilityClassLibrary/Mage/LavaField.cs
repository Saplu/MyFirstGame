using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Mage
{
    public class LavaField : Ability
    {
        public LavaField()
        {
            Name = "Lava Field";
            Description = "Instantly burn up to three enemies, and then again in the end of the turn.";
            Cooldown = 3;
        }

        public int Action(int spellpower, double crit, double multiplier, int increase)
        {
            var dmg = 5 + spellpower;
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }
    }
}
