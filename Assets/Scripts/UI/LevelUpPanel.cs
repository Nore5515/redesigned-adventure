using System;
using System.Collections.Generic;
using System.Net.Mime;
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

    private Button currentlyLockedButton = null;

    private int assignedAbilities = 0;
    private AbilitySO assignedAbility = null;
    
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        UpdateStats();
        assignedAbilities = 0;
        assignedAbility = null;
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
        hpText.text = playerStats.hp + "/" + playerStats.maxHp;
        speedText.text = playerStats.speedMod.ToString();
        jumpText.text = playerStats.jumpMod.ToString();
    }
    
    public void Unpause()
    {
        if (assignedAbility == null && noAbilityConfirmText.gameObject.activeSelf == false)
        {
            confirmButtonText.text = "Confirm";
            noAbilityConfirmText.gameObject.SetActive(true);
            return;
        }
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        AssignAbility();
        assignedAbility = null;
        UpdateLockedButton();
        gameObject.SetActive(false);
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
