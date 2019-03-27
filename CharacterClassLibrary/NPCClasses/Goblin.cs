using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityClassLibrary.NPC;
using Utils;

namespace CharacterClassLibrary.NPCClasses
{
    [Serializable]
    public class Goblin : NPC, Interfaces.CombatInterface
    {
        public Goblin(int level)
        {
            Level = level;
            Health = 70 + (35 * level);
            MaxHealth = Health;
            Strength = 7 + (level * 4);
            Crit = 10;
            SpellPower = 0;
            Armor = level * 5;
            Statuses = new List<CombatLogicClassLibrary.Status>();
        }

        private int whirlwind()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var whirl = new Whirlwind();
            return whirl.Action(Strength, Crit, multi, increase);
        }

        private int execute()
        {
            var multi = getAttackMultiplier();
            var increase = getAttackModifier();
            var exe = new Execute();
            return exe.Action(Strength, Crit, multi, increase);
        }

        public override string ChooseAbility()
        {
            if (RandomProvider.GetRandom(1, 100) > 70)
                return "Whirlwind";
            else return "Execute";
        }

        public override int GetTargets(string id)
        {
            if (id == "Whirlwind")
                return 2;
            else if (id == "Execute")
                return 1;
            else return 1;
        }

        public override int UseAbility(string id)
        {
            if (id == "Whirlwind")
                return whirlwind();
            else if (id == "Execute")
                return execute();
            else return 1;
        }

        public override string setPic()
        {
            return "Pictures\\Goblin.jpg";
        }

        public override List<int> setStatusTargets(string id, int targetPosition, int enemyCount)
        {
            var list = new List<int>();
            return list;
        }

        public override double setStatusEffect(string id)
        {
            return 0;
        }
    }
}
