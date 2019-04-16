using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ItemDAO
    {
        CharacterDbEntities db = new CharacterDbEntities();

        public List<Item> GetItems()
        {
            var list = new List<Item>();
            foreach(var item in db.Item)
            {
                list.Add(item);
            }
            return list;
        }

        public int GetMoney()
        {
            var list = db.Bank.ToList();
            var money = list[0].Money;
            return money;
        }

        public void ManageMoney(int value)
        {
            var money = GetMoney();
            if (value >= 0)
                money += value;
            else if (value < 0 && money >= -value)
                money += value;
            else throw new Exception("Not enough money.");
            foreach (var item in db.Bank)
                item.Money = money;
            db.SaveChanges();
        }

        public List<Item> GetInventoryItems()
        {
            var list = new List<Item>();
            foreach (var item in db.Item)
            {
                if (item.Owner == "Inventory")
                    list.Add(item);
            }
            return list;
        }

        public void AddNewItem(Item item)
        {
            var list = db.Item.ToList();
            var previous = list.Last().Id;
            item.Id = previous + 1;
            db.Item.Add(item);
            db.SaveChanges();
        }

        public string itemToString(Item item)
        {
            var value = item.Name + "<br/>Health: " + item.Health + "<br/>Strength: " + item.Strength +
                "<br/>Spellpower: " + item.SpellPower + "<br/>Armor: " + item.Armor + "<br/>Crit: " + item.Crit;
            return value;
        }

        public void removeCurrentItem(int itemPlace, string target)
        {
            if (db.Item.ToList().Exists(x => x.Owner == target && x.Place == itemPlace))
            {
                var item = db.Item.ToList().Find(x => (x.Owner == target && x.Place == itemPlace) || 
                (x.Owner == target && x.Place == 1 && itemPlace == 7) || (x.Owner == target && x.Place == 7 && itemPlace == 1));
                item.Owner = "Inventory";
                db.SaveChanges();
            }
        }

        public void DeleteItem(int id)
        {
            var list = db.Item.ToList();
            db.Item.Remove(db.Item.ToList().Find(x => x.Id == id));
            db.SaveChanges();
        }
    }
}
