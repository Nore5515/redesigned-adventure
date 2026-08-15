using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    
    public static ObjectPool SharedInstance;
    public List<GameObject> objectPool;

    public GameObject objectToPool;

    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            objectPool.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
