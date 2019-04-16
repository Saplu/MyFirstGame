using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO;

namespace Combat
{
    public partial class Shop : System.Web.UI.Page
    {
        DAO.CharacterDAO dao = new DAO.CharacterDAO();
        DAO.ItemDAO itemDAO = new DAO.ItemDAO();
        ShopClassLibrary.Shop shop = new ShopClassLibrary.Shop();

        protected void Page_Load(object sender, EventArgs e)
        {
            moneyLabel.Text = "Current money: " + shop.Money.ToString();
        }

        protected void craftButton_Click(object sender, EventArgs e)
        {
            itemGridView.Visible = false;
            exceptionLabel.Text = "";
            confirmButton.Visible = true;
            typeLabel.Visible = true;
            placeLabel.Visible = true;
            playerLabel.Visible = true;
            typeDropDownList.Visible = true;
            placeDropDownList.Visible = true;
            characterDropDownList.Visible = true;
            var players = shop.Players;
            characterDropDownList.DataSource = getData(players);
            characterDropDownList.DataBind();
            craftButton.Visible = false;
            sellButton.Visible = false;
            backButton.Visible = true;
            offerLabel.Text = getOffer();
        }

        protected void sellButton_Click(object sender, EventArgs e)
        {
            exceptionLabel.Text = "";
            itemGridView.Visible = true;
            itemGridView.DataSource = shop.InventoryItems;
            itemGridView.DataBind();
        }

        protected void confirmButton_Click(object sender, EventArgs e)
        {
            exceptionLabel.Text = "";
            var gen = new CharacterClassLibrary.RandomItemGenerator();
            try
            {
                var type = shop.SaleItemType(typeDropDownList.SelectedValue);
                var place = shop.SaleItemPlace(placeDropDownList.SelectedValue);
                if (characterDropDownList.SelectedValue == null)
                    throw new Exception("No character selected");
                type = shop.ManageType(place, type);
                var buyer = shop.setBuyer(characterDropDownList.SelectedValue, type);
                var itemType = shop.casterOrPhysical(buyer.Class);
                var item = gen.CreateItem(buyer.Level, place, type, itemType, buyer.Id);
                shop.ManageMoney(item);
                shop.AddItem(item);
                offerLabel.Text = "Thanks for giving your money. You got:<br/>" + item.ToString();
                labelsOff();
            }
            catch (Exception ex)
            {
                exceptionLabel.Text = ex.Message;
            }
        }

        protected void itemGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = itemGridView.Rows[index];

            var value = Convert.ToInt32(row.Cells[3].Text);
            itemDAO.ManageMoney(value);
            itemDAO.DeleteItem(Convert.ToInt32(row.Cells[1].Text));
            var items = itemDAO.GetInventoryItems();
            itemGridView.DataSource = items;
            itemGridView.DataBind();
        }

        private List<string> getData(List<Player> list)
        {
            var value = new List<string>();
            foreach (var item in list)
            {
                value.Add(item.Id);
            }
            return value;
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            labelsOff();
        }

        protected void typeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                exceptionLabel.Text = "";
                offerLabel.Text = getOffer();
            }
            catch(Exception ex)
            {
                exceptionLabel.Text = ex.Message;
            }
        }

        protected void placeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                exceptionLabel.Text = "";
                offerLabel.Text = getOffer();
            }
            catch (Exception ex)
            {
                exceptionLabel.Text = ex.Message;
            }
        }

        protected void characterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                exceptionLabel.Text = "";
                offerLabel.Text = getOffer();
            }
            catch (Exception ex)
            {
                exceptionLabel.Text = ex.Message;
            }
        }

        private string getOffer()
        {
            var type = shop.SaleItemType(typeDropDownList.SelectedValue);
            var place = shop.SaleItemPlace(placeDropDownList.SelectedValue);
            if (characterDropDownList.SelectedValue == null)
                throw new Exception("No character selected");
            type = shop.ManageType(place, type);
            var buyer = shop.setBuyer(characterDropDownList.SelectedValue, type);

            return shop.currentOffer(type, place, buyer);
        }

        private void labelsOff()
        {
            typeLabel.Visible = false;
            placeLabel.Visible = false;
            playerLabel.Visible = false;
            typeDropDownList.Visible = false;
            placeDropDownList.Visible = false;
            characterDropDownList.Visible = false;
            backButton.Visible = false;
            sellButton.Visible = true;
            craftButton.Visible = true;
            confirmButton.Visible = false;
        }
    }
}