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
            return Threat.ChooseEnemy();
        }
    }
}
