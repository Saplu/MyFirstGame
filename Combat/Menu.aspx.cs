using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Combat
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void fightButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("CombatMenu.aspx");
        }

        protected void shopButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("Shop.aspx");
        }
    }
}