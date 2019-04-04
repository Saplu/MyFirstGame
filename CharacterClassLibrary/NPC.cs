using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace CharacterClassLibrary
{
    [Serializable]
    public abstract class NPC : Character
    {
        private Threat threat;

        public Threat Threat { get => threat; set => threat = value; }

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
    }
}
