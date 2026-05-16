using System;
using System.Collections.Generic;
using EquipmentNamespace;
using Handler;
using Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class InvPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject invGrid;
    [SerializeField] private GameObject invButton;

    [SerializeField] private List<EquipmentNamespace.Equipment> testEquip;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;
    
    [SerializeField] private InventoryHandler inventoryHandler;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseInv();
    }

    public void AddItemButton(Equipment equipment)
    {
        GameObject button = Instantiate(invButton, invGrid.transform);
        button.GetComponent<InvItem>().Init(equipment);
    }

    public void EquipSelectedItem(Equipment equipment)
    {
        title.text = equipment.name;
        desc.text = equipment.description;
        inventoryHandler.EquipItem(equipment);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            AddItemButton(testEquip[new Random().Next(0, testEquip.Count)]);
        }
    }

    public void CloseInv()
    {
        Unpause();
        gameObject.SetActive(false);
    }
    
    public void OpenInv()
    {
        Pause();
        gameObject.SetActive(true);
        UpdateFromInv();
    }

    void UpdateFromInv()
    {
        ClearInv();
        List<Equipment> equippedItems = inventoryHandler.GetInventory();
        foreach (var item in equippedItems)
        {
            AddItemButton(item);
        }
    }
    
    void ClearInv()
    {
        foreach (Transform child in invGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
    
    public void Unpause()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
