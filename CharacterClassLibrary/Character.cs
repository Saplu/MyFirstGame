using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterClassLibrary
{
    [Serializable]
    public abstract class Character
    {
        private int health;
        private int maxHealth;
        private int strength;
        private int crit;
        private int spellPower;
        private int armor;
        private int level;
        private bool alive;

        public int Health { get => health; set => health = value; }
        public int Strength { get => strength; set => strength = value; }
        public int Crit { get => crit; set => crit = value; }
        public int SpellPower { get => spellPower; set => spellPower = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Level { get => level; set => level = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public bool Alive { get => alive; set => alive = value; }

        public abstract string setPic();

        public override string ToString()
        {
            return "Health: " + health;
        }
    }
}
