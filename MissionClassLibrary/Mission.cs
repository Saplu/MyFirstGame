using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;
using CharacterClassLibrary.Enums;
using CombatLogicClassLibrary;

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

        public void EnemyDefend(int enemyIndex, int playerIndex, string id, int enemyCount)
        {
            var targetCount = players[playerIndex - 1].GetTargets(id);
            var targets = setTargets(enemyIndex, targetCount);
            if (enemies[enemyIndex - 5].Health > 0)
            {
                foreach (var target in targets)
                {
                    if (enemies[target - 5].Health > 0)
                    {
                        var dmg = players[playerIndex - 1].UseAbility(id, enemyCount);
                        enemies[target - 5].Defend(dmg);
                    }
                }
            }
            else throw new Exception("Target already dead.");
        }

        public void SetStatuses(int playerIndex, string id)
        {
            var targetList = players[playerIndex - 1].setStatusTargets(id);
            if (targetList.Count > 0)
            {
                var status = Status.Create(id);
                foreach (var thing in targetList)
                {
                    var target = players.Find(x => x.Position == thing);
                    target.Statuses.Add(status);
                }
            }
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

        public void modifyLength()
        {
            foreach (var player in Players)
            {
                var keepList = new List<Status>();
                foreach (var status in player.Statuses)
                {
                    status.Duration--;
                    if (status.Duration > 0)
                    {
                        keepList.Add(status);
                    }
                }
                player.Statuses = keepList;
            }
        }

        private List<int> setTargets(int index, int targets)
        {
            var result = new List<int>();

            if (targets == 1)
                result.Add(index);
            else if (targets == 2)
                result = setTwoTargets(index);
            else if (targets == 3)
                result = setThreeTargets(index);
            else if (targets == 4)
                result = setFourTargets(index);

            return result;
        }

        private List<int> setTwoTargets(int index)
        {
            var result = new List<int>();
            if (index == 1 || index == 2 || index == 3 || index == 5 || index == 6 || index == 7)
            {
                result.Add(index);
                result.Add(index + 1);
            }
            else if (index == 4 || index == 8)
            {
                result.Add(index);
                result.Add(index - 1);
            }
            return result;
        }

        private List<int> setThreeTargets(int index)
        {
            var result = new List<int>();
            if (index == 1 || index == 2 || index == 5 || index == 6)
            {
                result.Add(index);
                result.Add(index + 1);
                result.Add(index + 2);
            }
            else if (index == 3 || index == 4 || index == 7 || index == 8)
            {
                result.Add(index);
                result.Add(index - 1);
                result.Add(index - 2);
            }
            return result;
        }

        private List<int> setFourTargets(int index)
        {
            var result = new List<int>();
            if (index < 5)
            {
                result.Add(1);
                result.Add(2);
                result.Add(3);
                result.Add(4);
            }
            else
            {
                result.Add(5);
                result.Add(6);
                result.Add(7);
                result.Add(8);
            }
            return result;
        }
    }
}
