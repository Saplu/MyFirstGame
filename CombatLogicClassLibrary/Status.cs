using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary
{
    [Serializable]
    public class Status
    {
        private StatusEnums name;
        private int duration;
        private List<int> targets;
        private double effect;

        public StatusEnums Name { get => name; set => name = value; }
        public int Duration { get => duration; set => duration = value; }
        public List<int> Targets { get => targets; set => targets = value; }
        public double Effect { get => effect; set => effect = value; }

        public static Status Create(string id)
        {
            switch (id)
            {
                case "Battle Cry": return new Statuses.AttackDmgMultiplier(1, new List<int>() { 1, 2, 3, 4 }, 1.2);
                default: return null;
            }
        }
    }
}
