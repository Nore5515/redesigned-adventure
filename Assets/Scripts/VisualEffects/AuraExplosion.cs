using System;
using UnityEngine;

public class AuraExplosion : MonoBehaviour
{
    [SerializeField] private SphereCollider circleCollider;

    private float lifetime = 0.0f;
    private float passedTime = 0.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Obsolete("Obsolete")]
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        lifetime = (ps.duration + ps.startLifetime) * 0.5f;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        circleCollider.radius = Mathf.Lerp(3, 9, passedTime / lifetime);
        passedTime += Time.deltaTime;
    }
}
