using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityClassLibrary.Fairy
{
    public class Inspire : Ability
    {
        public Inspire()
        {
            Name = "Inspire";
            Description = "Read a chapter of \"The Lord of the Rings\" to greatly increase all combat stats of your party for 2 turns.";
            Cooldown = 5;
        }

        public int Action(int SpellPower)
        {
            return Convert.ToInt32(SpellPower * .5);
        }
    }
}
