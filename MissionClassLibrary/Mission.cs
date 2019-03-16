using System;
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
                case MissionList.FirstChallenge: return new Missions.FirstChallenge(players);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void EnemyDefend(int enemyIndex, int playerIndex, string id)
        {
            if (enemies[enemyIndex].Health > 0)
            {
                var dmg = players[playerIndex].UseAbility(id);
                enemies[enemyIndex].Defend(dmg);
            }
            else throw new Exception("Target already dead.");
        }

        public void PlayerDefend(int enemyIndex)
        {
            if (enemies[enemyIndex].Health > 0)
            {
                var dmg = enemies[enemyIndex].UseAbility();
                var defender = enemies[enemyIndex].ChooseEnemy(players);
                if (players[defender].Health > 0)
                {
                    players[defender].Defend(dmg);
                }
                else if (CheckLoss())
                    return;
                else PlayerDefend(enemyIndex);
            }
        }

        public bool isAlive(int index)
        {
            if (players[index].Health > 0)
                return true;
            else return false;
        }

        public bool CheckLoss()
        {
            var aliveList = new List<Player>();
            foreach (var player in players)
                if (player.Health > 0)
                    aliveList.Add(player);
            if (aliveList.Count == 0)
                return true;
            else return false;
        }

        public bool CheckWin()
        {
            var aliveList = new List<NPC>();
            foreach (var npc in Enemies)
                if (npc.Health > 0)
                    aliveList.Add(npc);
            if (aliveList.Count == 0)
                return true;
            else return false;
        }
    }
}
