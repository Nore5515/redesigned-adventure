using Equipment;
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

    private Equipment.Equipment helmet;
    private Equipment.Equipment shirt;
    private Equipment.Equipment pants;
    private Equipment.Equipment boots;
    private Equipment.Equipment gloves;

    [SerializeField] private Sprite empty;


    public void AddNewEquipment(Equipment.Equipment equipment)
    {
        if (equipment.slot == ArmorSlot.HEAD)
        {
            helmet = equipment;
        }
        else if (equipment.slot == ArmorSlot.BODY)
        {
            shirt = equipment;
        }
        else if (equipment.slot == ArmorSlot.LEGS)
        {
            pants = equipment;
        }
        else if (equipment.slot == ArmorSlot.FEET)
        {
            boots = equipment;
        }
        else if (equipment.slot == ArmorSlot.HANDS)
        {
            gloves = equipment;
        }
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        head.sprite = helmet?.icon ?? empty;
        body.sprite = shirt?.icon ?? empty;
        legs.sprite = pants?.icon ?? empty;
        feet.sprite = boots?.icon ?? empty;
        hands.sprite = gloves?.icon ?? empty;
    }
}
