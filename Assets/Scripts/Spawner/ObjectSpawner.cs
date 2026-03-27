using System;
using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;
    
    [SerializeField, Tooltip("Spawn Count (0 means infinite!)")]
    private int spawnCount = 1;
    
    [SerializeField]
    private float spawnDelay = 1.0f;

    [SerializeField, Tooltip("How many spawn per batch (total objects spawned = spawnCount * batchCount)")]
    private int batchCount = 1;

    private bool infinite = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spawnCount == 0)
        {
            infinite = true;
        }

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (spawnCount > 0 || infinite)
        {
            yield return new WaitForSeconds(spawnDelay);
            for (int x = 0; x < batchCount; x++)
            {
                Instantiate(spawnedObject, transform.position, Quaternion.identity);
            }
            spawnCount--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
