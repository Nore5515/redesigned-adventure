using Player;
using UnityEngine;

public class LevelUpHandler : MonoBehaviour
{
    
    [SerializeField] private GameObject levelUpMenuGO;
    
    [SerializeField] private LevelUpPanel levelUpPanel;
    [SerializeField] private PlayerHandler playerHandler;
    [SerializeField] private AbilityHandler abilityHandler;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelUpMenuGO.SetActive(false);
    }
    
    public void ShowLevelUpMenu(PlayerStats playerStats)
    {
        levelUpMenuGO.SetActive(true);
        levelUpPanel.SetStats(playerStats);
        
        levelUpPanel.AddAbilityToStore(abilityHandler.ProvideRandomAbility(), 0);
        levelUpPanel.AddAbilityToStore(abilityHandler.ProvideRandomAbility(), 1);
        levelUpPanel.AddAbilityToStore(abilityHandler.ProvideRandomAbility(), 2);
        
        levelUpPanel.Pause();
    }
    
    public void RaiseHP()
    {
        playerHandler.LevelHP();
    }

    public void RaiseSpeed()
    {
        playerHandler.LevelSpeed();
    }

    public void RaiseJump()
    {
        playerHandler.LevelJump();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
