using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary.Enums;

namespace CharacterClassLibrary
{
    [Serializable]
    public class RandomItemGenerator
    {
        public Item CreateItem(int level)
        {
            var item = new Item(level);
            var type = casterOrPhysical();
            item.ItemType = (ItemType)Enum.Parse(typeof(ItemType), chooseItemType(type).ToString());
            item.ItemPlace = (ItemPlace)Enum.Parse(typeof(ItemPlace), chooseItemPlace().ToString());
            if (Convert.ToInt32(item.ItemPlace) == 0 || Convert.ToInt32(item.ItemPlace) == 1 || Convert.ToInt32(item.ItemPlace) == 8)
                item.ItemType = ItemType.Cloth;
            if (Convert.ToInt32(item.ItemPlace) == 7)
                item.ItemType = ItemType.Mail;
            item.Armor = getItemArmor(item.ItemType, item.ItemPlace, level);
            var usablePower = getItemPower(item.ItemPlace, item.Level);
            spreadPower(usablePower, item, type);

            return item;
        }

        private int casterOrPhysical()
        {
            var type = Utils.RandomProvider.GetRandom(0, 1);
            return type;
        }

        private int chooseItemType(int type)
        {
            if (type == 0)
                return Utils.RandomProvider.GetRandom(0, 2);
            else return Utils.RandomProvider.GetRandom(1, 3);
        }

        private int chooseItemPlace()
        {
            var place = Utils.RandomProvider.GetRandom(0, 6);
            if (place == 1)
            {
                var type = Utils.RandomProvider.GetRandom(0, 2);
                switch (type)
                {
                    case 0: return 1;
                    case 1: return 7;
                    case 2: return 8;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            else return place;
        }

        private int getItemPower(ItemPlace itemPlace, int level)
        {
            var value = Convert.ToInt32(itemPlace);
            switch(value)
            {
                case 0: return level * 5;
                case 1: return level * 2;
                case 2: return level * 3;
                case 3: return level * 4;
                case 4: return level * 2;
                case 5: return level * 4;
                case 6: return level * 2;
                case 7: return level * 2;
                case 8: return level * 2;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private int getItemArmor(ItemType itemType, ItemPlace itemPlace, int level)
        {
            var type = Convert.ToInt32(itemType) + 1;
            var place = Convert.ToInt32(itemPlace);
            switch (place)
            {
                case 0: return 0;
                case 1: return 0;
                case 8: return 0;
                case 2: return Convert.ToInt32(type * level * 1.2);
                case 3: return type * level * 2;
                case 4: return Convert.ToInt32(type * level * .8);
                case 5: return Convert.ToInt32(type * level * 1.5);
                case 6: return type * level;
                case 7: return Convert.ToInt32(type * level * 2.5);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void spreadPower(int usablePower, Item item, int type)
        {
            if (type == 0)
                item.Strength = 0;
            else if (type == 1)
                item.Spellpower = 0;
            switch (Convert.ToInt32(item.ItemPlace))
            {
                case 0: spreadWeaponPower(usablePower, item, type);
                    break;
                case 1: spreadWeaponPower(usablePower, item, type);
                    break;
                case 8: spreadWeaponPower(usablePower, item, type);
                    break;
                case 7: spreadShieldPower(usablePower, item, type);
                    break;
                default: spreadArmorPower(usablePower, item, type);
                    break;
            }
        }

        private void spreadWeaponPower(int usablePower, Item item, int type)
        {
            if (type == 0)
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 3: item.Crit++; break;
                        default: item.Spellpower++; break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 1: item.Crit++; break;
                        case 2: item.Health += 7; break;
                        default: item.Strength++; break;
                    }
                }
            }
        }

        private void spreadShieldPower(int usablePower, Item item, int type)
        {
            if (type == 0)
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 3);
                    switch(value)
                    {
                        case 1: item.Spellpower++; break;
                        case 2: item.Crit += .8; break;
                        case 3: item.Health += 8; break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 1: item.Strength++; break;
                        default: item.Health += 10; break;
                    }
                }
            }
        }

        private void spreadArmorPower(int usablePower, Item item, int type)
        {
            if (type == 0)
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 1: item.Health += 8; break;
                        case 2: item.Crit += .6; break;
                        default: item.Spellpower++; break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 1: item.Health += 9; break;
                        case 2: item.Crit += .6; break;
                        default: item.Strength++; break;
                    }
                }
            }
        }
    }
}
