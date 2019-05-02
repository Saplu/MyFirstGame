<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewCharacter.aspx.cs" Inherits="Combat.NewCharacter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Create a new character! You should make at least 4 to start playing! Isn&#39;t that fun!?<br />
            <br />
            Name:<br />
            <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            Choose class:<asp:RadioButtonList ID="classRadioButtonList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="classRadioButtonList_SelectedIndexChanged">
                <asp:ListItem Value="3" Selected="True">Protector</asp:ListItem>
                <asp:ListItem Value="7">Templar</asp:ListItem>
                <asp:ListItem Value="2">Blood Priest</asp:ListItem>
                <asp:ListItem Value="4">Fairy</asp:ListItem>
                <asp:ListItem Value="1">Mage</asp:ListItem>
                <asp:ListItem Value="6">Rogue</asp:ListItem>
                <asp:ListItem Value="5">Shaman</asp:ListItem>
                <asp:ListItem Value="0">Warrior</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:Button ID="okButton" runat="server" OnClick="okButton_Click" Text="Create" />
            <br />
            <br />
            <asp:Label ID="resultLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="menuButton" runat="server" OnClick="menuButton_Click" Text="Main Menu" />
        </div>
    </form>
</body>
</html>
