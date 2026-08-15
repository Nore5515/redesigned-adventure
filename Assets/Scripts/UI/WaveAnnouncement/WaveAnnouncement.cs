using System.Collections;
using System.Text;
using EnemyStuf;
using TMPro;
using UnityEngine;

public class WaveAnnouncement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] private GameObject panel;
    private EnemySpawnHandler spawnHandler;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("enemy_spawn_handler") == null)
        {
            Debug.LogError("No Enemy Spawn Handler found!");
        }
        spawnHandler = GameObject.FindGameObjectWithTag("enemy_spawn_handler").GetComponent<EnemySpawnHandler>();
        spawnHandler.newWav += SetWave;
    }

    public void SetWave(WaveSO wave, int waveNum)
    {
        panel.SetActive(true);
        StartCoroutine(TimedPanelHide());
        string content = "Wave " + (waveNum + 1);
        if (wave.commonEnemyCount > 0)
        {
            content += "  Commons " + wave.commonEnemyCount + "x";
        }
        if (wave.tankEnemyCount > 0)
        {
            content += "  Bigs " + wave.tankEnemyCount + "x";
        }
        waveText.text = content;
    }

    IEnumerator TimedPanelHide()
    {
        yield return new WaitForSeconds(3.0f);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
