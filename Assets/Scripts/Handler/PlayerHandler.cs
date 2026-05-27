using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using Entities;
using Handler;
using Passives;
using Player;
using Unity.Mathematics;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    CanvasHandler canvasHandler;

    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public PlayerStats defaultPlayerStats;
    
    
    // private PlayerStats
    
    [SerializeField] InventoryHandler invHandler;
    [SerializeField] LevelUpHandler levelUpHandler;
    
    public Entity playerEntity;
    
    public HashSet<TriggerTypes> activeTriggers = new();
    
    public void Start()
    {
        AddXp(0);
        ResetStats();
        StartCoroutine(RegenLoop());
        playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        
        // Assign delegate in playerStats to update activeTriggers
        Debug.Log("Assigning delegate");
        playerStats.EquipmentChangeEvent += UpdateTriggersFromEquipment;
    }

    public void UpdateTriggersFromEquipment()
    {
        // Debug.Log("New WELRLQWEKRLWEKL");
        activeTriggers = new HashSet<TriggerTypes>();
        foreach (var equipment in playerStats.equippedItems)
        {
            if (equipment.Value == null) continue;
            foreach (var passive in equipment.Value.passives)
            {
                foreach (var trigger in passive.triggers)
                {
                    activeTriggers.Add(trigger.GetTriggerType());
                    if (trigger.GetTriggerType() == TriggerTypes.AlwaysOn)
                    {
                        // If trigger is always on, apply stat boosts now
                        foreach (var effect in passive.effects)
                        {
                            Debug.Log("Applying!");
                            effect.Apply(playerEntity, null);
                        }
                    }
                }
            }
        }
    }

    public void ApplyStatBoosts(StatBoostEffect e)
    {
        Debug.Log("Applying stat boosts!");
        // TODO: Change HP and MP regen to floats...
        playerStats.statMods.hpRegenBonus += Mathf.RoundToInt(e.hpRegenBoost);
        playerStats.statMods.mpRegenBonus += Mathf.RoundToInt(e.manaRegenBoost);
        playerStats.statMods.meleeDamageBonus += Mathf.RoundToInt(e.meleeDamageBoost);
        playerStats.statMods.meleePoisonBonus += Mathf.RoundToInt(e.meleePoisonDamage);
     
        playerStats.statMods.speedMultiplier += e.speedBoost;
        playerStats.statMods.jumpMultiplier += e.jumpHeightBoost;
    }

    private void Update()
    {
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
        Debug.Log("Stats Reset!");
        playerStats = Instantiate(defaultPlayerStats);
        invHandler.AssignNewStats(playerStats);
        playerStats.EquipmentChangeEvent += UpdateTriggersFromEquipment;
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
        playerStats.statMods.speedMultiplier += 0.2f;
        UpdateCanvas();
    }

    public void LevelJump()
    {
        playerStats.statMods.jumpMultiplier += 0.2f;
        UpdateCanvas();
    }
}
