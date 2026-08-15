using Abilities;
using Entities;
using Player;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scene", menuName = "Scene")]
public class SceneSO : ScriptableObject
{
    public Scene scene;
    public string sceneName;
    public string description;
}