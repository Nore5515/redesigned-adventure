using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPurchaseButton : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField] private Equipment.Equipment equipment;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image.sprite = equipment.icon;
        GetComponentInChildren<TextMeshProUGUI>().text = equipment.name;

        ShopPanelUI shopObj = transform.parent.GetComponent<ShopPanelUI>();
        GetComponent<Button>().onClick.AddListener(() => shopObj.SelectEquipment(equipment));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
