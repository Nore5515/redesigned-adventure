using System;
using System.Collections;
using Interfaces;
using UnityEngine;

public class ShopStall : MonoBehaviour, Interactable
{
    private static readonly int FallDown = Animator.StringToHash("FallDown");
    private static readonly int StandUp = Animator.StringToHash("StandUp");
    public ShopPanelUI shopPanel;

    private float sleepX = -90.0f;
    private float awakeX = 0.0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(PlayerEntity p)
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

    public void ShopDown(float delayTime)
    {
        animator.SetTrigger(FallDown);
        StartCoroutine(DelayShop(delayTime));
    }

    IEnumerator DelayShop(float time)
    {
        yield return new WaitForSeconds(time);
        ShopUp();
    }

    public void ShopUp()
    {
        animator.SetTrigger(StandUp);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            animator.SetTrigger(StandUp);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            animator.SetTrigger(FallDown);
        }
    }
}
