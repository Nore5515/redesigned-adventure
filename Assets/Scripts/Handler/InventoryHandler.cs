using System.Collections.Generic;
using EquipmentNamespace;
using JetBrains.Annotations;
using Player;
using UnityEditor;
using UnityEngine;

namespace Handler
{
    public class InventoryHandler: MonoBehaviour
    {
        [SerializeField] private PlayerStats playerStats;
        
        [SerializeField] private InvPanelUI invPanelUI;
        [SerializeField] private BodyPartHandler bodyPartHandler;
        
        public void AddItem(Equipment item)
        {
            playerStats.inventory.Add(item);
            invPanelUI.AddItemButton(item);
        }

        public bool RemoveItem(Equipment item)
        {
            bool r = playerStats.inventory.Remove(item);
            // Update InvPanelUI
            return r;
        }

        public Equipment GetEquipmentInSlot(ArmorSlot slot)
        {
            return playerStats.equippedItems[slot];
        }

        public List<Equipment> GetInventory()
        {
            Debug.Log(playerStats.inventory.Count);
            return playerStats.inventory;
        }

        public Dictionary<ArmorSlot, Equipment> GetEquippedItems()
        {
            return playerStats.equippedItems;
        }

        public void EquipItem(Equipment equipment)
        {
            // Move old equipment out of slot and into inventory
            Equipment oldEquipment = playerStats.equippedItems[equipment.slot];
            if (oldEquipment != null)
            {
                AddItem(oldEquipment);
            }
            
            // Move new equipment into slot and out of inv
            playerStats.equippedItems[equipment.slot] = equipment;
            RemoveItem(equipment);
            
            // Update BodyPartHandler
            bodyPartHandler.SetEquipment(equipment);
        }
    }
}