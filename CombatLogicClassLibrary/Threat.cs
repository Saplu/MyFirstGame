using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLogicClassLibrary
{
    [Serializable]
    public class Threat
    {
        private int[] threatTable;

        public int[] ThreatTable { get => threatTable; set => threatTable = value; }

        public Threat()
        {
            threatTable = new int[4] { 25, 25, 25, 25 };
        }

        public int[] ManageThreat(int index, int amount)
        {
            threatTable[index] += amount + Convert.ToInt32(amount / 3);
            threatTable[0] -= Convert.ToInt32(amount / 3);
            threatTable[1] -= Convert.ToInt32(amount / 3);
            threatTable[2] -= Convert.ToInt32(amount / 3);
            threatTable[3] -= Convert.ToInt32(amount / 3);
            return threatTable;
        }

        public int ChooseEnemy()
        {
            var number = Utils.RandomProvider.GetRandom(1, 100);
            if (number <= threatTable[0])
                return 1;
            else if (number > threatTable[0] && number <= threatTable[0] + threatTable[1])
                return 2;
            else if (number > threatTable[0] + threatTable[1] && number <= threatTable[0] + threatTable[1] + threatTable[2])
                return 3;
            else return 4;
        }
    }
}
