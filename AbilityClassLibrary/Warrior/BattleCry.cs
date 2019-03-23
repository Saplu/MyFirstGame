using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace AbilityClassLibrary.Warrior
{
    public class BattleCry : Ability
    {
        public BattleCry()
        {
            Name = "Battle Cry";
            Description = "Deals light damage to every enemy and increases damage of all party members by 20% for this turn.";
            Cooldown = 2;
        }

        public int Action(int strength, double crit, double multiplier, int increase)
        {
            var dmg = Convert.ToInt32(strength / 2);            
            return AttackLogic.CalculateAttackDamage(dmg, crit, multiplier, increase);
        }

        public Status ApplyStatus()
        {
            var list = new List<int>() { 1, 2, 3, 4 };
            var status = new CombatLogicClassLibrary.Statuses.AttackDmgMultiplier(1, list, 1.2);
            return status;
        }
    }
}
