using System.Collections.Generic;
using UnityEngine;

namespace EnemyStuf
{
    [CreateAssetMenu(fileName = "WaveSO", menuName = "Scriptable Objects/Wave")]
    public class WaveSO: ScriptableObject
    {
        public GameObject commonEnemyPrefab;
        public GameObject tankEnemyPrefab;
        //public GameObject speedsterEnemyPrefab;
        
        public int commonEnemyCount = 0;
        public int tankEnemyCount = 0;

        public float waveDelay = 15.0f;

        public List<GameObject> GetEnemies()
        {
            List<GameObject> enemies = new List<GameObject>();
            for (int x = 0; x < commonEnemyCount; x++)
            {
                enemies.Add(commonEnemyPrefab);
            }

            for (int y = 0; y < tankEnemyCount; y++)
            {
                enemies.Add(tankEnemyPrefab);
            }

            return enemies;
        }
    }
}