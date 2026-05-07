using System.Collections.Generic;
using Abilities;

namespace Passives
{
    public interface Passive
    {

        public List<Trigger> triggers { get; set; }
        public List<Effect> effects { get; set; }
        
    }
}