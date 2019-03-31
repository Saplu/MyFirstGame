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
                var item = db.Item.ToList().Find(x => x.Owner == target && x.Place == itemPlace);
                item.Owner = "Inventory";
            }
        }
    }
}
