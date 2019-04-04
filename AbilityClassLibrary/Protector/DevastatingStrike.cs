using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Protector
{
    public class DevastatingStrike : Ability
    {
        public DevastatingStrike()
        {
            Name = "Devastating Strike";
            Description = "An attack so scary, that it will permanently increase your threat by large amount.";
            Cooldown = 0;
        }

        public int Action(int strength, double crit, double multi, int increase)
        {
            var dmg = Convert.ToInt32(strength * 1.7);
            return AttackLogic.CalculateAttackDamage(dmg, crit, multi, increase);
        }
    }
}
