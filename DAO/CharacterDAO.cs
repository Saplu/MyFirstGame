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
                    case 0: modifyWarrior(player); break;
                    case 1: modifyMage(player); break;
                    case 2: modifyBloodPriest(player); break;
                    case 3: modifyProtector(player); break;
                    case 4: modifyFairy(player); break;
                    case 5: modifyShaman(player); break;
                }
            }
        }

        private void modifyBloodPriest(Player player)
        {
            player.Health += 17;
            player.SpellPower += 2;
        }

        private void modifyMage(Player player)
        {
            player.Health += 15;
            player.SpellPower += 2;
            db.SaveChanges();
        }

        private void modifyWarrior(Player player)
        {
            player.Health += 20;
            player.Strength += 2;
            db.SaveChanges();
        }

        private void modifyProtector(Player player)
        {
            player.Health += 24;
            player.Strength += 2;
            db.SaveChanges();
        }

        private void modifyFairy(Player player)
        {
            player.Health += 15;
            player.SpellPower += 2;
            db.SaveChanges();
        }

        private void modifyShaman(Player player)
        {
            player.Health += 18;
            player.SpellPower += 2;
            db.SaveChanges();
        }
    }
}
