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

        // TODO: Change this 3 to "number of options available"
        for (int x = 0; x < 3; x++)
        {
            levelUpPanel.AddAbilityToStore(abilityHandler.ProvideRandomAbility());
        }
        
        levelUpPanel.Pause();
    }

    public void AssignAbility(AbilitySO ability)
    {
        Debug.Log("beep beep! just got ability " + ability.name + "!");
        abilityHandler.AssignAbility(ability);
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
    
}
