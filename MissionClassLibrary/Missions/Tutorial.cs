using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    [Serializable]
    public class Tutorial : Mission
    {
        public Tutorial(List<Player> players)
        {
            var enemy = new CharacterClassLibrary.NPCClasses.Rabbit(1);
            Enemies = new List<NPC>() { enemy };
            Players = new List<Player>();
            foreach (var player in players)
                Players.Add(player);
            Turn = 1;
        }
    }
}
