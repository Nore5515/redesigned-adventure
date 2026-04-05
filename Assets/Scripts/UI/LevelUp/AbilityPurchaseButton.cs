using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityPurchaseButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private Image icon;

    [SerializeField] public Button button;
    
    public void Init(string title, string desc, Sprite img, UnityAction lockAbility)
    {
        this.title.text = title;
        this.desc.text = desc;
        icon.sprite = img;  
        button.onClick.AddListener(lockAbility);
    }
}
