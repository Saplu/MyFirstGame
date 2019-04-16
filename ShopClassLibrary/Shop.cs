using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;
using DAO;

namespace ShopClassLibrary
{
    public class Shop
    {
        private List<DAO.Player> players;
        private int money;
        private List<DAO.Item> items;
        private List<DAO.Item> inventoryItems;

        public List<DAO.Player> Players { get => players; set => players = value; }
        public int Money { get => money; set => money = value; }
        public List<DAO.Item> Items { get => items; set => items = value; }
        public List<DAO.Item> InventoryItems { get => inventoryItems; set => inventoryItems = value; }

        public Shop()
        {
            DAO.CharacterDAO dao = new DAO.CharacterDAO();
            DAO.ItemDAO itemDAO = new DAO.ItemDAO();
            Players = dao.GetPlayers();
            Money = itemDAO.GetMoney();
            Items = itemDAO.GetItems();
            InventoryItems = itemDAO.GetInventoryItems();
        }

        public int SaleItemType(string type)
        {
            switch (type)
            {
                case "Cloth": return 0;
                case "Leather": return 1;
                case "Mail": return 2;
                case "Plate": return 3;
                default: throw new Exception("No type selected.");
            }
        }

        public int SaleItemPlace(string place)
        {
            switch (place)
            {
                case "MainHand": return 0;
                case "OffHand": return 1;
                case "Helmet": return 2;
                case "Chest": return 3;
                case "Hands": return 4;
                case "Legs": return 5;
                case "Feet": return 6;
                case "Shield": return 7;
                default: throw new Exception("No place selected.");
            }
        }

        public int ManageType(int place, int type)
        {
            if (place == 0 || place == 1)
                type = 0;
            if (place == 7)
                type = 2;
            return type;
        }

        public DAO.Player setBuyer(string target, int type)
        {
            var buyer = Players.Find(x => x.Id == target);
            typeValid(type, buyer);
            return buyer;
        }

        public int casterOrPhysical(int playerClass)
        {
            if (playerClass == 0 || playerClass == 3 || playerClass == 6)
                return 1;
            else return 0;
        }

        public void AddItem(CharacterClassLibrary.Item item)
        {
            var dbItem = convertToDbItem(item);
            ItemDAO itemDAO = new ItemDAO();
            itemDAO.AddNewItem(dbItem);
            itemDAO.removeCurrentItem(dbItem.Place, dbItem.Owner);
        }

        public void ManageMoney(CharacterClassLibrary.Item item)
        {
            ItemDAO itemDAO = new ItemDAO();
            itemDAO.ManageMoney(-item.SellValue * 4);
        }

        private void typeValid(int type, DAO.Player player)
        {
            if ((player.Class == 1 || player.Class == 4) && type > 0)
                throw new Exception("Target cannot use the armor type");
            if ((player.Class == 2 || player.Class == 6) && type > 1)
                throw new Exception("Target cannot use the armor type");
            if ((player.Class == 0 || player.Class == 5) && type > 2)
                throw new Exception("target Cannot use the armor type");
        }

        private DAO.Item convertToDbItem(CharacterClassLibrary.Item item)
        {
            var dbItem = new DAO.Item();
            dbItem.Armor = item.Armor;
            dbItem.Crit = item.Crit;
            dbItem.Health = item.Health;
            dbItem.Owner = item.Owner;
            dbItem.Place = Convert.ToInt32(item.ItemPlace);
            dbItem.SpellPower = item.Spellpower;
            dbItem.Strength = item.Strength;
            dbItem.Type = Convert.ToInt32(item.ItemType);
            dbItem.Name = item.Name;
            dbItem.SellValue = item.SellValue;
            return dbItem;
        }
    }
}
