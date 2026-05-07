using System;
using System.Collections.Generic;
using NPCs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    private TextMeshProUGUI text;

    private List<DialogueLine> lines = new();
    private int currentIndex = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        GetComponent<Image>().enabled = false;
        text.enabled = false;
    }

    public void LoadLines(List<DialogueLine> _lines)
    {
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
            GetComponent<Image>().enabled = false;
            text.enabled = false;
        }
        else
        {
            text.text = lines[currentIndex].text;
        }
    }

    private void Update()
    {
        if (currentIndex < lines.Count && Input.GetMouseButtonDown(0))
        {
            LoadNextLine();
        }
    }
}
