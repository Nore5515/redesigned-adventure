using Abilities;
using Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DreamShopPanel : MonoBehaviour
{

    [SerializeField] private GameObject conceptualizeButton;
    
    [SerializeField] private GameObject dreamButton;
    [SerializeField] private TextMeshProUGUI dreamLabel;
    [SerializeField] private GameObject lucidButton;
    [SerializeField] private TextMeshProUGUI lucidLabel;
    [SerializeField] private GameObject egoButton;
    [SerializeField] private TextMeshProUGUI egoLabel;

    [SerializeField] private TextMeshProUGUI perkCost;
    [SerializeField] private TextMeshProUGUI perkTitle;
    [SerializeField] private TextMeshProUGUI perkDesc;

    private int currentPerkPrice = 0;
    string currentPerk = "";
    
    private SaveObject saveObj;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveObj = GameObject.FindGameObjectWithTag("save").GetComponent<SaveObject>();
        UpdateFromData();
    }

    public void BuyPerk()
    {
        if (saveObj.GetSaveData().DreamCoin >= currentPerkPrice)
        {
            saveObj.GetSaveData().DreamCoin -= currentPerkPrice;
            UpdateFromData();
        }
        else
        {
            return;
        }
        if (currentPerk == "conceptualization")
        {
            ActivateConceptualize();
        }
        else if (currentPerk == "dreaming")
        {
            IncreaseDreaming();
        }
        else if (currentPerk == "lucid")
        {
            IncreaseLucid();
        }
        else if (currentPerk == "ego")
        {
            IncreaseEgo();
        }
        UpdateFromData();
        UpdatePerkPrice(currentPerk);
    }

    public void SetPerk(string perk)
    {
        currentPerk = perk;
        if (currentPerk == "conceptualization")
        {
            perkTitle.text = "Conceptualize";
            perkDesc.text = "Your qualia defined, and this dream ripples in response. You can now carry equipment between runs.";
        }
        else if (currentPerk == "dreaming")
        {
            perkTitle.text = "Dreaming";
            perkDesc.text = "Reality is frayed, and you pull on the strings. You immediately start with a free level up.";
        }
        else if (currentPerk == "lucid")
        {
            perkTitle.text = "Lucid";
            perkDesc.text = "It's visions, ephemeral sensations, but you are made of sturdier stuff. You gain +2 max HP, +4 max mana.";
        }
        else if (currentPerk == "ego")
        {
            perkTitle.text = "Ego";
            perkDesc.text = "Your base instincts empower you, your very presence warping reality. You gain +20% more gold per kill, and +1 Dreamcoin per completion.";
        }
        UpdatePerkPrice(currentPerk);
    }

    private void UpdatePerkPrice(string perk)
    {
        if (perk == "conceptualization")
        {
            currentPerkPrice = 1; 
        }
        else if (perk == "dreaming")
        {
            currentPerkPrice = 2 * (saveObj.GetSaveData().dreaming + 1);
        }
        else if (perk == "lucid")
        {
            currentPerkPrice = 2 * (saveObj.GetSaveData().lucid + 1);
        }
        else if (perk == "ego")
        {
            currentPerkPrice = 2 * (saveObj.GetSaveData().ego + 1);
        }
        perkCost.text = currentPerkPrice.ToString();
    }

    public void UpdateFromData()
    {
        SaveData data = saveObj.GetSaveData();
        dreamLabel.text = data.dreaming.ToString();
        lucidLabel.text = data.lucid.ToString();
        egoLabel.text = data.ego.ToString();

        if (data.conceptualized)
        {
            dreamButton.GetComponent<Button>().interactable = true;
            lucidButton.GetComponent<Button>().interactable = true; 
            egoButton.GetComponent<Button>().interactable = true; 
        }
        else
        {
            dreamButton.GetComponent<Button>().interactable = false;
            lucidButton.GetComponent<Button>().interactable = false;
            egoButton.GetComponent<Button>().interactable = false;
        }
    }

    public void ActivateConceptualize()
    {
        saveObj.GetSaveData().conceptualized = true;
        UpdateFromData();
    }

    public void IncreaseDreaming()
    {
        saveObj.GetSaveData().dreaming++;
        UpdateFromData();
    }
    
    public void IncreaseLucid()
    {
        saveObj.GetSaveData().lucid++;
        UpdateFromData();
    }
    
    public void IncreaseEgo()
    {
        saveObj.GetSaveData().ego++;
        UpdateFromData();   
    }
}


    
// Dreamperks
// Dreamperks will last between arena tours, and are permanent upgrades to your character.
    
// Conceptualizing
// "Your qualia defined, and this dream ripples in response. You can now carry equipment between runs."
// 1 Dreamcoin
// Unlocks Dreaming 1, Lucid 1, and Ego 1
    
// Dreaming 1-10
// "Reality is frayed, and you pull on the strings. You immediately start with a free level up."
// Dreamcoin Cost: 2, 4, 6, 8, 10, 12, 14, 16, 18, 20
// You buy them one at a time, upgrading it each time
    
// Lucid 1-10
// "It's visions, ephemeral sensations, but you are made of sturdier stuff. You gain +2 max HP, +4 max mana."
// Dreamcoin Cost: 2, 4, 6, 8, 10, 12, 14, 16, 18, 20
// You buy them one at a time, upgrading it each time
    
// Ego 1-10
// "Your base instincts empower you, your very presence warping reality. You gain +20% more gold per kill, and +1 Dreamcoin per completion."
// Dreamcoin Cost: 2, 4, 6, 8, 10, 12, 14, 16, 18, 20
// You buy them one at a time, upgrading it each time
