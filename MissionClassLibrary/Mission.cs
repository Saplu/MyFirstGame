﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;
using CharacterClassLibrary.Enums;

namespace MissionClassLibrary
{
    [Serializable]
    public abstract class Mission
    {
        /*
        Kaikki taisteluun osallistuvat tyypit. Kaksi partya me vs te.
        Kaikki komennot joita taisteluun liittyy.
        StatusEffectJutut. Stun yms mitä mietittiin. Käsittely vuoron alussa ja lopussa.
        */

        private List<NPC> enemies;
        private List<Player> players;
        private int turn;

        public List<NPC> Enemies { get => enemies; set => enemies = value; }
        public List<Player> Players { get => players; set => players = value; }
        public int Turn { get => turn; set => turn = value; }

        public static Mission Create(MissionList missions, List<Player> players)
        {
            switch (missions)
            {
                case MissionList.Tutorial: return new Missions.Tutorial(players);
                case MissionList.NextStep: return new Missions.NextStep(players);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void EnemyDefend(int enemyIndex, int playerIndex, string id)
        {
            var dmg = players[playerIndex].UseAbility(id);
            enemies[enemyIndex].Defend(dmg);
        }

        public void PlayerDefend(int enemyIndex)
        {
            var dmg = enemies[enemyIndex].UseAbility();
            var defender = enemies[enemyIndex].ChooseEnemy(players);
            players[defender].Defend(dmg);
        }
    }
}