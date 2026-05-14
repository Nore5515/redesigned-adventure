using System;
using System.Collections.Generic;
using EquipmentNamespace;
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
    
    [SerializeField] private BodyPartHandler bodyPartHandler;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseInv();
    }

    public void AddItem(Equipment equipment)
    {
        GameObject button = Instantiate(invButton, invGrid.transform);
        button.GetComponent<InvItem>().Init(equipment);
    }

    public void EquipSelectedItem(Equipment equipment)
    {
        // Remove item from bodypart first
        if (bodyPartHandler.GetEquipment(equipment.slot) != null)
        {
            AddItem(bodyPartHandler.RemoveEquipment(equipment.slot));
        }
        title.text = equipment.name;
        desc.text = equipment.description;
        bodyPartHandler.AddNewEquipment(equipment);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            AddItem(testEquip[new Random().Next(0, testEquip.Count)]);
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
        PlayerHandler ph = GameObject.FindGameObjectWithTag("player_handler").GetComponent<PlayerHandler>();
        foreach (var item in ph.playerStats.inventory)
        {
            AddItem(item);
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
