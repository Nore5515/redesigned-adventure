using System;
using Entities;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fillBar;
    [SerializeField] private GameObject entityGO;
    private Entity entity;

    private void Start()
    {
        entity = entityGO.GetComponent<Entity>();
    }

    private const float xScaleMax = 7.0f;
    
    // Update is called once per frame
    void Update()
    {
        float xScale = ((1.0f * entity.hp) / (1.0f*entity.maxHP)) * xScaleMax;
        Vector3 scale = new Vector3(xScale, 1.0f, 1.0f);
        fillBar.transform.localScale = scale;
        transform.rotation = Camera.main.transform.rotation;
    }
}
