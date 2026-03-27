using System.Collections;
using UnityEngine;

public class SwordPicHandler : MonoBehaviour
{

    [SerializeField] private GameObject swordUp;
    [SerializeField] private GameObject swordDown;

    public bool swordSpriteState = false;

    private bool isSwinging = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        swordDown.SetActive(false);
    }

    public void TriggerSword()
    {
        if (isSwinging)
        {
            return;
        }

        isSwinging = true;
        StartCoroutine(SwordSwing(0.1f));
    }

    void ToggleSwing()
    {
        swordSpriteState = !swordSpriteState;
        swordUp.SetActive(!swordSpriteState);
        swordDown.SetActive(swordSpriteState);
    }

    IEnumerator SwordSwing(float time)
    {
        ToggleSwing();
        yield return new WaitForSeconds(time);
        ToggleSwing();
        isSwinging = false;
    }
}
