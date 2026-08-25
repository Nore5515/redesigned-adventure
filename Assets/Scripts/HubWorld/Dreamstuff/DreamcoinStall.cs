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
            Unpause();
        }
        else
        {
            dreamcoinShopGO.SetActive(true);
            Pause();
        }
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
