using System.Collections.Generic;
using UnityEngine;

public class RadarUI : MonoBehaviour
{
    [SerializeField] private GameObject radarPingPrefab;
    [SerializeField] private GameObject radarParent;

    private List<GameObject> radarPings = new();
    
    // For the rect transforms, 90 distance from origin (0,0) is the max it can be.
    private float maxDistForPings = 90.0f;
    
    public void UpdatePings(List<GameObject> enemies, List<Vector3> directions, List<float> distanceRatios, Quaternion rotation)
    {
        radarPings.ForEach(x => Destroy(x));
        radarPings = new List<GameObject>();
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject ping = Instantiate(radarPingPrefab, transform.position, transform.rotation);
            ping.transform.SetParent(transform);
            Vector2 pingPos = new Vector2((directions[i].z * distanceRatios[i]) * maxDistForPings, directions[i].x * distanceRatios[i] * maxDistForPings);
            ping.GetComponent<RectTransform>().anchoredPosition = pingPos;
            radarPings.Add(ping);
        } 
    }
}
