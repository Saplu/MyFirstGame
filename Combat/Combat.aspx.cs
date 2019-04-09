using System;
using System.Collections.Generic;
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
                ViewState.Add("Targets", "");

                setPictures();
                setLabels();

                //var taunt = new CombatLogicClassLibrary.Statuses.Shield(3, new List<int>(), 50);
                //mission.Players[2].Statuses.Add(taunt);

            }
            checkStuns();
        }

        protected void player1ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (ViewState["Targets"].ToString() == "Heal")
            {
                try
                {
                    heal(1);
                }
                catch (Exception ex)
                { resultLabel.Text = ex.Message; }
            }
            else
            {
                if (mission.isAlive(0))
                {
                    displaySkills(0);
                    skillOnCooldown(0);
                }
            }
        }

        protected void player2ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (ViewState["Targets"].ToString() == "Heal")
            {
                try
                {
                    heal(2);
                }
                catch (Exception ex)
                { resultLabel.Text = ex.Message; }
            }
            else
            {
                if (mission.isAlive(1))
                {
                    displaySkills(1);
                    skillOnCooldown(1);
                }
            }
        }

        protected void player3ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (ViewState["Targets"].ToString() == "Heal")
            {
                try
                {
                    heal(3);
                }
                catch (Exception ex)
                { resultLabel.Text = ex.Message; }
            }
            else
            {
                if (mission.isAlive(2))
                {
                    displaySkills(2);
                    skillOnCooldown(2);
                }
            }
        }

        protected void player4ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (ViewState["Targets"].ToString() == "Heal")
            {
                try
                {
                    heal(4);
                }
                catch (Exception ex)
                { resultLabel.Text = ex.Message; }
            }
            else
            {
                if (mission.isAlive(3))
                {
                    displaySkills(3);
                    skillOnCooldown(3);
                }
            }
        }

        protected void skill1Button_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = skill1Button.Text;
            healOrDmg(skill1Button.Text);
        }

        protected void skill2Button_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = skill2Button.Text;
            healOrDmg(skill2Button.Text);
        }

        protected void skill3Button_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = skill3Button.Text;
            healOrDmg(skill3Button.Text);
        }

        protected void skill4Button_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = skill4Button.Text;
            healOrDmg(skill4Button.Text);
        }

        protected void enemy1ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                attack(5);
            }
            catch (Exception ex)
            { resultLabel.Text = ex.Message; }
        }

        protected void enemy2ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                attack(6);
            }
            catch (Exception ex)
            { resultLabel.Text = ex.Message; }
        }

        protected void enemy3ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                attack(7);
            }
            catch (Exception ex)
            { resultLabel.Text = ex.Message; }
        }

        protected void enemy4ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                attack(8);
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
            player1Label.Text = mission.Players[0].ToString();
            player2Label.Text = mission.Players[1].ToString();
            player3Label.Text = mission.Players[2].ToString();
            player4Label.Text = mission.Players[3].ToString();
            attackDone();
            mission.ModifyLength();
            setLabels();
            ViewState["Mission"] = mission;
            turnOver(mission.Turn);
            gameOver(mission);
        }

        protected void endButton_Click(object sender, EventArgs e)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            var survivors = new List<string>();
            var party = new List<string>();
            foreach (var player in mission.Players)
            {
                party.Add(player.Name);
                if (player.Health > 0)
                    survivors.Add(player.Name);
            }
            Context.Items.Add("Survivors", survivors);
            Context.Items.Add("Party", party);
            Context.Items.Add("Mission", ViewState["Mission"]);
            Server.Transfer("VictoryPage.aspx");
        }

        private void heal(int player)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            resultLabel.Text = "";
            mission.PlayerHeal(player, Convert.ToInt32(ViewState["Player"]), ViewState["ID"].ToString());
            mission.SetStatuses(Convert.ToInt32(ViewState["Player"]), ViewState["ID"].ToString(), player);
            setLabels();
            ViewState["Mission"] = mission;
            actionDone();
            attackDone();
            healDone();
            victory(mission);
        }

        private void attack(int target)
        {
            resultLabel.Text = "";
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            mission.EnemyDefend(target, Convert.ToInt32(ViewState["Player"]), ViewState["ID"].ToString());
            mission.SetStatuses(Convert.ToInt32(ViewState["Player"]), ViewState["ID"].ToString(), target);
            setLabels();
            ViewState["Mission"] = mission;
            actionDone();
            attackDone();
            victory(mission);
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
                case 1: player1ImageButton.Enabled = false;
                    break;
                case 2: player2ImageButton.Enabled = false;
                    break;
                case 3: player3ImageButton.Enabled = false;
                    break;
                case 4: player4ImageButton.Enabled = false;
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
            enablePlayers();
            turnLabel.Text = "Turn: " + turn.ToString();
        }
        private void enablePlayers()
        {
            player1ImageButton.Enabled = true;
            player2ImageButton.Enabled = true;
            player3ImageButton.Enabled = true;
            player4ImageButton.Enabled = true;
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
                endButton.Visible = true;
            }
        }

        private void displaySkills(int index)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            skill1Button.Visible = true;
            skill2Button.Visible = true;
            skill3Button.Visible = true;
            skill4Button.Visible = true;
            skill1Button.Text = mission.Players[index].Ability1()[0];
            skill2Button.Text = mission.Players[index].Ability2()[0];
            skill3Button.Text = mission.Players[index].Ability3()[0];
            skill4Button.Text = mission.Players[index].Ability4()[0];
            skill1Button.ToolTip = mission.Players[index].Ability1()[1];
            skill2Button.ToolTip = mission.Players[index].Ability2()[1];
            skill3Button.ToolTip = mission.Players[index].Ability3()[1];
            skill4Button.ToolTip = mission.Players[index].Ability4()[1];
            skill1Button.BorderColor = System.Drawing.Color.White;
            skill2Button.BorderColor = System.Drawing.Color.White;
            skill3Button.BorderColor = System.Drawing.Color.White;
            skill4Button.BorderColor = System.Drawing.Color.White;
            skill1Button.Enabled = true;
            skill2Button.Enabled = true;
            skill3Button.Enabled = true;
            skill4Button.Enabled = true;
            ViewState["Player"] = index + 1;
        }

        private void skillOnCooldown(int index)
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            var cdarr = mission.CheckCooldowns(index);
            if (cdarr[0] > 0)
            {
                skill1Button.BorderColor = System.Drawing.Color.Red;
                skill1Button.Enabled = false;
            }
            if (cdarr[1] > 0)
            {
                skill2Button.BorderColor = System.Drawing.Color.Red;
                skill2Button.Enabled = false;
            }
            if (cdarr[2] > 0)
            {
                skill3Button.BorderColor = System.Drawing.Color.Red;
                skill3Button.Enabled = false;
            }
            if (cdarr[3] > 0)
            {
                skill4Button.BorderColor = System.Drawing.Color.Red;
                skill4Button.Enabled = false;
            }
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
            else enemy2ImageButton.Visible = false;
            if (enemyExists(2)) enemy3ImageButton.ImageUrl = mission.Enemies[2].setPic();
            else enemy3ImageButton.Visible = false;
            if (enemyExists(3)) enemy4ImageButton.ImageUrl = mission.Enemies[3].setPic();
            else enemy4ImageButton.Visible = false;
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

        private void checkStuns()
        {
            var mission = (MissionClassLibrary.Mission)ViewState["Mission"];
            if (mission.Players[0].isStunned())
            {
                player1ImageButton.BorderColor = System.Drawing.Color.Red;
                player1ImageButton.Enabled = false;
            }
            else player1ImageButton.BorderColor = System.Drawing.Color.White;
            if (mission.Players[1].isStunned())
            {
                player2ImageButton.BorderColor = System.Drawing.Color.Red;
                player2ImageButton.Enabled = false;
            }
            else player2ImageButton.BorderColor = System.Drawing.Color.White;
            if (mission.Players[2].isStunned())
            {
                player3ImageButton.BorderColor = System.Drawing.Color.Red;
                player3ImageButton.Enabled = false;
            }
            else player3ImageButton.BorderColor = System.Drawing.Color.White;
            if (mission.Players[3].isStunned())
            {
                player4ImageButton.BorderColor = System.Drawing.Color.Red;
                player4ImageButton.Enabled = false;
            }
            else player4ImageButton.BorderColor = System.Drawing.Color.White;
            if (mission.Enemies[0].isStunned())
            {
                enemy1ImageButton.BorderColor = System.Drawing.Color.Red;
            }
            else enemy1ImageButton.BorderColor = System.Drawing.Color.White;
            if (mission.Enemies.Count >= 2 && mission.Enemies[1].isStunned())
            {
                enemy2ImageButton.BorderColor = System.Drawing.Color.Red;
            }
            else enemy2ImageButton.BorderColor = System.Drawing.Color.White;
            if (mission.Enemies.Count >= 3 && mission.Enemies[2].isStunned())
            {
                enemy3ImageButton.BorderColor = System.Drawing.Color.Red;
            }
            else enemy3ImageButton.BorderColor = System.Drawing.Color.White;
            if (mission.Enemies.Count >= 4 && mission.Enemies[3].isStunned())
            {
                enemy4ImageButton.BorderColor = System.Drawing.Color.Red;
            }
            else enemy4ImageButton.BorderColor = System.Drawing.Color.White;
        }

        private void healOrDmg(string id)
        {
            if (Utils.HealAbilities.IsHeal(id))
            {
                enablePlayers();
                ViewState["Targets"] = "Heal";
            }
            else
            {
                enableEnemies();
                ViewState["Targets"] = "Dmg";
            }
        }

        private void healDone()
        {
            ViewState["Targets"] = "Dmg";
        }
    }
}