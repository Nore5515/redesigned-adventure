using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Obsolete("Obsolete")]
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        float totalDuration = (ps.duration + ps.startLifetime) * 0.5f;
        Destroy(gameObject, totalDuration);
    }
}
