using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CharacterDAO
    {
        CharacterDbEntities db = new CharacterDbEntities();

        public List<Player> GetSurvivors(List<string> survivors)
        {
            var survive = new List<Player>();
            foreach (var thing in db.Player)
            {
                if (survivors.Exists(x => x == thing.Id))
                    survive.Add(thing);
            }
            return survive;
        }

        public List<Player> GetPlayers(List<string> players)
        {
            var plays = new List<Player>();
            foreach (var thing in db.Player)
            {
                if (players.Exists(x => x == thing.Id))
                    plays.Add(thing);
            }
            return plays;
        }

        public List<Player> GetPlayers()
        {
            return db.Player.ToList();
        }

        public void ModifyXp(List<Player> gainers, int xp)
        {
            foreach (var player in gainers)
            {
                player.Xp += xp;
                checkForLevelup(player);
            }
            db.SaveChanges();
        }

        private void checkForLevelup(Player player)
        {
            if (player.Xp >= (player.Level * 100))
            {
                player.Xp -= (player.Level * 100);
                player.Level++;
                var info = player.Class;

                switch(info)
                {
                    case 0: modifyPlayer(player, 20, 2, 0); break;
                    case 1: modifyPlayer(player, 15, 0, 2); break;
                    case 2: modifyPlayer(player, 17, 0, 2); break;
                    case 3: modifyPlayer(player, 24, 2, 0); break;
                    case 4: modifyPlayer(player, 15, 0, 2); break;
                    case 5: modifyPlayer(player, 18, 0, 2); break;
                    case 6: modifyPlayer(player, 17, 2, 0); break;
                    case 7: modifyPlayer(player, 24, 0, 2); break;
                }
            }
        }

        private void modifyPlayer(Player player, int health, int strength, int spellPower)
        {
            player.Health += health;
            player.Strength += strength;
            player.SpellPower += spellPower;
            db.SaveChanges();
        }
    }
}
