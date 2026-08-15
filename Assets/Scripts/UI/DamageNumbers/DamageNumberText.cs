using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageNumberText : MonoBehaviour
{
    public float lifeTime;
    public GameObject enemy;
    public TextMeshProUGUI text;
    private Vector3 dest;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void InstantiateDamageNumber(int damage, GameObject enemy)
    {
        this.enemy = enemy;
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        text.text = damage.ToString();
        lifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
    }
}
