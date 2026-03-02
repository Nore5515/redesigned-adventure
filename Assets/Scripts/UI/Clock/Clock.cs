using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private Image minuteHand;
    [SerializeField] private Image hourHand;
    [SerializeField] private int minutesInDay = 12;

    private float timeElapsed = 0.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        timeElapsed += Time.deltaTime;
        if (timeElapsed <= 60.0f * minutesInDay)
        {
            minuteHand.transform.eulerAngles = new Vector3(0.0f, 0.0f, (-360.0f * timeElapsed) / (60.0f * minutesInDay));
            hourHand.transform.eulerAngles = new Vector3(0.0f, 0.0f, (-40.0f * timeElapsed) / (60.0f * minutesInDay));
        }
    }
    
    
}
