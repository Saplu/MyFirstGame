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
        DAO.CharacterDAO dao = new DAO.CharacterDAO();
        DAO.ItemDAO itemDAO = new DAO.ItemDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var party = (List<string>)Context.Items["Party"];
                var survivors = (List<string>)Context.Items["Survivors"];
                ViewState.Add("Survivors", survivors);
                var mission = (MissionClassLibrary.Mission)Context.Items["Mission"];
                ViewState.Add("Mission", mission);

                var players = dao.GetPlayers(party);

                var item = new RandomItemGenerator();
                var quality = mission.RewardItemQuality();
                var reward = item.CreateItem(mission.Level, quality);
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
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            var xp = mission.CalculateXp();
            var tryThis = playerRadioButtonList.SelectedValue;
            var target = mission.Players.Find(x => x.Name == tryThis);
            var items = itemDAO.GetItems();
            var survivors = (List<string>)ViewState["Survivors"];
            var xpGainers = dao.GetSurvivors(survivors);

            if (tryThis == "Inventory")
            {
                dao.ModifyXp(xpGainers, xp);
                item.Owner = tryThis;
                var reward = convertToDbItem(item);
                itemDAO.AddNewItem(reward);
                Server.Transfer("Menu.aspx");
            }
            else if (target.ItemTypes.Exists(x => x == item.ItemType))
            {
                itemDAO.removeCurrentItem(Convert.ToInt32(item.ItemPlace), target.Name);
                dao.ModifyXp(xpGainers, xp);
                item.Owner = target.Name;
                var reward = convertToDbItem(item);
                itemDAO.AddNewItem(reward);
                Server.Transfer("Menu.aspx");
            }
            else currentLabel.Text = "Cannot wear the armor type.";
        }

        private void PlayerRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = itemDAO.GetItems();
            var loot = (CharacterClassLibrary.Item)ViewState["Loot"];
            if (items.Exists(x => x.Owner == playerRadioButtonList.SelectedValue && x.Place == Convert.ToInt32(loot.ItemPlace)))
            {
                var item = items.Find(x => x.Owner == playerRadioButtonList.SelectedValue &&
                x.Place == Convert.ToInt32(loot.ItemPlace));
                currentLabel.Text = itemDAO.itemToString(item);
            }
            else currentLabel.Text = "Nothing equipped.";
        }

        private DAO.Item convertToDbItem(CharacterClassLibrary.Item item)
        {
            var dbItem = new DAO.Item();
            dbItem.Armor = item.Armor;
            dbItem.Crit = item.Crit;
            dbItem.Health = item.Health;
            dbItem.Owner = item.Owner;
            dbItem.Place = Convert.ToInt32(item.ItemPlace);
            dbItem.SpellPower = item.Spellpower;
            dbItem.Strength = item.Strength;
            dbItem.Type = Convert.ToInt32(item.ItemType);
            dbItem.Name = item.Name;
            dbItem.SellValue = item.SellValue;
            return dbItem;
        }
    }
}