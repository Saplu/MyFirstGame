<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VictoryPage.aspx.cs" Inherits="Combat.VictoryPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Victory!<br />
            <br />
            Your survivors earned
            <asp:Label ID="xpLabel" runat="server"></asp:Label>
&nbsp;experience.<br />
            <br />
            Your loot:<br />
            <asp:Label ID="lootLabel" runat="server"></asp:Label>
            <br />
            <br />
            Select which player to give loot:<br />
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            </asp:RadioButtonList>
            Currently wearing:<br />
            <asp:Label ID="currentLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="okButton" runat="server" OnClick="okButton_Click" Text="Great!" />
        </div>
    </form>
</body>
</html>
