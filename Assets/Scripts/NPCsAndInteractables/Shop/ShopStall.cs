using Interfaces;
using UnityEngine;

public class ShopStall : MonoBehaviour, Interactable
{
    public ShopPanelUI shopPanel;

    public void Interact(PlayerMovement p)
    {
        if (shopPanel.gameObject.activeSelf == false)
        {
            shopPanel.OpenShop();
        }
        else
        {
            shopPanel.CloseShop();
        }    
    }
}
