using System;
using System.Collections.Generic;
using System.Net.Mime;
using Abilities;
using Entities;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI jumpText;
    [SerializeField] private TextMeshProUGUI noAbilityConfirmText;
    [SerializeField] private TextMeshProUGUI confirmButtonText;
    
    [SerializeField] private LevelUpHandler levelUpHandler;

    [SerializeField] List<AbilityPurchaseButton> abilityButtons = new();
    
    
    PlayerStats playerStats;
    private Entity playerEntity;

    private Button currentlyLockedButton = null;

    private int assignedAbilities = 0;
    private AbilitySO assignedAbility = null;

    private bool assignedHP = false;
    [SerializeField] private Button raiseHPButton;

    private void Start()
    {
        playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        UpdateStats();
        assignedAbilities = 0;
        assignedAbility = null;
        assignedHP = false;
        UpdateLockedButton();
        noAbilityConfirmText.gameObject.SetActive(false);
        confirmButtonText.text = "Continue";
    }

    public void SetStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
        UpdateStats();
    }

    public void AddAbilityToStore(AbilitySO ability)
    {
        int slot = assignedAbilities;
        UnityAction onClick = () => AbilityLockOnClick(ability, abilityButtons[slot].button);
        abilityButtons[slot].Init(ability.name, ability.description, ability.icon, onClick);
        assignedAbilities++;
    }
    
    // TODO: Move assignment out of this and into the on click for the "all done" button.
    // Have it assign only the locked ability.
    // ...also have a popup confirming if you don't assign any ability.
    void AbilityLockOnClick(AbilitySO ability, Button button)
    {
        currentlyLockedButton = button;
        assignedAbility = ability;
        assignedHP = false;
        UpdateLockedButton();
    }

    void AssignAbility()
    {
        if (assignedAbility != null)
        {
            levelUpHandler.AssignAbility(assignedAbility);
        }
    }

    void UpdateLockedButton()
    {
        if (assignedHP == false)
        {
            raiseHPButton.interactable = true;
        }
        else
        {
            raiseHPButton.interactable = false;
        }
        foreach (AbilityPurchaseButton button in abilityButtons)
        {
            button.button.interactable = true;
        }
        if (currentlyLockedButton != null)
        {
            currentlyLockedButton.interactable = false;
        }
    }
    
    void UpdateStats()
    {
        hpText.text = playerEntity.hp + "/" + playerEntity.maxHP;
        speedText.text = playerStats.statMods.speedMultiplier.ToString();
        jumpText.text = playerStats.statMods.jumpMultiplier.ToString();
    }
    
    public void Unpause()
    {
        if (assignedAbility == null && noAbilityConfirmText.gameObject.activeSelf == false && assignedHP == false)
        {
            confirmButtonText.text = "Confirm";
            noAbilityConfirmText.gameObject.SetActive(true);
            return;
        }
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        if (assignedHP)
        {
            playerEntity.maxHP += 2;
            playerEntity.hp += 2;
            playerStats.hpRegen = Mathf.FloorToInt(playerEntity.maxHP * 0.1f);
        }
        else
        {
            AssignAbility();
        }
        assignedAbility = null;
        UpdateLockedButton();
        gameObject.SetActive(false);
    }

    public void ClickRaiseMaxHP()
    {
        assignedHP = true;
        assignedAbility = null;
        UpdateLockedButton();
    }

    public void RaiseHPOnClick()
    {
        levelUpHandler.RaiseHP();
        UpdateStats();
    }

    public void RaiseSpeedOnClick()
    {
        levelUpHandler.RaiseSpeed();
        UpdateStats();
    }

    public void RaiseJumpOnClick()
    {
        levelUpHandler.RaiseJump();
        UpdateStats();
    }
}
