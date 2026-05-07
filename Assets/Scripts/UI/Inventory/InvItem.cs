using UnityEngine;
using UnityEngine.UI;

public class InvItem : MonoBehaviour
{
    
    [SerializeField]
    public Equipment.Equipment equipment;

    [SerializeField] private Image itemIcon;
    private InvPanelUI invPanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateToEquipment();
        invPanel = GetComponentInParent<InvPanelUI>();
    }

    public void Init(Equipment.Equipment equipment)
    {
        this.equipment = equipment;
        UpdateToEquipment();
    }

    public void OnClick()
    {
        Debug.Log("Click!");
        Debug.Log(equipment.name);
        invPanel.SetSelectedItem(equipment);
    }

    void UpdateToEquipment()
    {
        itemIcon.sprite = equipment.icon;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
