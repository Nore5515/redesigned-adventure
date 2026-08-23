using Interfaces;
using UnityEngine;

public class DreamcoinStall : MonoBehaviour, Interactable
{
    [SerializeField]
    public GameObject dreamcoinShopGO;
    
    public void Interact(PlayerEntity p)
    {
        if (dreamcoinShopGO.activeInHierarchy)
        {
            dreamcoinShopGO.SetActive(false);
        }
        else
        {
            dreamcoinShopGO.SetActive(true);
        }
    }
}
