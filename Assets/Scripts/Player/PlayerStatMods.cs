using System;
using System.Collections.Generic;
using EquipmentNamespace;

namespace Player
{
    [Serializable]
    public class PlayerStatMods
    {
        public int meleeDamageBonus = 0;
        public int meleePoisonBonus = 0;
        
        public int maxHpBonus = 0;
        public int hpRegenBonus = 0;
        
        public int maxMpBonus = 0;
        public int mpRegenBonus = 0;
        
        public float speedMultiplier = 1.0f;
        public float jumpMultiplier = 1.0f;
    }
}