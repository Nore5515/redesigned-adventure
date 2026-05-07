using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class InvPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject invGrid;
    [SerializeField] private GameObject invButton;

    [SerializeField] private List<Equipment.Equipment> testEquip;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;
    
    [SerializeField] private BodyPartHandler bodyPartHandler;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseInv();
    }

    public void AddItem(Equipment.Equipment equipment)
    {
        GameObject button = Instantiate(invButton, invGrid.transform);
        button.GetComponent<InvItem>().Init(equipment);
    }

    public void SetSelectedItem(Equipment.Equipment equipment)
    {
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
