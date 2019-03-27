using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CharacterClassLibrary;
using CharacterClassLibrary.Enums;
using MissionClassLibrary;

namespace Combat
{
    public partial class VictoryPage : System.Web.UI.Page
    {
        CharacterDbEntities db = new CharacterDbEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var party = (List<string>)Context.Items["Party"];
                var survivors = (List<string>)Context.Items["Survivors"];
                ViewState.Add("Survivors", survivors);
                var mission = (MissionClassLibrary.Mission)Context.Items["Mission"];
                ViewState.Add("Mission", mission);
                var dbPlayers = db.Player.ToList();

                var players = getPlayers(dbPlayers, party);

                var item = new RandomItemGenerator();
                var reward = item.CreateItem(mission.Level);
                ViewState.Add("Loot", reward);

                lootLabel.Text = reward.ToString();
                xpLabel.Text = mission.CalculateXp().ToString();

                party.Add("Inventory");
                playerRadioButtonList.DataSource = party;
                playerRadioButtonList.DataBind();
            }
            playerRadioButtonList.SelectedIndexChanged += PlayerRadioButtonList_SelectedIndexChanged;
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            var item = (CharacterClassLibrary.Item)ViewState["Loot"];
            var dbPlayers = db.Player.ToList();
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            var tryThis = playerRadioButtonList.SelectedValue;
            var target = mission.Players.Find(x => x.Name == tryThis);
            var items = db.Item.ToList();
            var survivors = (List<string>)ViewState["Survivors"];
            var xpGainers = getSurvivors(dbPlayers, survivors);

            if (tryThis == "Inventory")
            {
                modifyXp(xpGainers);
                item.Owner = tryThis;
                var reward = convertToDbItem(item);
                var previous = items.Last().Id;
                reward.Id = previous + 1;
                db.Item.Add(reward);
                db.SaveChanges();
                Server.Transfer("MainMenu.aspx");
            }
            else if (target.ItemTypes.Exists(x => x == item.ItemType))
            {
                removeCurrentItem(item.ItemPlace, target);
                modifyXp(xpGainers);
                item.Owner = target.Name;
                var reward = convertToDbItem(item);
                var previous = items.Last().Id;
                reward.Id = previous + 1;
                db.Item.Add(reward);
                db.SaveChanges();
                Server.Transfer("MainMenu.aspx");
            }
            else currentLabel.Text = "Cannot wear the armor type.";
        }

        private void PlayerRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = db.Item.ToList();
            var loot = (CharacterClassLibrary.Item)ViewState["Loot"];
            if (items.Exists(x => x.Owner == playerRadioButtonList.SelectedValue && x.Place == Convert.ToInt32(loot.ItemPlace)))
            {
                var item = items.Find(x => x.Owner == playerRadioButtonList.SelectedValue &&
                x.Place == Convert.ToInt32(loot.ItemPlace));
                currentLabel.Text = itemToString(item);
            }
            else currentLabel.Text = "Nothing equipped.";
        }
        
        private List<Player> getSurvivors(List<Player> db, List<string> survivors)
        {
            var survive = new List<Player>();
            foreach(var thing in db)
            {
                if (survivors.Exists(x => x == thing.Id))
                    survive.Add(thing);
            }
            return survive;
        }
        
        private List<Player> getPlayers(List<Player> db, List<string> players)
        {
            var plays = new List<Player>();
            foreach (var thing in db)
            {
                if (players.Exists(x => x == thing.Id))
                    plays.Add(thing);
            }
            return plays;
        }

        private void modifyXp(List<Player> gainers)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            var xp = mission.CalculateXp();
            foreach (var player in gainers)
            {
                player.Xp += xp;
                checkForLevelup(player);
            }            
            db.SaveChanges();
        }

        private string itemToString(Item item)
        {
            var value = item.Name + "<br/>Health: " + item.Health + "<br/>Strength: " + item.Strength + 
                "<br/>Spellpower: " + item.SpellPower + "<br/>Armor: " + item.Armor + "<br/>Crit: " + item.Crit;
            return value;
        }

        private Item convertToDbItem(CharacterClassLibrary.Item item)
        {
            var dbItem = new Item();
            dbItem.Armor = item.Armor;
            dbItem.Crit = item.Crit;
            dbItem.Health = item.Health;
            dbItem.Owner = item.Owner;
            dbItem.Place = Convert.ToInt32(item.ItemPlace);
            dbItem.SpellPower = item.Spellpower;
            dbItem.Strength = item.Strength;
            dbItem.Type = Convert.ToInt32(item.ItemType);
            dbItem.Name = item.Name;
            return dbItem;
        }

        private void removeCurrentItem(ItemPlace itemPlace, CharacterClassLibrary.Player target)
        {
            if (db.Item.ToList().Exists(x => x.Owner == target.Name && x.Place == Convert.ToInt32(itemPlace)))
            {
                var item = db.Item.ToList().Find(x => x.Owner == target.Name && x.Place == Convert.ToInt32(itemPlace));
                item.Owner = "Inventory";
            }
        }

        private void checkForLevelup(Player player)
        {
            if (player.Xp >= (player.Level * 100))
            {
                player.Xp -= (player.Level * 100);
                player.Level++;
                var className = (ClassName)Enum.Parse(typeof(ClassName), player.Class.ToString());
                var Player = CharacterClassLibrary.Player.Create(className);
                Player.LevelUp();
                player.Health = Player.Health;
                player.Strength = Player.Strength;
                player.SpellPower = Player.SpellPower;
            }
        }

    }
}