using System;
using Interfaces;
using UnityEngine;

public class StaticCharacter : MonoBehaviour, Interactable
{
    public Dialogue dialogue;

    [SerializeField] private Material spokenWithMaterial;
    [SerializeField] private bool changeColorAfterSpeaking;
    private GameObject dialogueBox; // TODO - Replace with dialogue system

    private bool isSpeaking = false;
    
    public void Start()
    {
        dialogueBox = GameObject.FindGameObjectWithTag("dialogue_box");
    }

    public Dialogue Prompt()
    {
        if (changeColorAfterSpeaking)
        {
            gameObject.GetComponent<Renderer>().material = spokenWithMaterial;
        }
        return dialogue;
    }
    
    public void StopSpeaking()
    {
        isSpeaking = false;
    }
    
    public void Interact(PlayerEntity p)
    {
        if (!isSpeaking)
        {
            isSpeaking = true;
            Dialogue dialogue = Prompt();
            dialogueBox.GetComponent<DialogueBox>().LoadLines(dialogue.lines, StopSpeaking, transform.position);
        }
    }
    
}
