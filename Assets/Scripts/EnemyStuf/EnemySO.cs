using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public Color color;
    public Vector3 localScale = new Vector3(1.0f, 1.0f, 1.0f);
    
    [Range(0.0f, 30.0f)] public float m_Speed;
    [Range(0.0f, 30.0f)] public float m_Acceleration;

    [SerializeField] public float m_playerKnockback;
    
    [SerializeField] public int m_HP;
    [SerializeField] public int m_Dmg;
    
    [SerializeField] public GameObject explosionPrefab;

    [SerializeField] public bool weeping = false;

    [SerializeField] public int xpReward = 30;
}
