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
            <br />
            <asp:DropDownList ID="typeDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="typeDropDownList_SelectedIndexChanged" Visible="False">
                <asp:ListItem>Cloth</asp:ListItem>
                <asp:ListItem>Leather</asp:ListItem>
                <asp:ListItem>Mail</asp:ListItem>
                <asp:ListItem>Plate</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="placeLabel" runat="server" Text="Select place:" Visible="False"></asp:Label>
            <br />
            <asp:DropDownList ID="placeDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="placeDropDownList_SelectedIndexChanged" Visible="False">
                <asp:ListItem>Helmet</asp:ListItem>
                <asp:ListItem>Chest</asp:ListItem>
                <asp:ListItem>Legs</asp:ListItem>
                <asp:ListItem>Hands</asp:ListItem>
                <asp:ListItem>Feet</asp:ListItem>
                <asp:ListItem>MainHand</asp:ListItem>
                <asp:ListItem>OffHand</asp:ListItem>
                <asp:ListItem>Shield</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="playerLabel" runat="server" Text="Select character:" Visible="False"></asp:Label>
            <br />
            <asp:DropDownList ID="characterDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="characterDropDownList_SelectedIndexChanged" Visible="False">
            </asp:DropDownList>
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
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="backButton" runat="server" OnClick="backButton_Click" Text="Not Interested" Visible="False" />
            <br />
            <br />
            <asp:Label ID="offerLabel" runat="server"></asp:Label>
            <br />
            <asp:Label ID="exceptionLabel" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
