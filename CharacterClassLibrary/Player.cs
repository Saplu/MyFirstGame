﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary.Enums;

namespace CharacterClassLibrary
{
    [Serializable]
    public abstract class Player : Character
    {
        private int xp;
        private string name;
        private Enums.ClassName className;
        private List<Item> items;
        private List<ItemType> itemTypes;
        private int[] cooldowns;

        public int Xp { get => xp; set => xp = value; }
        public string Name { get => name; set => name = value; }
        public ClassName ClassName { get => className; set => className = value; }
        public List<Item> Items { get => items; set => items = value; }
        public List<ItemType> ItemTypes { get => itemTypes; set => itemTypes = value; }
        public int[] Cooldowns { get => cooldowns; set => cooldowns = value; }

        public static Player Create(ClassName className)
        {
            switch(className)
            {
                case ClassName.Warrior: return new PlayerClasses.Warrior("asd");
                case ClassName.Mage: return new PlayerClasses.Mage("asd");
                case ClassName.BloodPriest: return new PlayerClasses.BloodPriest("asd");
                case ClassName.Protector: return new PlayerClasses.Protector("asd");
                case ClassName.Fairy: return new PlayerClasses.Fairy("asd");
                case ClassName.Shaman: return new PlayerClasses.Shaman("asd");
                case ClassName.Rogue: return new PlayerClasses.Rogue("asd");
                case ClassName.Templar: return new PlayerClasses.Templar("asd");
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public abstract string[] Ability1();
        public abstract string[] Ability2();
        public abstract string[] Ability3();
        public abstract string[] Ability4();

        public virtual int GetThreat(string id)
        {
            return 0;
        }

        public void ModifyCooldownLength()
        {
            var newcd = new int[4];
            for (int i = 0; i < 4; i++)
            {
                cooldowns[i]--;
                if (cooldowns[i] > 0)
                    newcd[i] = cooldowns[i];
            }
            Cooldowns = newcd;
        }

        public virtual void AfterCombatReset()
        {
            Health = MaxHealth;
            Cooldowns = new int[4] { 0, 0, 0, 4 };
            Statuses = new List<CombatLogicClassLibrary.Status>();
        }
    }
}
