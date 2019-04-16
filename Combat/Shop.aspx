<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="Combat.Shop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            K-Market Moisio<br />
            <br />
            <asp:GridView ID="itemGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="itemGridView_RowCommand" Visible="False">
                <Columns>
                    <asp:ButtonField Text="Sell" />
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="SellValue" HeaderText="Value" />
                </Columns>
            </asp:GridView>
            <br />
            <br />
            <asp:Label ID="typeLabel" runat="server" Text="Select type:" Visible="False"></asp:Label>
            <asp:RadioButtonList ID="typeRadioButtonList" runat="server" Visible="False">
                <asp:ListItem>Cloth</asp:ListItem>
                <asp:ListItem>Leather</asp:ListItem>
                <asp:ListItem>Mail</asp:ListItem>
                <asp:ListItem>Plate</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:Label ID="placeLabel" runat="server" Text="Select place:" Visible="False"></asp:Label>
            <asp:RadioButtonList ID="placeRadioButtonList" runat="server" Visible="False">
                <asp:ListItem>Helmet</asp:ListItem>
                <asp:ListItem>Chest</asp:ListItem>
                <asp:ListItem>Legs</asp:ListItem>
                <asp:ListItem>Hands</asp:ListItem>
                <asp:ListItem>Feet</asp:ListItem>
                <asp:ListItem>MainHand</asp:ListItem>
                <asp:ListItem>OffHand</asp:ListItem>
                <asp:ListItem>Shield</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:Label ID="playerLabel" runat="server" Text="Select character:"></asp:Label>
            <br />
            <asp:RadioButtonList ID="characterRadioButtonList" runat="server" Visible="False">
            </asp:RadioButtonList>
            <br />
            <br />
            <asp:Label ID="moneyLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="sellButton" runat="server" OnClick="sellButton_Click" Text="Sell Items" />
            <br />
            <br />
            <asp:Button ID="craftButton" runat="server" OnClick="craftButton_Click" Text="Craft Item" />
            <br />
            <br />
            <asp:Button ID="confirmButton" runat="server" OnClick="confirmButton_Click" Text="Confirm" Visible="False" />
            <br />
            <br />
            <asp:Label ID="exceptionLabel" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
