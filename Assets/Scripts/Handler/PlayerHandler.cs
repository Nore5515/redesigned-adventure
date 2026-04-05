using System;
using System.Collections;
using Entities;
using Player;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    CanvasHandler canvasHandler;

    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public PlayerStats defaultPlayerStats;
    
    [SerializeField] LevelUpHandler levelUpHandler;
    
    public Entity playerEntity;
    
    public void Start()
    {
        AddXp(0);
        ResetStats();
        StartCoroutine(RegenLoop());
        playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
    }

    IEnumerator RegenLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            playerStats.mp = Mathf.Min(playerStats.mp + playerStats.mpRegen, playerStats.maxMp);
            playerStats.hp = Mathf.Min(playerStats.hp + playerStats.hpRegen, playerStats.maxHp);
            UpdateCanvas();
        }
    }

    public void ResetStats()
    {
        playerStats = Instantiate(defaultPlayerStats);
    }

    public void AddXp(int amount)
    {
        playerStats.xp += amount;
        if (playerStats.xp >= playerStats.maxXp)
        {
            playerStats.level++;
            playerStats.xp -= playerStats.maxXp;
            playerStats.maxXp += 25; 
            levelUpHandler.ShowLevelUpMenu(playerStats);
        }
        UpdateCanvas();
    }

    public void UpdateCanvas()
    {
        canvasHandler.UpdateAll(playerStats);
    }

    public void LevelHP()
    {
        playerStats.maxHp += 2;
        playerStats.hp += 2;
        UpdateCanvas();
    }

    public void PayAbilityCost(AbilitySO ability)
    {
        playerStats.hp -= ability.hpCost;
        playerStats.mp -= ability.manaCost;
    }

    public void DealDamage(int amount)
    {
        playerStats.hp -= amount;
        UpdateCanvas();
    }

    public void LevelSpeed()
    {
        playerStats.speedMod += 0.2f;
        UpdateCanvas();
    }

    public void LevelJump()
    {
        playerStats.jumpMod += 0.2f;
        UpdateCanvas();
    }
}
