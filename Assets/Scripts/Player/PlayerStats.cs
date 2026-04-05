namespace Player
{
    using UnityEngine;
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        
        public int hp = 8;
        public int maxHp = 8;
        public int hpRegen = 0;
        
        public int mp = 12;
        public int maxMp = 12;
        public int mpRegen = 1;

        public int xp = 0;
        public int maxXp = 100;
        public int level = 1;

        public float speed = 12.0f;

        public float speedMod = 1.0f;
        public float jumpMod = 1.0f;
    }
}