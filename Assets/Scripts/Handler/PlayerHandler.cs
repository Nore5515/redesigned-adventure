using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using Entities;
using EquipmentNamespace;
using Handler;
using Passives;
using Player;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    CanvasHandler canvasHandler;

    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public PlayerStats defaultPlayerStats;

    [SerializeField] private GameObject gameOverMenuObj;
   
    [SerializeField] InventoryHandler invHandler;
    [SerializeField] LevelUpHandler levelUpHandler;

    public GameObject playerGO;
    public Entity playerEntity;
    
    // Use this to see IF we have a trigger
    public HashSet<TriggerTypes> activeTriggers = new();
    // If we do have a trigger,  search this list for all instances of said trigger and activate the associated passive.
    public List<Passive> activePassives = new();
    
    public void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerEntity = playerGO.GetComponent<Entity>();
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
        activeTriggers = new HashSet<TriggerTypes>();
        activePassives = new List<Passive>();
        foreach (var equipment in playerStats.equippedItems)
        {
            if (equipment.Value == null) continue;
            activePassives.Add(equipment.Value.passive);
            AddTrigger(equipment.Value.passive);
        }
    }

    private void AddTrigger(Passive p)
    {
        activeTriggers.Add(p.trigger.GetTriggerType());
        if (p.trigger.GetTriggerType() == TriggerTypes.AlwaysOn)
        {
            // If trigger is always on, apply stat boosts now
            foreach (var effect in p.effects)
            {
                effect.Apply(playerEntity, null);
            }
        }
    }

    // This should be called whenever a trigger type could potentially be called.
    public bool HasTrigger(TriggerTypes trigger)
    {
        return activeTriggers.Contains(trigger);
    }

    public void TriggerAllOfType(TriggerTypes trigger, Entity caster, Entity[] targets)
    {
        foreach (var equipment in playerStats.equippedItems)
        {
            if (equipment.Value == null) continue;
            if (equipment.Value.passive.trigger.type == trigger)
            {
                foreach (var effect in equipment.Value.passive.effects)
                {
                    effect.Apply(caster, targets);   
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
        playerStats.statMods.spellDamageBonus += Mathf.RoundToInt(e.spellDamageBoost);
     
        playerStats.statMods.speedMultiplier += e.speedBoost;
        playerStats.statMods.jumpMultiplier += e.jumpHeightBoost;
    }

    public void SetSpellDamage(float amount)
    {
        playerStats.statMods.spellDamageBonus = Mathf.RoundToInt(amount);
    }
    
    private void Update()
    {
        if (playerEntity.hp <= 0)
        {
            gameOverMenuObj.SetActive(true);
            Pause();
        }
    }
    
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    IEnumerator RegenLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            playerStats.mp = Mathf.Min(playerStats.mp + playerStats.mpRegen, playerStats.maxMp);
            playerEntity.hp = Mathf.Min(playerEntity.hp + playerStats.hpRegen, playerEntity.maxHP);
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
        canvasHandler.UpdateAll(this, playerStats);
    }

    public void LevelHP()
    {
        playerEntity.maxHP += 2;
        playerEntity.hp += 2;
        UpdateCanvas();
    }

    public void PayAbilityCost(AbilitySO ability)
    {
        playerEntity.hp -= ability.hpCost;
        playerStats.mp -= ability.manaCost;
    }

    public void DealDamage(int amount)
    {
        playerEntity.hp -= amount;
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
