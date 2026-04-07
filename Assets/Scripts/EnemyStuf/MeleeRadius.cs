using System;
using UnityEngine;

public class MeleeRadius : MonoBehaviour
{
    private EnemyInstance enemyInstance;
    
    public void Start()
    {
        enemyInstance = transform.parent.GetComponent<EnemyInstance>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FirstPersonPlayer")
        {
            enemyInstance.FlingAwayFromPoint(other.transform.position, 30.0f);
            enemyInstance.StruckPlayer();
        }
    }
}
