<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CombatMenu.aspx.cs" Inherits="Combat.MainMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Pick your team:<br />
            <br />
            <asp:DropDownList ID="player1DropDownList" runat="server" AutoPostBack="True">
            </asp:DropDownList>
&nbsp;
            <asp:DropDownList ID="player2DropDownList" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            &nbsp;
            <asp:DropDownList ID="player3DropDownList" runat="server" AutoPostBack="True">
            </asp:DropDownList>
&nbsp;
            <asp:DropDownList ID="player4DropDownList" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <br />
            Pick your fight:<br />
            <br />
            <asp:RadioButtonList ID="fightRadioButtonList" runat="server">
            </asp:RadioButtonList>
            <br />
            <br />
            <asp:Button ID="okButton" runat="server" Text="GO" OnClick="okButton_Click" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
