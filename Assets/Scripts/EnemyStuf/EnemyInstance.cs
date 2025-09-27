using System;
using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject playerObj;

    // Stats Loaded from Enemy Scriptable Object
    public EnemySO enemySO;
    private float m_Speed;
    private int m_HP;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Speed = enemySO.m_Speed;
        m_HP = enemySO.m_HP;
        gameObject.GetComponent<Renderer>().material.color = enemySO.color;
        gameObject.transform.localScale = enemySO.localScale;
        
        rb = GetComponent<Rigidbody>();
        playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerObj.transform, Vector3.up);
        
        // While it works, its very choppy and sporadic. Often times it ends up unable to move at all.
        // I suspsect it is an interaction with rigid body logic. Might as well use what I got (rb).
        // transform.Translate(transform.forward * (speed * Time.deltaTime));
        
        Vector3 direction = (playerObj.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * m_Speed;
        
        Debug.DrawRay(transform.position, transform.forward * 20.0f, Color.red);
    }

    public void Die()
    {
        Instantiate(enemySO.explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "aura_explosion")
        {
            Die();
        }
    }
}
