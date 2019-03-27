using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionClassLibrary
{
    [Serializable]
    public class SuccessfulMission
    {
        private Mission mission;

        public Mission Mission { get => mission; set => mission = value; }

        public SuccessfulMission()
        {
            
        }

        public List<CharacterClassLibrary.Player> GetSurvivors(List<string> ids)
        {
            var survive = new List<CharacterClassLibrary.Player>();
            foreach (var thing in mission.Players)
            {
                if (ids.Exists(x => x == thing.Name))
                    survive.Add(thing);
            }
            return survive;
        }
    }
}
