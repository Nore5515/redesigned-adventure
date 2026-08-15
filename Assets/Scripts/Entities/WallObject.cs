using System.Collections;
using System.Collections.Generic;
using Entities;
using Unity.VisualScripting;
using UnityEngine;

public class WallObject : MonoBehaviour, Entity
{
    [SerializeField] private List<GameObject> wallParts;
    public int hp { get; set; }
    public int maxHP { get; set; }

    [SerializeField] BoxCollider wallCollider;
    public float lifetime;

    public void Init(int hp, Material material, bool passThrough, float lifetime)
    {
        this.hp = hp;
        maxHP = hp;
        this.lifetime = lifetime;
        StartCoroutine(DestroyAfterTime());
        foreach (var wallPart in wallParts)
        {
            wallPart.GetComponent<MeshRenderer>().material = material;
        }

        if (passThrough)
        {
            wallCollider.isTrigger = true;
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);   
    }

    public void TakeDamageFromSource(int damage, Entity source)
    {
        hp -= damage;
        if (hp <= 0)
        {
            source.KillReward(GetXPReward(), GetCashReward(), GetScoreReward());
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

    public int GetCashReward()
    {
        return 10;
    }

    public int GetScoreReward()
    {
        return 10;
    }

    public void KillReward(int xp, int cash, int score)
    {
        throw new System.NotImplementedException();
    }
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
