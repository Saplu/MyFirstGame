﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterClassLibrary
{
    [Serializable]
    public abstract class NPC : Character
    {
        public abstract void Defend(int incomingDmg);
        public abstract int UseAbility();
        public int ChooseEnemy(List<Player> players)
        {
            return Utils.RandomProvider.GetRandom(0, players.Count - 1);
        }
    }
}
