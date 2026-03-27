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
    
    [SerializeField] private LevelUpHandler levelUpHandler;

    [SerializeField] List<AbilityPurchaseButton> abilityButtons = new();
    
    PlayerStats playerStats;
    
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        UpdateStats();
    }

    public void SetStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
        UpdateStats();
    }

    public void AddAbilityToStore(AbilitySO ability, int slot)
    {
        UnityAction onClick = AbilityLockOnClick;
        abilityButtons[slot].Init(ability.name, ability.description, ability.icon, onClick);
    }

    void AbilityLockOnClick()
    {
        Debug.Log("Locked!");
    }
    
    void UpdateStats()
    {
        hpText.text = playerStats.hp + "/" + playerStats.maxHp;
        speedText.text = playerStats.speedMod.ToString();
        jumpText.text = playerStats.jumpMod.ToString();
    }
    
    public void Unpause()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
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
