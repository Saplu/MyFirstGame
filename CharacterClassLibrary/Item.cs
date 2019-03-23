﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary.Enums;

namespace CharacterClassLibrary
{
    [Serializable]
    public class Item
    {
        private int health;
        private int strength;
        private double crit;
        private int spellpower;
        private int armor;
        private string name;
        private int level;
        private ItemType itemType;
        private ItemPlace itemPlace;
        private string owner;

        public int Health { get => health; set => health = value; }
        public int Strength { get => strength; set => strength = value; }
        public double Crit { get => crit; set => crit = value; }
        public int Spellpower { get => spellpower; set => spellpower = value; }
        public int Armor { get => armor; set => armor = value; }
        public string Name { get => name; set => name = value; }
        public int Level { get => level; set => level = value; }
        public ItemType ItemType { get => itemType; set => itemType = value; }
        public ItemPlace ItemPlace { get => itemPlace; set => itemPlace = value; }
        public string Owner { get => owner; set => owner = value; }

        public Item(int health, int strength, int crit, int spellPower, int armor, string name, int level,
            ItemType itemtype, ItemPlace itemplace, string owner)
        {
            Health = health;
            Strength = strength;
            Crit = crit;
            Spellpower = spellPower;
            Armor = armor;
            Name = name;
            Level = level;
            ItemType = itemtype;
            ItemPlace = itemplace;
            Owner = owner;
        }

        public Item(int level)
        {
            Level = level;
        }
    }
}
