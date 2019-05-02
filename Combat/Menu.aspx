<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Combat.Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            MyFirstGame<br />
            <br />
            <asp:Button ID="fightButton" runat="server" OnClick="fightButton_Click" Text="Fight" />
            <br />
            <br />
            <asp:Button ID="playerButton" runat="server" Text="New Player" OnClick="playerButton_Click" />
            <br />
            <br />
            <asp:Button ID="shopButton" runat="server" OnClick="shopButton_Click" Text="Shop" />
            <br />
        </div>
    </form>
</body>
</html>
