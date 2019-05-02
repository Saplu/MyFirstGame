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
            itemDAO.removeCurrentItem(dbItem.Place, dbItem.Owner);
            itemDAO.AddNewItem(dbItem);
        }

        public void ManageMoney(CharacterClassLibrary.Item item)
        {
            ItemDAO itemDAO = new ItemDAO();
            itemDAO.ManageMoney(-item.SellValue * 4);
        }

        public string CurrentOffer(int type, int place, DAO.Player buyer)
        {
            var typeString = offerString(type);
            var Place = placeString(place);
            var value = new RandomItemGenerator();
            var placeEnum = (CharacterClassLibrary.Enums.ItemPlace)Enum.Parse(typeof(CharacterClassLibrary.Enums.ItemPlace), place.ToString());
            var price = value.GetItemPower(placeEnum, buyer.Level, CharacterClassLibrary.Enums.ItemQuality.Good) * 8;
            var offer = "Level " + buyer.Level + " " + typeString + " " + Place + 
                "<br/>Price: " + price + ". Don't bother bargaining, I am computer.";
            return offer;
        }

        public string CurrentlyWearing(string name, int place)
        {
            var current = new ItemDAO();
            return current.GetCurrentItem(name, place);
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

        private string offerString(int type)
        {
            switch(type)
            {
                case 0: return "Cloth";
                case 1: return "Leather";
                case 2: return "Mail";
                case 3: return "Plate";
                default: throw new Exception("Something weird has happened. Please contact our customer support.");
            }
        }

        private string placeString(int place)
        {
            switch(place)
            {
                case 0: return "Main Hand";
                case 1: return "Offhand";
                case 2: return "Helmet";
                case 3: return "Chest";
                case 4: return "Gloves";
                case 5: return "Pants";
                case 6: return "Shoes";
                case 7: return "Shield";
                default: throw new Exception("Something weird has happened. Please contarc our customer support.");
            }
        }
    }
}
