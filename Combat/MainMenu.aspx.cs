using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharacterClassLibrary;
using CharacterClassLibrary.Enums;
using MissionClassLibrary;

namespace Combat
{
    public partial class MainMenu : System.Web.UI.Page
    {
        CharacterDbEntities db = new CharacterDbEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var list = new List<string>();
                foreach (var player in db.Player)
                    list.Add(player.Id);

                player1DropDownList.DataSource = list;
                player1DropDownList.DataBind();
                player2DropDownList.DataSource = list;
                player2DropDownList.DataBind();
                player3DropDownList.DataSource = list;
                player3DropDownList.DataBind();
                player4DropDownList.DataSource = list;
                player4DropDownList.DataBind();

                var missions = new List<string>();
                foreach (var mission in db.Mission)
                    missions.Add(mission.Name);

                fightRadioButtonList.DataSource = missions;
                fightRadioButtonList.DataBind();

                ViewState.Add("Mission", "");
                ViewState.Add("Players", "");

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
                ViewState["Players"] = players;
            }
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                var list = (List<CharacterClassLibrary.Player>)ViewState["Players"];
                list = getSelectedPlayers(list);
                ViewState["Players"] = list;
                if (fightRadioButtonList.SelectedValue.Length > 0)
                {
                    ViewState["Mission"] = fightRadioButtonList.SelectedValue;
                }
                else throw new Exception("Must select a mission.");
                Context.Items.Add("MissionName", ViewState["Mission"].ToString());
                Context.Items.Add("MissionPlayers", ViewState["Players"]);
                Server.Transfer("Combat.aspx");
            }
            catch (Exception ex) { Label1.Text = ex.Message; }
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

        private List<CharacterClassLibrary.Player> getSelectedPlayers(List<CharacterClassLibrary.Player> list)
        {
            var lastList = new List<CharacterClassLibrary.Player>();

            lastList.Add(list.Find(x => x.Name == player1DropDownList.SelectedValue));
            lastList.Add(list.Find(x => x.Name == player2DropDownList.SelectedValue));
            lastList.Add(list.Find(x => x.Name == player3DropDownList.SelectedValue));
            lastList.Add(list.Find(x => x.Name == player4DropDownList.SelectedValue));

            return lastList;
        }

    }
}