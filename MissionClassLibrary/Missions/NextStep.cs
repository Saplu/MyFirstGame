using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    [Serializable]
    public class NextStep : Mission
    {
        public NextStep(List<Player> players)
        {
            var enemy1 = new CharacterClassLibrary.NPCClasses.Rabbit(1);
            var enemy2 = new CharacterClassLibrary.NPCClasses.Rabbit(1);
            Enemies = new List<NPC>() { enemy1, enemy2 };
            Players = new List<Player>();
            foreach (var player in players)
                Players.Add(player);
            Turn = 1;
        }
    }
}
