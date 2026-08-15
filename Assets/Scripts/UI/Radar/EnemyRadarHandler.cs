using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRadarHandler : MonoBehaviour
{
    private List<GameObject> activeEnemies = new();
    private GameObject player;

    public float radarRange = 20.0f;
    public float radarPing = 1.0f;

    [SerializeField]
    RadarUI radarUI;

    private GameObject radarGO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(RadarPing());
        radarGO = radarUI.gameObject;
    }

    private void Update()
    {
        // radarGO.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, player.transform.rotation.y, 1);
    }

    IEnumerator RadarPing()
    {
        while (SceneManager.loadedSceneCount > 0)
        {
            yield return new WaitForSeconds(radarPing);
            List<GameObject> inRange = UpdateRadar();
            List<Vector3> directions = new();
            List<float> distancesRatios = new();
            
            foreach (GameObject obj in inRange)
            {
                Vector3 dir = player.transform.position - obj.transform.position;
                dir = dir.normalized * -1.0f;
                directions.Add(dir);
                // Debug.Log("Direction to Enemy " + obj.name + ": " + dir);
                distancesRatios.Add(Vector3.Distance(player.transform.position, obj.transform.position) / radarRange);
            }
            
            // Debug.Log(inRange.Count);
            radarUI.UpdatePings(inRange, directions, distancesRatios, player.transform.rotation);
        }
    }
    
    public void AddEnemy(GameObject go)
    {
        activeEnemies.Add(go);
    }

    public List<GameObject> UpdateRadar()
    {
        List<GameObject> enemiesInRange = new();
        activeEnemies.RemoveAll(x => x == null);
        foreach (GameObject go in activeEnemies)
        {
            if (Vector3.Distance(go.transform.position, player.transform.position) <= radarRange)
            {
                // Enemy is being tracked
                enemiesInRange.Add(go);
            }
        }

        return enemiesInRange;
    }
    
    
}
