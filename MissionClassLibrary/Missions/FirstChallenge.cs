﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;

namespace MissionClassLibrary.Missions
{
    [Serializable]
    public class FirstChallenge : Mission
    {
        public FirstChallenge(List<Player> players)
        {
            var enemy1 = new CharacterClassLibrary.NPCClasses.Rabbit(2);
            var enemy2 = new CharacterClassLibrary.NPCClasses.Rabbit(2);
            var enemy3 = new CharacterClassLibrary.NPCClasses.Rabbit(2);
            var enemy4 = new CharacterClassLibrary.NPCClasses.Rabbit(2);
            Enemies = new List<NPC>() { enemy1, enemy2, enemy3, enemy4 };
            Players = new List<Player>();
            foreach (var player in players)
                Players.Add(player);
            Turn = 1;
        }
    }
}
