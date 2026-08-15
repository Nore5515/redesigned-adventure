using System.Collections.Generic;
using EquipmentNamespace;

namespace Player
{
    using UnityEngine;
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        public PlayerStatMods statMods = new();
        
        public int meleeDamage = 1;
        public int meleePoison = 0;
        public int spellDamageBoost = 0;
        
        public int hpRegen = 0;
        
        public int mp = 12;
        public int maxMp = 12;
        public int mpRegen = 1;

        public int xp = 0;
        public int maxXp = 100;
        public int level = 1;

        public float speed = 12.0f;

        public int score = 0;
  
        public int cash = 0;
        
        public event System.Action EquipmentChangeEvent;

        public void FireEquipmentChangeEvent()
        {
            EquipmentChangeEvent?.Invoke();
        }

        public List<Equipment> inventory = new();
        public Dictionary<ArmorSlot, Equipment> equippedItems = new()
        {
            { ArmorSlot.HEAD, null},
            { ArmorSlot.BODY, null},
            { ArmorSlot.LEGS, null},
            { ArmorSlot.FEET, null}, 
            {ArmorSlot.HANDS, null}
        }; // <slot, equipment>
    }
}