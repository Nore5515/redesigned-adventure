using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public Color color;
    public Vector3 localScale = new Vector3(1.0f, 1.0f, 1.0f);
    
    [Range(1.0f, 30.0f)] public float m_Speed;

    [Range(1, 100)] public int m_HP;
    
    [SerializeField] public GameObject explosionPrefab;
}
