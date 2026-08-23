using UnityEngine;

public class TitleHead : MonoBehaviour
{
    private RectTransform rect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();   
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, 5f);
    }
}
