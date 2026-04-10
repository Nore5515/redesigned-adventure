using System.Collections.Generic;
using Entities;
using UnityEngine;

public class WallObject : MonoBehaviour, Entity
{
    [SerializeField] private List<GameObject> wallParts;
    public int hp;
    [SerializeField] BoxCollider wallCollider;

    public void Init(int hp, Material material, bool passThrough)
    {
        this.hp = hp;
        foreach (var wallPart in wallParts)
        {
            wallPart.GetComponent<MeshRenderer>().material = material;
        }

        if (passThrough)
        {
            wallCollider.isTrigger = true;
        }
    }

    public void DealDamage(int damage, Entity source)
    {
        hp -= damage;
        if (hp <= 0)
        {
            source.AddXP(GetXPReward());
            Destroy(gameObject);
        }       
    }

    public void DealKnockback(float knockback, Entity source)
    {
        // It's a wall. Ain't going anywhere!
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetXPReward()
    {
        return 10;
    }

    public void AddXP(int xp)
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
