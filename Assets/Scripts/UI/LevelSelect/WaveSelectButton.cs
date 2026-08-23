using System;
using ElevatorScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSelectButton : MonoBehaviour
{
    private Elevator elevator;
    [SerializeField] private string levelName;
    [SerializeField] private string levelDescText;
    private TextMeshProUGUI levelDesc;
    
    public void OnClick()
    {
        elevator.SetDest(levelName);
        levelDesc.text = levelDescText;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelDesc = GameObject.FindGameObjectWithTag("level_desc").GetComponent<TextMeshProUGUI>();
        elevator = GameObject.FindGameObjectWithTag("elevator").GetComponent<Elevator>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
