using System.Collections.Generic;
using EquipmentNamespace;
using Handler;
using NPCs;
using TMPro;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelUI : MonoBehaviour
{

    [SerializeField] List<Equipment> equipmentList;
    
    public EquipmentNamespace.Equipment selectedEquipment;

    [SerializeField] private List<ShopPurchaseButton> buttons;
    
    [SerializeField] TextMeshProUGUI selectedEquipmentDescText;
    [SerializeField] TextMeshProUGUI selectedEquipmentCostText;
    [SerializeField] Button buyButton;
    
    [InspectorLabel("Neil Sprites")]
    [SerializeField] private Image neilImage;
    [SerializeField] private TextMeshProUGUI neilText;

    [SerializeField] private List<Sprite> neilIdleSprites;
    [SerializeField] private List<Sprite> neilTalkingSprites;
    [SerializeField] private List<Sprite> neilHappyTalkingSprites;

    private NPCTalkingSpriteSO currentSprites;
    
    public float neilFrameTime = 0.5f;
    public NeilState neilState = NeilState.idle;

    private float countdown;
    private int index = 0;
    private int spriteCount = 1;

    private int dialogueIndex = 0;

    [SerializeField]
    private NPCTalkingSpriteSO currentSpritesList;
    
    ShopPurchaseButton selectedButton;
    [SerializeField] InventoryHandler inventoryHandler;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdown = neilFrameTime;
    }

    public void RefreshShop()
    {
        foreach (ShopPurchaseButton shopButton in buttons)
        {
            Equipment randEquip = equipmentList[Random.Range(0, equipmentList.Count)];
            shopButton.SetEquipment(randEquip);
        }
    }

    public void PlayDialogue(Dialogue dialogue)
    {
        neilText.text = dialogue.lines[dialogueIndex].text;
        currentSpritesList = dialogue.lines[dialogueIndex].talkingSprites;
        dialogueIndex++;
        if (dialogueIndex >= dialogue.lines.Count)
        {
            dialogueIndex = 0;
        }
    }

    public void SelectEquipment(Equipment equipment, ShopPurchaseButton button)
    {
        selectedEquipment = equipment;
        selectedEquipmentCostText.text = "$" + equipment.cost;
        selectedEquipmentDescText.text = equipment.description;

        selectedButton = button;
        
        neilText.text = "Ya wanna buy " + equipment.name + "?";
    }

    public void PurchaseEquipment()
    {
        if (selectedEquipment == null)
        {
            neilText.text = "Pick something to buy first. Something not sold out.";
            return;
        }
        PlayerHandler ph = GameObject.FindGameObjectWithTag("player_handler").GetComponent<PlayerHandler>();
        if (ph != null && ph.playerStats.cash >=
            selectedEquipment.cost)
        {
            inventoryHandler.AddItem(selectedEquipment);
            // TODO: Move this logic to inv? Maybe??
            ph.playerStats.cash -= selectedEquipment.cost;
            neilText.text = "Thanks!";
            selectedButton.DisableButton();
            selectedButton = null;
            selectedEquipment = null;
        }
        else
        {
            neilText.text = "You're a little low on funds.";
        }
    }

    // TODO: Replace sprites with currentSprites 
    // Update is called once per frame
    void Update()
    {
        if (countdown >= 0.0f)
        {
            countdown -= Time.unscaledDeltaTime;
        }
        else
        {
            Debug.Log("Neil is " + neilState + " and index is " + index);
            countdown = neilFrameTime;
            index = (index + 1) % spriteCount;
        }
        if (neilState == NeilState.idle)
        {
            neilImage.sprite = neilIdleSprites[index];
            spriteCount = neilIdleSprites.Count;
        }
        else if (neilState == NeilState.talking)
        {
            neilImage.sprite = neilTalkingSprites[index];
            spriteCount = neilTalkingSprites.Count;
        }
        else if (neilState == NeilState.happyTalking)
        {
            neilImage.sprite = neilHappyTalkingSprites[index];
            spriteCount = neilHappyTalkingSprites.Count;
        }
    }

    public void SetNeilState(string state)
    {
        index = 0;
        if (state == "idle")
        {
            neilState = NeilState.idle;
        }
        else if (state == "talking")
        {
            neilState = NeilState.talking;
        }
        else if (state == "happyTalking")
        {
            neilState = NeilState.happyTalking;
        }
    }
    
    public void CloseShop()
    {
        Unpause();
        gameObject.SetActive(false);
    }
    
    public void OpenShop()
    {
        Pause();
        RefreshShop();
        gameObject.SetActive(true);
    }
    
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
    
    public void Unpause()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

public enum NeilState { idle, talking, happyTalking }