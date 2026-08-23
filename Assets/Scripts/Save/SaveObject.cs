using System;
using System.Collections.Generic;
using System.IO;
using EquipmentNamespace;
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
}

public class SaveObject : MonoBehaviour
{
    private List<Equipment> savedEquipment = new();
    private SaveModel saveModel = new();
    public int dreamCoin = 0;

    private List<ArenaProgress> arenas = new List<ArenaProgress>
    {
        new ArenaProgress { ArenaName = "Morning", FurthestWave = 0, IsCompleted = false, BestTime = float.MaxValue },
        new ArenaProgress { ArenaName = "Midday", FurthestWave = 0, IsCompleted = false, BestTime = float.MaxValue },
        new ArenaProgress { ArenaName = "Dusk", FurthestWave = 0, IsCompleted = false, BestTime = float.MaxValue }
    };

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

    public void SaveProgress(SaveModel _saveModel)
    {
        saveModel = _saveModel;
    }
    
    public SaveModel GetSaveProgress()
    {
        return saveModel;
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

    public void SaveToFile()
    {
        SaveData data = new SaveData
        {
            DreamCoin = dreamCoin,
            Arenas = arenas,
            SavedEquipment = savedEquipment
        };

        string json = JsonUtility.ToJson(data, true);
        try
        {
            File.WriteAllText(saveFilePath, json);
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
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                dreamCoin = data.DreamCoin;
                arenas = data.Arenas;
                savedEquipment = data.SavedEquipment;

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
        }
    }
}