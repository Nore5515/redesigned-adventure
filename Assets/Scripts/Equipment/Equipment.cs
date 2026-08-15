
using System;
using System.Collections.Generic;
using Passives;
using UnityEditor;
using UnityEngine;

namespace EquipmentNamespace
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Equipment", fileName = "Equipment")]
    public class Equipment : ScriptableObject
    {
        public Guid id = Guid.NewGuid();
        
        public Passive passive;

        public ArmorSlot slot;
        
        public Sprite icon;
        
        public string name;
        public string description;
        public int cost;
        
    }
}
