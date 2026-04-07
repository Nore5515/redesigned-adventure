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
        public void AddXP(int xp);
        
        public GameObject GetGameObject();
    }
}