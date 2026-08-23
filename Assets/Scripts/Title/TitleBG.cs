using UnityEngine;
using UnityEngine.UI;

public class TitleBG : MonoBehaviour
{
    // Exposes a gradient picker in the Unity Inspector
    public Gradient myGradient; 
    private Image myImage;

    void Start()
    {
        myImage = GetComponent<Image>();
    }

    void Update()
    {
        // Cycles the color over time from 0.0 to 1.0
        float t = Mathf.PingPong(Time.time, 1f);
        
        // Evaluate extracts the exact color at position 't'
        myImage.color = myGradient.Evaluate(t); 
    }
}
