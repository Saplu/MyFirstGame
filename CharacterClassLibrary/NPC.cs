using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary.Enums;
using CombatLogicClassLibrary;

namespace CharacterClassLibrary
{
    [Serializable]
    public abstract class NPC : Character
    {
        private Threat threat;
        private Enums.NPCType type;

        public Threat Threat { get => threat; set => threat = value; }
        public NPCType Type { get => type; set => type = value; }

        public abstract string ChooseAbility();

        public int ChooseEnemy()
        {
            foreach (var status in Statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.Taunt)
                    return Convert.ToInt32(status.Effect);
            }
            return Threat.ChooseEnemy();
        }

        public void ManageThreat(int index, int amount)
        {
            Threat.ManageThreat(index, amount);
        }

        public void ManageThreat(int index)
        {
            Threat.ManageThreat(index, -Threat.ThreatTable[index]);
        }

        protected double typeMultiplier()
        {
            switch (Type)
            {
                case Enums.NPCType.Recruit: return .7;
                case Enums.NPCType.Normal: return 1;
                case Enums.NPCType.Veteran: return 1.3;
                case Enums.NPCType.Elite: return 1.7;
                default: return 1;
            }
        }
    }
}
