using UnityEngine;

namespace Entities
{
    public interface Entity
    {
        // TO THE ENTITY. GOD!!
        public void DealDamage(int damage);
        public int GetHP();

        public int GetXPReward();
        
        public GameObject GetGameObject();
    }
}