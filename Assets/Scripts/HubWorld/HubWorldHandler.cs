using System;
using ElevatorScripts;
using TMPro;
using UnityEngine;

public class HubWorldHandler : MonoBehaviour
{
    [SerializeField]
    private Elevator elevator;

    [SerializeField] private TextMeshProUGUI dreamCoinText;
    private SaveObject saveObject;
    
    private void Start()
    {
        GameObject saveGO = GameObject.FindGameObjectWithTag("save");
        saveObject = saveGO.GetComponent<SaveObject>();
    }

    // Update is called once per frame
    void Update()
    {
        dreamCoinText.text = saveObject.GetSaveData().DreamCoin.ToString();
    }
}
