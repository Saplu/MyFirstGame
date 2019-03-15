using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharacterClassLibrary.Enums;
using CharacterClassLibrary.NPCClasses;
using CharacterClassLibrary;

namespace Combat
{
    public partial class Combat : System.Web.UI.Page
    {
        CharacterDbEntities db = new CharacterDbEntities();
        Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var players = new List<CharacterClassLibrary.Player>();
                foreach (var player in db.Player)
                {
                    var character = CharacterClassLibrary.Player.Create((ClassName)player.Class);
                    getStats(character, player);
                    players.Add(character);
                }

                var items = getItems(db.Item);

                foreach (var player in players)
                {
                    foreach (var item in items)
                    {
                        if (item.Owner == player.Name)
                            player.Items.Add(item);
                    }
                }
                players = getStats(players);
                var pasi = new Rabbit(1);
                var enemies = new List<NPC>();
                enemies.Add(pasi);

                ViewState.Add("Players", players);
                ViewState.Add("Enemies", enemies);
                ViewState.Add("ID", "");
                ViewState.Add("Player", "");

                player1ImageButton.ImageUrl = "Pictures\\ninja.jpg";
                enemy1ImageButton.ImageUrl = "Pictures\\Pasi.jpg";

                player1Label.Text = players[0].Health.ToString();
                enemy1Label.Text = pasi.Health.ToString();
            }

        }

        protected void player1ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            skill1Button.Visible = true;
            skill2Button.Visible = true;
            var players = (List<CharacterClassLibrary.Player>)ViewState["Players"];
            var player = CharacterClassLibrary.Player.Create(players[0].ClassName);
            player.Items = players[0].Items;
            getStats(player);
            skill1Button.Text = player.Ability1()[0];
            skill2Button.Text = player.Ability2()[0];
            ViewState["Player"] = player;
        }

        protected void skill1Button_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = skill1Button.Text;
            enemy1ImageButton.Enabled = true;
            player1ImageButton.Enabled = false;
        }

        protected void skill2Button_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = skill2Button.Text;
            enemy1ImageButton.Enabled = true;
            player1ImageButton.Enabled = false;
        }

        protected void enemy1ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var enemies = (List<NPC>)ViewState["Enemies"];
            var enemy = enemies[0];
            var players = (List<CharacterClassLibrary.Player>)ViewState["Players"];
            var player = CharacterClassLibrary.Player.Create(players[0].ClassName);
            player.Items = players[0].Items;
            getStats(player);
            var dmg = player.UseAbility(ViewState["ID"].ToString(), random);
            enemy.Defend(dmg);
            enemy1Label.Text = enemy.Health.ToString();
            ViewState["Enemies"] = enemies;
            attackDone();
        }

        protected void endTurnButton_Click(object sender, EventArgs e)
        {
            var enemies = (List<NPC>)ViewState["Enemies"];
            var players = (List<CharacterClassLibrary.Player>)ViewState["Players"];
            var dmg = enemies[0].UseAbility(random);
            players[0].Defend(dmg);
            player1Label.Text = players[0].Health.ToString();
            attackDone();
            turnOver();
        }

        private List<CharacterClassLibrary.Player> getStats(List<CharacterClassLibrary.Player> players)
        {
            foreach (var player in players)
            {
                getStats(player);
            }
            return players;
        }

        private void getStats(CharacterClassLibrary.Player player)
        {
            foreach (var item in player.Items)
            {
                player.Health += item.Health;
                player.MaxHealth = player.Health;
                player.Strength += item.Strength;
                player.SpellPower += item.Spellpower;
                player.Armor += item.Armor;
                player.Crit += item.Crit;
            }
        }

        private List<CharacterClassLibrary.Item> getItems(DbSet<Item> items)
        {
            var list = new List<CharacterClassLibrary.Item>();
            foreach (var item in items)
            {
                var thing = new CharacterClassLibrary.Item(item.Health, item.Strength, item.Crit, item.SpellPower,
                    item.Armor, item.Name, 1, (ItemType)item.Type, (ItemPlace)item.Place, item.Owner);
                list.Add(thing);
            }
            return list;
        }

        private CharacterClassLibrary.Player getStats(CharacterClassLibrary.Player character, Player player)
        {
            character.Armor = player.Armor;
            character.ClassName = (ClassName)player.Class;
            character.Crit = player.Crit;
            character.Health = player.Health;
            character.Level = player.Level;
            character.SpellPower = player.SpellPower;
            character.Name = player.Id;
            character.Strength = player.Strength;
            character.Xp = player.Xp;
            return character;
        }

        private void attackDone()
        {
            enemy1ImageButton.Enabled = false;
            enemy2ImageButton.Enabled = false;
            enemy3ImageButton.Enabled = false;
            enemy4ImageButton.Enabled = false;
            skill1Button.Visible = false;
            skill2Button.Visible = false;
            skill3Button.Visible = false;
            skill4Button.Visible = false;
        }

        private void turnOver()
        {
            player1ImageButton.Enabled = true;
            player2ImageButton.Enabled = true;
            player3ImageButton.Enabled = true;
            player4ImageButton.Enabled = true;
        }
    }
}