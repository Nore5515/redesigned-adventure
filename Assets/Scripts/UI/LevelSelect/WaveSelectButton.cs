using System;
using ElevatorScripts;
using UnityEngine;
using UnityEngine.UI;

public class WaveSelectButton : MonoBehaviour
{
    private Elevator elevator;
    [SerializeField] private string levelName;
    
    public void OnClick()
    {
        elevator.SetDest(levelName);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elevator = GameObject.FindGameObjectWithTag("elevator").GetComponent<Elevator>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
