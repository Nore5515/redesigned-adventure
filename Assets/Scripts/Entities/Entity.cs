using UnityEngine;

namespace Entities
{
    public interface Entity
    {
        // TO THE ENTITY. GOD!!
        public void DealDamage(int damage, Entity source);
        public void DealKnockback(float knockback, Entity source);
        public int GetHP();

        public int GetXPReward();
        public int GetCashReward();
        // When this entity gets a kill, it  gets these as rewards
        public void KillReward(int xp, int cash);
        
        public GameObject GetGameObject();
    }
}