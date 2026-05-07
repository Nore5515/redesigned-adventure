
using System.Collections.Generic;
using Passives;
using UnityEngine;

namespace Equipment
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Equipment", fileName = "Equipment")]
    public class Equipment : ScriptableObject
    {
        public List<Passive> passives;

        public ArmorSlot slot;

        public Sprite icon;
        
        public string name;
        public string description;
        public int cost;

    }
}
