using System;
using System.Collections.Generic;
using NPCs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private float talkDist = 10.0f;
    
    private TextMeshProUGUI text;
    private GameObject playerGO;

    private List<DialogueLine> lines = new();
    private int currentIndex = 0;
    
    Action endAction;
    private Vector3 sourcePos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        GetComponent<Image>().enabled = false;
        text.enabled = false;
    }

    public void LoadLines(List<DialogueLine> _lines, Action onEnd = null, Vector3 sourcePos = default)
    {
        this.sourcePos = sourcePos;
        endAction = onEnd;
        currentIndex = 0;
        GetComponent<Image>().enabled = true;
        text.enabled = true;
        lines = _lines;
        text.text = lines[currentIndex].text;
    }

    public void LoadNextLine()
    {
        currentIndex++;
        if (currentIndex == lines.Count)
        {
            StopTalking();
        }
        else
        {
            text.text = lines[currentIndex].text;
        }
    }

    private void StopTalking()
    {
        GetComponent<Image>().enabled = false;
        endAction?.Invoke();
        text.enabled = false;
    }

    private void Update()
    {
        if (text.enabled)
        {
            if (Vector3.Distance(playerGO.transform.position, sourcePos) > talkDist)
            {
                StopTalking();
            }
        }
        
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            if (currentIndex < lines.Count)
            {
                LoadNextLine();
            }
        }
    }
}
