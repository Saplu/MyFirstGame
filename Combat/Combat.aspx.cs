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
                var missionName = Context.Items["MissionName"].ToString();
                var play = (List<CharacterClassLibrary.Player>)Context.Items["MissionPlayers"];

                var Name = (MissionList)Enum.Parse(typeof(MissionList), missionName);
                var mission = MissionClassLibrary.Mission.Create(Name, play);

                ViewState.Add("ID", "");
                ViewState.Add("Player", "");
                ViewState.Add("Mission", mission);

                setPictures();
                setLabels();
            }

        }

        protected void player1ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (mission.isAlive(0))
            {
                displaySkills(0);
            }
        }

        protected void player2ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (mission.isAlive(1))
            {
                displaySkills(1);
            }
        }

        protected void player3ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (mission.isAlive(2))
            {
                displaySkills(2);
            }
        }

        protected void player4ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (mission.isAlive(3))
            {
                displaySkills(3);
            }
        }

        protected void skill1Button_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = skill1Button.Text;
            enableEnemies();
            actionDone();
        }

        protected void skill2Button_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = skill2Button.Text;
            enableEnemies();
            actionDone();
        }

        protected void enemy1ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                resultLabel.Text = "";
                var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
                mission.EnemyDefend(0, Convert.ToInt32(ViewState["Player"]), ViewState["ID"].ToString());
                enemy1Label.Text = mission.Enemies[0].ToString();
                ViewState["Mission"] = mission;
                attackDone();
                victory(mission);
            }
            catch (Exception ex)
            { resultLabel.Text = ex.Message; }
        }

        protected void enemy2ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                resultLabel.Text = "";
                var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
                mission.EnemyDefend(1, Convert.ToInt32(ViewState["Player"]), ViewState["ID"].ToString());
                enemy2Label.Text = mission.Enemies[1].ToString();
                ViewState["Mission"] = mission;
                attackDone();
                victory(mission);
            }
            catch (Exception ex)
            { resultLabel.Text = ex.Message; }
        }

        protected void enemy3ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                resultLabel.Text = "";
                var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
                mission.EnemyDefend(2, Convert.ToInt32(ViewState["Player"]), ViewState["ID"].ToString());
                enemy3Label.Text = mission.Enemies[2].ToString();
                ViewState["Mission"] = mission;
                attackDone();
                victory(mission);
            }
            catch (Exception ex)
            { resultLabel.Text = ex.Message; }
        }

        protected void enemy4ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                resultLabel.Text = "";
                var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
                mission.EnemyDefend(3, Convert.ToInt32(ViewState["Player"]), ViewState["ID"].ToString());
                enemy4Label.Text = mission.Enemies[3].ToString();
                ViewState["Mission"] = mission;
                attackDone();
                victory(mission);
            }
            catch (Exception ex)
            { resultLabel.Text = ex.Message; }
        }

        protected void endTurnButton_Click(object sender, EventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];

            for (int i = 0; i < mission.Enemies.Count; i++)
            {
                mission.PlayerDefend(i);
            }
            mission.Turn++;
            ViewState["Mission"] = mission;
            player1Label.Text = mission.Players[0].ToString();
            player2Label.Text = mission.Players[1].ToString();
            player3Label.Text = mission.Players[2].ToString();
            player4Label.Text = mission.Players[3].ToString();
            attackDone();
            turnOver(mission.Turn);
            gameOver(mission);
        }

        private void enableEnemies()
        {
            enemy1ImageButton.Enabled = true;
            enemy2ImageButton.Enabled = true;
            enemy3ImageButton.Enabled = true;
            enemy4ImageButton.Enabled = true;
        }

        private void actionDone()
        {
            var index = Convert.ToInt32(ViewState["Player"]);
            switch(index)
            {
                case 0: player1ImageButton.Enabled = false;
                    break;
                case 1: player2ImageButton.Enabled = false;
                    break;
                case 2: player3ImageButton.Enabled = false;
                    break;
                case 3: player4ImageButton.Enabled = false;
                    break;
            }
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

        private void gameOver(MissionClassLibrary.Mission mission)
        {
            if (mission.CheckLoss())
            {
                resultLabel.Text = "You lost. Pathetic.";
                player1ImageButton.Enabled = false;
                player2ImageButton.Enabled = false;
                player3ImageButton.Enabled = false;
                player4ImageButton.Enabled = false;
                attackDone();
                endTurnButton.Enabled = false;
            }
        }

        private void victory(MissionClassLibrary.Mission mission)
        {
            if (mission.CheckWin())
            {
                resultLabel.Text = "Victory! Yay.";
                player1ImageButton.Enabled = false;
                player2ImageButton.Enabled = false;
                player3ImageButton.Enabled = false;
                player4ImageButton.Enabled = false;
                attackDone();
                endTurnButton.Enabled = false;
            }
        }

        private void displaySkills(int index)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            skill1Button.Visible = true;
            skill2Button.Visible = true;
            skill1Button.Text = mission.Players[index].Ability1()[0];
            skill2Button.Text = mission.Players[index].Ability2()[0];
            skill1Button.ToolTip = mission.Players[index].Ability1()[1];
            skill2Button.ToolTip = mission.Players[index].Ability2()[1];
            ViewState["Player"] = index;
        }

        private void setPictures()
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            player1ImageButton.ImageUrl = mission.Players[0].setPic();
            enemy1ImageButton.ImageUrl = mission.Enemies[0].setPic();
            if (playerExists(1)) player2ImageButton.ImageUrl = mission.Players[1].setPic();
            if (playerExists(2)) player3ImageButton.ImageUrl = mission.Players[2].setPic();
            if (playerExists(3)) player4ImageButton.ImageUrl = mission.Players[3].setPic();
            if (enemyExists(1)) enemy2ImageButton.ImageUrl = mission.Enemies[1].setPic();
            if (enemyExists(2)) enemy3ImageButton.ImageUrl = mission.Enemies[2].setPic();
            if (enemyExists(3)) enemy4ImageButton.ImageUrl = mission.Enemies[3].setPic();
        }

        private void setLabels()
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            player1Label.Text = mission.Players[0].ToString();
            enemy1Label.Text = mission.Enemies[0].ToString();
            turnLabel.Text = "Turn: " + mission.Turn.ToString();
            if (playerExists(1)) player2Label.Text = mission.Players[1].ToString();
            if (playerExists(2)) player3Label.Text = mission.Players[2].ToString();
            if (playerExists(3)) player4Label.Text = mission.Players[3].ToString();
            if (enemyExists(1)) enemy2Label.Text = mission.Enemies[1].ToString();
            if (enemyExists(2)) enemy3Label.Text = mission.Enemies[2].ToString();
            if (enemyExists(3)) enemy4Label.Text = mission.Enemies[3].ToString();
        }

        private bool playerExists(int index)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (mission.Players.ElementAtOrDefault(index) != null)
                return true;
            else return false;
        }

        private bool enemyExists(int index)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (mission.Enemies.ElementAtOrDefault(index) != null)
                return true;
            else return false;
        }
    }
}