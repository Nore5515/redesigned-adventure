using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeepingAngelLooking : MonoBehaviour
{

    [SerializeField] private float xRays = 10;
    [SerializeField] private float yRays = 3;
    [SerializeField] private float xAngleBetween = 15.0f;
    [SerializeField] private float yAngleBetween = 15.0f;

    [SerializeField] private GameObject tempEye;

    private Dictionary<GameObject, bool> observationStatus = new();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Fucking AI Magic
        // Research later to figure out how it works...

        // Calculate the starting angles to center the grid
        float startXAngle = -(xRays - 1) * xAngleBetween * 0.5f;
        float startYAngle = -(yRays - 1) * yAngleBetween * 0.5f;

        bool isWatched = false;
        
        for (int x = 0; x < observationStatus.Count; x++)
        {
            KeyValuePair<GameObject, bool> kvp = observationStatus.ElementAt(x);
            if (kvp.Key is null)
            {
                observationStatus.Remove(kvp.Key);
                continue;
            }
            
            observationStatus[kvp.Key] = false;
        }
        
        for (int y = 0; y < yRays; y++)
        {
            for (int x = 0; x < xRays; x++)
            {
                // Calculate the horizontal and vertical angles for this ray
                float horizontalAngle = startXAngle + (x * xAngleBetween);
                float verticalAngle = startYAngle + (y * yAngleBetween);
                
                // Create rotation based on the angles
                Quaternion rotation = transform.rotation * 
                                      Quaternion.Euler(verticalAngle, horizontalAngle, 0);
                
                // Calculate the ray direction
                Vector3 rayDirection = rotation * Vector3.forward;
                
                Debug.DrawRay(transform.position, rayDirection * 20.0f, Color.red);

                RaycastHit hit;
                if (Physics.Raycast(transform.position, rayDirection, out hit, 100.0f))
                {
                    if (hit.collider.gameObject.tag == "weeping")
                    {
                        Debug.Log("Weeping Angel Spotted!");
                        isWatched = true;
                        // hit.collider.gameObject.GetComponent<EnemyInstance>().isWatched = true;
                        observationStatus.TryAdd(hit.collider.gameObject, true);
                        observationStatus[hit.collider.gameObject] = true;
                    }
                }
            }
        }

        foreach (KeyValuePair<GameObject, bool> kvp in observationStatus)
        {
            kvp.Key.GetComponent<EnemyInstance>().isWatched = kvp.Value;
        }

        if (isWatched)
        {
            tempEye.SetActive(true);
        }
        else
        {
            tempEye.SetActive(false);
        }
    }
}
