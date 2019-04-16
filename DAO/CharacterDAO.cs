﻿using System;
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

        public string TooltipInfo(string name)
        {
            var player = db.Player.ToList().Find(x => x.Id == name);
            return info(player);
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

        private string info(Player player)
        {
            var value = "";
            switch(player.Class)
            {
                case 0: value = "Warrior. Level: " + player.Level + " Tough fighter with high dmg single-target abilities."; break;
                case 1: value = "Mage. Level: " + player.Level + " Fragile caster with both high single-target nuke and AoE."; break;
                case 2: value = "Blood Priest. Level: " + player.Level + " Single target heals, self survival and debuffing attacks."; break;
                case 3: value = "Protector. Level: " + player.Level + " Real bosstanker. " +
                        "Taunt, threat abilities and permanently reduced taken dmg."; break;
                case 4: value = "Fairy. Level: " + player.Level + " Fragile healer with HoT, shield and party buff."; break;
                case 5: value = "Shaman. Level: " + player.Level + " Tough caster with both single target and aoe nukes. " +
                        "More of a tank, less of a mage.";break;
                case 6: value = "Rogue. Level: " + player.Level + " Tricky fighter. Abilities cost energy, apply poison to the target " +
                        "and gain combo points consumed by ultimate."; break;
                case 7: value = "Templar. Level: " + player.Level + " Magetank. Permanently reduced taken damage. " +
                        "High threat aoe abilities, but no taunt."; break;
            }
            return value;
        }
    }
}
