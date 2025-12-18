using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInstance : MonoBehaviour
{
    private GameObject playerObj;

    // Stats Loaded from Enemy Scriptable Object
    public EnemySO enemySO;
    private float m_Speed;
    private float m_Acceleration;
    private int m_HP;

    private NavMeshAgent agent;

    [SerializeField] private float watchBoxColliderSize = 4.0f;

    public bool isWatched = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Speed = enemySO.m_Speed;
        m_Acceleration = enemySO.m_Acceleration;
        m_HP = enemySO.m_HP;
        gameObject.GetComponent<Renderer>().material.color = enemySO.color;
        gameObject.transform.localScale = enemySO.localScale;
        
        playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
        agent = GetComponent<NavMeshAgent>();

        agent.speed = m_Speed;
        agent.acceleration = m_Acceleration;

        if (enemySO.weeping)
        {
            gameObject.tag = "weeping";
            BoxCollider watchBoxCollider = gameObject.AddComponent<BoxCollider>();
            watchBoxCollider.size = new Vector3(watchBoxColliderSize,watchBoxColliderSize,watchBoxColliderSize);
            watchBoxCollider.isTrigger = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(playerObj.transform, Vector3.up);
        
        // While it works, its very choppy and sporadic. Often times it ends up unable to move at all.
        // I suspsect it is an interaction with rigid body logic. Might as well use what I got (rb).
        // transform.Translate(transform.forward * (speed * Time.deltaTime));
        //
        // Vector3 direction = (playerObj.transform.position - transform.position).normalized;
        // rb.linearVelocity = direction * m_Speed;
        //
        Debug.DrawRay(transform.position, transform.forward * 20.0f, Color.red);

        if (enemySO.weeping && isWatched)
        {
            agent.SetDestination(this.transform.position);
        }
        else
        {
            agent.SetDestination(playerObj.transform.position);
        }
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
