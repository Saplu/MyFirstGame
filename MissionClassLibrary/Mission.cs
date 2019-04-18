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
        private List<NPC> enemies;
        private List<Player> players;
        private int turn;
        private int level;
        private List<int> actionsTaken;
        private int[] rewardTable;

        public List<NPC> Enemies { get => enemies; set => enemies = value; }
        public List<Player> Players { get => players; set => players = value; }
        public int Turn { get => turn; set => turn = value; }
        public int Level { get => level; set => level = value; }
        public List<int> ActionsTaken { get => actionsTaken; set => actionsTaken = value; }
        public int[] RewardTable { get => rewardTable; set => rewardTable = value; }

        public static Mission Create(MissionList missions, List<Player> players)
        {
            switch (missions)
            {
                case MissionList.Tutorial: return new Missions.Tutorial(players);
                case MissionList.NextStep: return new Missions.NextStep(players);
                case MissionList.FirstChallenge: return new Missions.FirstChallenge(players);
                case MissionList.SomethingNew: return new Missions.SomethingNew(players);
                case MissionList.TankThat: return new Missions.TankThat(players);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void EnemyDefend(int enemyIndex, int playerIndex, string id)
        {
            if (players[playerIndex - 1].isStunned() == false)
            {
                var targetCount = players[playerIndex - 1].GetTargets(id);
                var util = new Utils.TargetSetter();
                var targets = util.setTargets(enemyIndex, targetCount, Enemies.Count);
                if (enemies[enemyIndex - 5].Health > 0)
                {
                    foreach (var target in targets)
                    {
                        if (enemies[target - 5].Health > 0)
                        {
                            var dmg = players[playerIndex - 1].UseAbility(id);
                            var threat = players[playerIndex - 1].GetThreat(id);
                            if (Utils.TrueDamageAbilities.IsTrueDmg(id))
                                enemies[target - 5].TrueDmgDefend(dmg);
                            else enemies[target - 5].Defend(dmg);
                            enemies[target - 5].ManageThreat(playerIndex - 1, threat);
                        }
                    }
                }
                else throw new Exception("Target already dead.");
            }
            else throw new Exception("You are stunned and cannot attack.");
        }

        public void PlayerDefend(int enemyIndex)
        {
            if (enemies[enemyIndex].Health > 0 && enemies[enemyIndex].isStunned() == false)
            {
                var id = enemies[enemyIndex].ChooseAbility();
                var targetCount = enemies[enemyIndex].GetTargets(id);
                var dmg = enemies[enemyIndex].UseAbility(id);
                var defender = enemies[enemyIndex].ChooseEnemy();
                if (players[defender - 1].Health > 0)
                {
                    var util = new Utils.TargetSetter();
                    var targets = util.setTargets(defender, targetCount);

                    foreach (var target in targets)
                    {
                        if (players[target - 1].Health > 0)
                        {
                            if (Utils.TrueDamageAbilities.IsTrueDmg(id))
                                players[target - 1].TrueDmgDefend(dmg);
                            else players[target - 1].Defend(dmg);
                        }
                    }
                }
                else if (CheckLoss())
                    return;
                else
                {
                    enemies[enemyIndex].ManageThreat(defender - 1);
                    PlayerDefend(enemyIndex);
                }
            }
        }

        public void PlayerHeal(int targetIndex, int playerIndex, string id)
        {
            if (players[targetIndex - 1].Health > 0)
            {
                var targetCount = players[playerIndex - 1].GetTargets(id);
                var util = new Utils.TargetSetter();
                var targets = util.setTargets(targetIndex, targetCount, Enemies.Count);
                foreach (var player in targets)
                {
                    if (players[player - 1].Health > 0)
                    {
                        var heal = players[playerIndex - 1].UseAbility(id);
                        players[player - 1].RecieveHeal(heal);
                    }
                }
            }
            else throw new Exception("Healing dead people won't make any good to the mankind.");
        }

        public void SetStatuses(int playerIndex, string id, int targetPosition)
        {
            Players[playerIndex - 1].SetStatuses(id, players, enemies, targetPosition);
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

        public void ModifyLength()
        {
            foreach (var player in Players)
            {
                player.ApplyHoT();
                player.ApplyDoT();
                player.ModifyStatusLength();
                player.ModifyCooldownLength();
            }
            foreach (var enemy in Enemies)
            {
                enemy.ApplyHoT();
                enemy.ApplyDoT();
                enemy.ModifyStatusLength();
            }
        }

        public int[] CheckCooldowns(int index)
        {
            var cdarr = Players[index].Cooldowns;
            return cdarr;
        }

        public int CalculateXp()
        {
            var playerLevel = 0;
            var enemyLevel = 0;
            var value = Enemies.Count * 5;
            foreach (var player in Players)
                playerLevel += player.Level;
            foreach (var enemy in Enemies)
            {
                var bonus = typeWeight(Convert.ToInt32(enemy.Type));
                enemyLevel += enemy.Level + bonus;
            }
            value += enemyLevel - playerLevel;
            if (value > 0)
                return value;
            else return 0;
        }

        public void ActionDone(int place)
        {
            ActionsTaken.Add(place);
        }

        public ItemQuality RewardItemQuality()
        {
            var rand = Utils.RandomProvider.GetRandom(1, 100);
            if (rand <= RewardTable[0])
                return ItemQuality.Poor;
            else if (rand > RewardTable[0] && rand <= RewardTable[0] + RewardTable[1])
                return ItemQuality.Good;
            else if (rand > RewardTable[0] + RewardTable[1] && rand <= RewardTable[0] + RewardTable[1] + RewardTable[2])
                return ItemQuality.Great;
            else return ItemQuality.Masterpiece;
        }

        private int typeWeight(int type)
        {
            switch (type)
            {
                case 0: return -1;
                case 1: return 0;
                case 2: return 2;
                case 3: return 4;
                default: return 0;
            }
        }
    }
}
