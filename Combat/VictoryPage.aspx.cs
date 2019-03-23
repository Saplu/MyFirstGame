using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                var mission = (MissionClassLibrary.Mission)Context.Items["Mission"];
                var dbPlayers = db.Player.ToList();

                var xpGainers = getSurvivors(dbPlayers, survivors);
                var players = getPlayers(dbPlayers, party);

                modifyXp(xpGainers);
            }


        }

        protected void okButton_Click(object sender, EventArgs e)
        {

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
            var mission = (MissionClassLibrary.Mission)Context.Items["Mission"];
            var xp = mission.CalculateXp();
            foreach (var player in gainers)
                player.Xp += xp;
        }
    }
}