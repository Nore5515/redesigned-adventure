using System;
using System.Collections;
using System.Collections.Generic;
using ElevatorScripts;
using EnemyStuf;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class EnemySpawnHandler : MonoBehaviour
{
    [SerializeField]
    private List<WaveSO> waves = new();

    [SerializeField] private float levelCompletionLoopTime = 0.5f;

    [SerializeField] private List<GameObject> spawnpoints = new();

    public List<GameObject> enemyInstances = new();

    private int currentWaveIndex = 0;
    private WaveSO currentWave;

    public float waveTimer = 0.0f;
    public TextMeshProUGUI timerText;

    public Action<WaveSO, int> newWav;
    
    [SerializeField] EnemyRadarHandler radarHandler;
    
    [SerializeField] private Elevator elevator; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elevator = GameObject.FindGameObjectWithTag("elevator").GetComponent<Elevator>();
        
        if (waves.Count == 0)
        {
            throw new Exception("Empty wave");
        }
        currentWave = waves[currentWaveIndex];
        SpawnWave(currentWave);
        waveTimer = currentWave.waveDelay;
    }
    
    public void NextWave()
    {
        currentWaveIndex++;
		if (currentWaveIndex < waves.Count){
    	    currentWave = waves[currentWaveIndex];
	        waveTimer = currentWave.waveDelay;
        	SpawnWave(currentWave);
		}
    }

    public void SpawnWave(WaveSO wave)
    {
        List<GameObject> enemyGOs = wave.GetEnemies();
        Random r = new Random();
        foreach (var enemy in enemyGOs)
        {
            Vector3 spawnPos = spawnpoints[r.Next(0, spawnpoints.Count)].transform.position;
            GameObject inst = Instantiate(enemy, spawnPos, Quaternion.identity);
            enemyInstances.Add(inst);
            radarHandler.AddEnemy(inst);
        }
        newWav.Invoke(wave, currentWaveIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (waveTimer > 0.0f)
        {
            waveTimer -= Time.deltaTime;
            timerText.text = waveTimer.ToString("0.00");
        }
        else
        {
            timerText.text = "0.00";
            if (currentWaveIndex < waves.Count)
            {
                NextWave();
            }
            else
            {
                // NO MORE WAVES!
                // wait for all enemies to be dead, then call elevator.
                StartCoroutine(WaitForEnemiesDead());
            }
        }
    }

    IEnumerator WaitForEnemiesDead()
    {
        yield return new WaitForSeconds(levelCompletionLoopTime);
        if (GameObject.FindGameObjectWithTag("enemy") == null)
        {
            // VICTORY!
            elevator.SummonElevator();
        }
        else
        {
            StartCoroutine(WaitForEnemiesDead());
        }
    }
}
