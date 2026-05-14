using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPurchaseButton : MonoBehaviour
{
    [SerializeField]
    private Image image;
    
    [SerializeField]
    private Sprite soldOutIcon;

    [SerializeField] private EquipmentNamespace.Equipment equipment;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetEquipment(equipment);
    }

    public void SetEquipment(EquipmentNamespace.Equipment newEqup)
    {
        GetComponent<Button>().interactable = true;
        equipment = newEqup;
        image.sprite = equipment.icon;
        GetComponentInChildren<TextMeshProUGUI>().text = equipment.name;

        ShopPanelUI shopObj = transform.parent.GetComponent<ShopPanelUI>();
        GetComponent<Button>().onClick.AddListener(() => shopObj.SelectEquipment(equipment, this));
    }

    public void DisableButton()
    {
        GetComponent<Button>().interactable = false;
        image.sprite = soldOutIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
