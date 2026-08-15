using System;
using Entities;
using TMPro;
using UnityEngine;

public class SpinningSawblade : MonoBehaviour, Entity
{
    [SerializeField] private float rotationSpeed = 1080.0f;

    // [SerializeField] private TextMeshProUGUI levelText;
    
    private int xp, cash, score = 0;
    private int level = 1;

    public int hp { get; set; } = 1;
    public int maxHP { get; set; } = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // levelText.text = "Level " + level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Entity>() != null)
        {
            Entity e = other.gameObject.GetComponent<Entity>();
            e.TakeDamageFromSource(1 + Mathf.FloorToInt(level / 2), this);
        }
    }

    public void TakeDamageFromSource(int damage, Entity source)
    {
        throw new NotImplementedException();
    }

    public void DealKnockback(float knockback, Entity source)
    {
        throw new NotImplementedException();
    }

    public int GetHP()
    {
        return hp;
    }
    
    public int GetXPReward()
    {
        throw new NotImplementedException();
    }

    public int GetCashReward()
    {
        throw new NotImplementedException();
    }

    public int GetScoreReward()
    {
        throw new NotImplementedException();
    }

    // LOL
    public void KillReward(int xp, int cash, int score)
    {
        this.xp += xp;
        this.cash += cash;
        this.score += score;
        if (this.xp >= level * 100)
        {
            this.xp -= level * 100;
            level++;
        }

        // levelText.text = "Level " + level.ToString();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
