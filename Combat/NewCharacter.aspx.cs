using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Combat
{
    public partial class NewCharacter : System.Web.UI.Page
    {
        DAO.CharacterDAO dao = new DAO.CharacterDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                resultLabel.Text = dao.TooltipInfo(Convert.ToInt32(classRadioButtonList.SelectedValue));
        }

        protected void menuButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("Menu.aspx");
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                resultLabel.Text = "";
                var tryName = nameTextBox.Text;
                dao.NameExists(tryName);
                dao.NewPlayer(Convert.ToInt32(classRadioButtonList.SelectedValue), tryName);
                resultLabel.Text = "Character successfully created! GZ!";
            }
            catch(Exception ex)
            {
                resultLabel.Text = ex.Message;
            }
        }

        protected void classRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            resultLabel.Text = dao.TooltipInfo(Convert.ToInt32(classRadioButtonList.SelectedValue));
        }
    }
}