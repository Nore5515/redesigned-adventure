using System.Collections.Generic;
using Abilities;
using UnityEditor;

namespace Passives.PassivePool
{
    public class SpellDuper : Passive
    {
        public List<Trigger> triggers { get; set; }
        public List<Effect> effects { get; set; }
    }
}