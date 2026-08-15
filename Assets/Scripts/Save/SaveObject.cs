using System;
using System.Collections.Generic;
using EquipmentNamespace;
using Save;
using UnityEngine;

public class SaveObject : MonoBehaviour
{
    private List<Equipment> savedEquipment = new();
    private SaveModel saveModel = new();
    
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("save");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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

}
