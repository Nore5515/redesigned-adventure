using UnityEngine;

public class StaticCharacter : MonoBehaviour
{
    public Dialogue dialogue;

    [SerializeField] private Material spokenWithMaterial;
    [SerializeField] private bool changeColorAfterSpeaking;

    public Dialogue Prompt()
    {
        if (changeColorAfterSpeaking)
        {
            gameObject.GetComponent<Renderer>().material = spokenWithMaterial;
        }
        return dialogue;
    }
}
