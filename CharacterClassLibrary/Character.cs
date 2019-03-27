using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatLogicClassLibrary;

namespace CharacterClassLibrary
{
    [Serializable]
    public abstract class Character
    {
        private int health;
        private int maxHealth;
        private int strength;
        private double crit;
        private int spellPower;
        private int armor;
        private int level;
        private int position;
        private List<Status> statuses;

        public int Health { get => health; set => health = value; }
        public int Strength { get => strength; set => strength = value; }
        public double Crit { get => crit; set => crit = value; }
        public int SpellPower { get => spellPower; set => spellPower = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Level { get => level; set => level = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Position { get => position; set => position = value; }
        public List<Status> Statuses { get => statuses; set => statuses = value; }

        public abstract string setPic();
        public abstract List<int> setStatusTargets(string id, int targetPosition, int enemyCount);
        public abstract int GetTargets(string id);
        public abstract int UseAbility(string id);
        public abstract double setStatusEffect(string id);

        public virtual void Defend(int incomingDmg)
        {
            var dmg = getDefenseModifier() + incomingDmg;
            dmg -= (armor / 4);
            if (dmg > 1)
                Health -= dmg;
            else Health -= 1;
        }

        public virtual void TrueDmgDefend(int incomingDmg)
        {
            if (incomingDmg > 1)
                Health -= incomingDmg;
            else Health -= 1;
        }

        public override string ToString()
        {
            return "Health: " + health;
        }

        public void ModifyStatusLength()
        {
            var keepList = new List<Status>();
            foreach (var status in Statuses)
            {
                status.Duration--;
                if (status.Duration > 0)
                {
                    keepList.Add(status);
                }
            }
            Statuses = keepList;
        }

        public void SetStatuses(string id, List<Player> players, List<NPC> enemies, int targetPosition)
        {
            var targetList = setStatusTargets(id, targetPosition, enemies.Count);
            var effect = setStatusEffect(id);
            if (targetList.Count > 0)
            {
                var status = Status.Create(id, targetList, effect);
                foreach (var thing in targetList)
                {
                    Character target = players.Find(x => x.Position == thing);
                    if (target == null)
                        target = enemies.Find(x => x.Position == thing);
                    target.Statuses.Add(status);
                }
            }
        }

        public bool isStunned()
        {
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.Stun) return true;
            }
            return false;
        }

        public void ApplyDoT()
        {
            foreach (var status in Statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.DoT)
                    Defend(Convert.ToInt32(status.Effect));
            }
        }

        protected double getAttackMultiplier()
        {
            var attackMultiplier = (double)1;
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.AttackDmgMultiplier)
                    attackMultiplier += (status.Effect - 1);
            }
            return attackMultiplier;
        }

        protected int getAttackModifier()
        {
            var attackModifier = 0;
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.AttackDmgModifier)
                    attackModifier += Convert.ToInt32(status.Effect);
            }
            return attackModifier;
        }

        private int getDefenseModifier()
        {
            var defenseModifier = 0;
            foreach (var status in statuses)
            {
                if (status is CombatLogicClassLibrary.Statuses.TakenDmgModifier)
                    defenseModifier += Convert.ToInt32(status.Effect);
            }
            return defenseModifier;
        }
    }
}
