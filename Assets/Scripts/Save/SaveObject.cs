using System;
using System.Collections.Generic;
using System.IO;
using EquipmentNamespace;
using JetBrains.Annotations;
using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class ArenaProgress
{
    public string ArenaName;
    public int FurthestWave;
    public bool IsCompleted;
    public float BestTime;
}

[Serializable]
public class SaveData
{
    public int DreamCoin;
    public List<ArenaProgress> Arenas = new();
    public List<Equipment> SavedEquipment = new();
    
    // Dreamperks
    public bool conceptualized = false;
    public int dreaming = 0;
    public int lucid = 0;
    public int ego = 0;
}

public class SaveObject : MonoBehaviour
{
    private List<Equipment> savedEquipment = new();
    public int dreamCoin = 0;
    [CanBeNull] private SaveData data;

    private List<ArenaProgress> arenas = new List<ArenaProgress>
    {
        new ArenaProgress { ArenaName = "Morning", FurthestWave = 0, IsCompleted = false, BestTime = float.MaxValue },
        new ArenaProgress { ArenaName = "Midday", FurthestWave = 0, IsCompleted = false, BestTime = float.MaxValue },
        new ArenaProgress { ArenaName = "Dusk", FurthestWave = 0, IsCompleted = false, BestTime = float.MaxValue }
    };

    public SaveData GetSaveData()
    {
        if (data == null)
        {
            LoadFromFile();
        }

        return data;
    }

    public void SaveData([CanBeNull] SaveData newData)
    {
        SaveToFile(newData);
    }

    private string saveFilePath;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("save");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // Set up the save file path
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveEquip(List<Equipment> _savedEquipment)
    {
        savedEquipment = _savedEquipment;
    }

    public List<Equipment> GetSavedEquip()
    {
        return savedEquipment;
    }

    public void UpdateArenaProgress(string arenaName, int furthestWave, float bestTime, bool isCompleted)
    {
        foreach (var arena in arenas)
        {
            if (arena.ArenaName == arenaName)
            {
                arena.FurthestWave = Mathf.Max(arena.FurthestWave, furthestWave);
                arena.IsCompleted = isCompleted;
                arena.BestTime = Mathf.Min(arena.BestTime, bestTime);
                break;
            }
        }
    }

    public ArenaProgress GetArenaProgress(string arenaName)
    {
        return arenas.Find(x => x.ArenaName == arenaName);
    }

    public List<ArenaProgress> GetAllArenaProgress()
    {
        return arenas;
    }

    // Offline Save/Load Methods

    public void SaveToFile([CanBeNull] SaveData newData)
    {
        if (newData == null)
        {
            SaveData data = new SaveData
            {
                DreamCoin = dreamCoin,
                Arenas = arenas,
                SavedEquipment = savedEquipment,
                conceptualized = false,
                dreaming = 0,
                lucid = 0,
                ego = 0
            };
            newData = data;
        }

        string json = JsonUtility.ToJson(newData, true);
        try
        {
            File.WriteAllText(saveFilePath, json);
            data = newData;
            Debug.Log($"Save successful: {saveFilePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save data to file: {e.Message}");
        }
    }

    public void LoadFromFile()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);
                data = JsonUtility.FromJson<SaveData>(json);
                Debug.Log("Load successful!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data from file: {e.Message}");
            }
        }
        else
        {
            Debug.Log("No save file found; starting with default values.");
            SaveData newData = new SaveData
            {
                DreamCoin = 10,
                Arenas = arenas,
                SavedEquipment = savedEquipment,
                conceptualized = false,
                dreaming = 0,
                lucid = 0,
                ego = 0
            };
            data = newData;
        }
    }
}