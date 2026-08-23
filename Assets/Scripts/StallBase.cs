using System.Collections;
using Interfaces;
using UnityEngine;

public class StallBase : MonoBehaviour
{
    private static readonly int FallDown = Animator.StringToHash("FallDown");
    private static readonly int StandUp = Animator.StringToHash("StandUp");

    private float sleepX = -90.0f;
    private float awakeX = 0.0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
