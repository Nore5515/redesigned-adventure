using System;
using System.Collections.Generic;
using EquipmentNamespace;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartHandler : MonoBehaviour
{
    [SerializeField] private Image head;
    [SerializeField] private Image body;
    [SerializeField] private Image legs;
    [SerializeField] private Image feet;
    [SerializeField] private Image hands;
    
    private Dictionary<ArmorSlot, Image> equipmentImages = new();
    public Dictionary<ArmorSlot, Equipment> equipmentDict = new(); // <slot, equipment>

    [SerializeField] private Sprite empty;


    private void Start()
    {
        equipmentDict.Add(ArmorSlot.HEAD, null);
        equipmentDict.Add(ArmorSlot.BODY, null);
        equipmentDict.Add(ArmorSlot.LEGS, null);
        equipmentDict.Add(ArmorSlot.FEET, null);
        equipmentDict.Add(ArmorSlot.HANDS, null);   
        
        equipmentImages.Add(ArmorSlot.HEAD, head);
        equipmentImages.Add(ArmorSlot.BODY, body);
        equipmentImages.Add(ArmorSlot.LEGS, legs);
        equipmentImages.Add(ArmorSlot.FEET, feet);
        equipmentImages.Add(ArmorSlot.HANDS, hands);
    }

    // TODO: not happy with this but ArmorSlot enum not appearing in inspector
    public void ClickEquipment(string stringSlot)
    {
        if (stringSlot == "head")
        {
            ClickEquipment(ArmorSlot.HEAD);
        }
        else if (stringSlot == "shirt")
        {
            ClickEquipment(ArmorSlot.BODY);
        }
        else if (stringSlot == "pants")
        {
            ClickEquipment(ArmorSlot.LEGS);
        }
        else if (stringSlot == "boots")
        {
            ClickEquipment(ArmorSlot.FEET);
        }
        else if (stringSlot == "hands")
        {
            ClickEquipment(ArmorSlot.HANDS);
        }
    }

    private void ClickEquipment(ArmorSlot slot)
    {
        Debug.Log(slot);
    }

    [CanBeNull]
    public Equipment GetEquipment(ArmorSlot slot)
    {
        return equipmentDict[slot];
    }

    [CanBeNull]
    public EquipmentNamespace.Equipment RemoveEquipment(ArmorSlot slot)
    {
        if (equipmentDict[slot] is null) return null;
        Equipment temp = equipmentDict[slot];
        equipmentDict[slot] = null;
        return temp;
    }
    
    public void AddNewEquipment(Equipment equipment)
    {
        equipmentDict[equipment.slot] = equipment;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        foreach (KeyValuePair<ArmorSlot, Equipment> kvp in equipmentDict)
        {
            if (kvp.Value is null)
            {
                equipmentImages[kvp.Key].sprite = empty;
            }
            else
            {
                equipmentImages[kvp.Key].sprite = kvp.Value.icon;
            }
        }
    }
}
