using UnityEngine;

namespace Entities
{
    public interface Entity
    {
        // TO THE ENTITY. GOD!!
        public void ReceieveDamage(int damage, Entity source);
        public void DealKnockback(float knockback, Entity source);
        public int GetHP();

        public int GetXPReward();
        public int GetCashReward();

        public int GetScoreReward();
        
        // When this entity gets a kill, it calls this to get its rewards
        public void KillReward(int xp, int cash, int score);
        
        public GameObject GetGameObject();
    }
}