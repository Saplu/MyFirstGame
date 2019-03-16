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

                var mission = new MissionClassLibrary.Tutorial(players);

                ViewState.Add("Players", players);
                ViewState.Add("Enemies", mission.Enemies);
                ViewState.Add("ID", "");
                ViewState.Add("Player", "");
                ViewState.Add("Mission", mission);

                player1ImageButton.ImageUrl = "Pictures\\ninja.jpg";
                enemy1ImageButton.ImageUrl = "Pictures\\Pasi.jpg";

                player1Label.Text = players[0].Health.ToString();
                enemy1Label.Text = mission.Enemies[0].Health.ToString();
                turnLabel.Text = "Turn: " + mission.Turn.ToString();
            }

        }

        protected void player1ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            skill1Button.Visible = true;
            skill2Button.Visible = true;
            var mission = (MissionClassLibrary.Tutorial)ViewState["Mission"];
            skill1Button.Text = mission.Players[0].Ability1()[0];
            skill2Button.Text = mission.Players[0].Ability2()[0];
            ViewState["Player"] = 0;
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
            var mission = (MissionClassLibrary.Tutorial)ViewState["Mission"];
            mission.EnemyDefend(0, Convert.ToInt32(ViewState["Player"]), ViewState["ID"].ToString());
            enemy1Label.Text = mission.Enemies[0].Health.ToString();
            ViewState["Enemies"] = mission.Enemies;
            ViewState["Mission"] = mission;
            attackDone();
        }

        protected void endTurnButton_Click(object sender, EventArgs e)
        {
            var mission = (MissionClassLibrary.Tutorial)ViewState["Mission"];

            for (int i = 0; i < mission.Enemies.Count; i++)
            {
                mission.PlayerDefend(i);
            }
            mission.Turn++;
            ViewState["Mission"] = mission;
            player1Label.Text = mission.Players[0].Health.ToString();
            attackDone();
            turnOver(mission.Turn);
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

        private void turnOver(int turn)
        {
            player1ImageButton.Enabled = true;
            player2ImageButton.Enabled = true;
            player3ImageButton.Enabled = true;
            player4ImageButton.Enabled = true;
            turnLabel.Text = "Turn: " + turn.ToString();
        }
    }
}