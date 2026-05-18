using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{

    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] private Slider mpSlider;
    [SerializeField] private TextMeshProUGUI mpText;
    
    [SerializeField] private TextMeshProUGUI levelText;
    
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TextMeshProUGUI xpText;

    [SerializeField] private TextMeshProUGUI cashText;

    
    public void UpdateAll(PlayerStats playerStats)
    {
        UpdateHP(playerStats.hp, playerStats.maxHp);
        UpdateMP(playerStats.mp, playerStats.maxMp);
        UpdateXP(playerStats.xp, playerStats.maxXp);
        UpdateCash(playerStats.cash);
        UpdateLevel(playerStats.level);
    }
    
    public void UpdateHP(int hp, int maxHP)
    {
        hpSlider.maxValue = maxHP;
        hpSlider.value = hp;
        hpText.text = hp.ToString() + "/" + maxHP.ToString();
    }

    public void UpdateCash(int cash)
    {
        cashText.text = "$" + cash.ToString();
    }
    
    public void UpdateMP(int mp, int maxMP)
    {
        mpSlider.maxValue = maxMP;
        mpSlider.value = mp;
        mpText.text = mp.ToString() + "/" + maxMP.ToString();
    }

    public void UpdateXP(int xp, int maxXP)
    {
        xpSlider.maxValue = maxXP;
        xpSlider.value = xp;
        xpText.text = xp.ToString() + "/" + maxXP.ToString();   
    }

    public void UpdateLevel(int level)
    {
        levelText.text = "Level " + level.ToString();
    }
}
