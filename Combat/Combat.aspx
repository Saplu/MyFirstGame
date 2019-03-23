<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Combat.aspx.cs" Inherits="Combat.Combat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ImageButton ID="enemy1ImageButton" runat="server" Height="100px" Width="100px" Enabled="False" OnClick="enemy1ImageButton_Click" BorderColor="White" BorderWidth="5px" />
&nbsp;
            <asp:ImageButton ID="enemy2ImageButton" runat="server" Height="100px" Width="100px" Enabled="False" OnClick="enemy2ImageButton_Click" BorderColor="White" BorderWidth="5px" />
&nbsp;
            <asp:ImageButton ID="enemy3ImageButton" runat="server" Height="100px" Width="100px" Enabled="False" OnClick="enemy3ImageButton_Click" BorderColor="White" BorderWidth="5px" />
&nbsp;
            <asp:ImageButton ID="enemy4ImageButton" runat="server" Height="100px" Width="100px" Enabled="False" OnClick="enemy4ImageButton_Click" BorderColor="White" BorderWidth="5px" />
            <br />
            <asp:Label ID="enemy1Label" runat="server"></asp:Label>
&nbsp;
            <asp:Label ID="enemy2Label" runat="server"></asp:Label>
&nbsp;
            <asp:Label ID="enemy3Label" runat="server"></asp:Label>
&nbsp;
            <asp:Label ID="enemy4Label" runat="server"></asp:Label>
            <br />
            <br />
            <asp:ImageButton ID="player1ImageButton" runat="server" Height="100px" Width="100px" OnClick="player1ImageButton_Click" BorderColor="White" BorderWidth="5px" />
&nbsp;
            <asp:ImageButton ID="player2ImageButton" runat="server" Height="100px" Width="100px" OnClick="player2ImageButton_Click" BorderColor="White" BorderWidth="5px" />
&nbsp;
            <asp:ImageButton ID="player3ImageButton" runat="server" Height="100px" Width="100px" OnClick="player3ImageButton_Click" BorderColor="White" BorderWidth="5px" />
&nbsp;
            <asp:ImageButton ID="player4ImageButton" runat="server" Height="100px" Width="100px" OnClick="player4ImageButton_Click" BorderColor="White" BorderWidth="5px" />
            <br />
            <asp:Label ID="player1Label" runat="server"></asp:Label>
&nbsp;
            <asp:Label ID="player2Label" runat="server"></asp:Label>
&nbsp;
            <asp:Label ID="player3Label" runat="server"></asp:Label>
&nbsp;
            <asp:Label ID="player4Label" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Button ID="skill1Button" runat="server" Visible="False" OnClick="skill1Button_Click" BorderColor="White" BorderWidth="1px" />
&nbsp;
            <asp:Button ID="skill2Button" runat="server" Visible="False" OnClick="skill2Button_Click" BorderColor="White" BorderWidth="1px" />
&nbsp;
            <asp:Button ID="skill3Button" runat="server" Visible="False" OnClick="skill3Button_Click" BorderColor="White" BorderWidth="1px" />
&nbsp;
            <asp:Button ID="skill4Button" runat="server" Visible="False" OnClick="skill4Button_Click" BorderColor="White" BorderWidth="1px" />
            <br />
            <br />
            <br />
            <asp:Button ID="endTurnButton" runat="server" Text="End Turn" OnClick="endTurnButton_Click" />
&nbsp;<asp:Button ID="endButton" runat="server" OnClick="endButton_Click" Text="End mission" Visible="False" />
&nbsp;<asp:Label ID="turnLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="resultLabel" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
