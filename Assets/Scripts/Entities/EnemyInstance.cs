using System;
using System.Collections;
using Entities;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInstance : MonoBehaviour, Entity
{
    private GameObject playerObj;
    private PlayerHandler playerHandler;

    [SerializeField] private GameObject meshObj;

    // Stats Loaded from Enemy Scriptable Object
    public EnemySO enemySO;
    private float m_Speed;
    private float m_Acceleration;
    private int m_HP;
    private int xpReward;
    private int cashReward;
    private int scoreReward;

    private NavMeshAgent agent;

    [SerializeField] private float watchBoxColliderSize = 4.0f;

    public bool isWatched = false;

    public Rigidbody rb;

    private bool knockbacked = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_Speed = enemySO.m_Speed;
        m_Acceleration = enemySO.m_Acceleration;
        m_HP = enemySO.m_HP;
        xpReward = enemySO.xpReward;
        cashReward = enemySO.cashReward;
        scoreReward = enemySO.scoreReward;
        
        // Mesh Scaling
        meshObj.GetComponent<Renderer>().material.color = enemySO.color;
        meshObj.transform.localScale = enemySO.localScale;
        Vector3 tempPos = meshObj.transform.position;
        meshObj.transform.position = tempPos + new Vector3(0.0f, enemySO.localScale.y - 1, 0.0f);
        
        // Collider Scaling
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius *= enemySO.localScale.x;
        capsuleCollider.height *= enemySO.localScale.y;
        capsuleCollider.center = new Vector3(capsuleCollider.center
            .x, capsuleCollider.center.y + enemySO.localScale.y - 1, capsuleCollider.center.z);
        
        playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerHandler = GameObject.FindGameObjectWithTag("player_handler").GetComponent<PlayerHandler>();
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

        if (!knockbacked)
        {
            if (enemySO.weeping && isWatched)
            {
                agent.SetDestination(this.transform.position);
            }
            else
            {
                MovementLogic(playerObj.transform.position);
            }
        }
    }

    void MovementLogic(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path)) {
            if (path.status == NavMeshPathStatus.PathComplete) {
                // Path exists!
                agent.SetDestination(pos);
            }
            else
            {
                // we want them to move to the nearest door interactable to open it up and try again.
                agent.SetDestination(pos);

            }
        }
    }

    public void StruckPlayer()
    {
        playerHandler.DealDamage(enemySO.m_Dmg);
    }

    public void FlingAwayFromPoint(Vector3 point, float kbForce)
    {
        if (!knockbacked)
        {
            Ragdoll();
            Vector3 direction = (transform.position - point).normalized;
            rb.AddForce(direction * kbForce, ForceMode.Impulse);
        }
    }

    public void Ragdoll()
    {
        EnableRigidbody(true);
        StartCoroutine(KnockbackCountdown(3.0f));
        knockbacked = true;
    }

    IEnumerator KnockbackCountdown(float time, int retries = 10)
    {
        yield return new WaitForSeconds(time);
        if (retries <= 0)
        {
            Die();
        }
        // See if on the ground! Check the global direction down though, not the local
        // direction down.
        if (Physics.Raycast(transform.position, Vector3.down, Mathf.Max(enemySO.localScale.x, enemySO.localScale.y)))
        {
            Debug.Log("Grounded!");
            knockbacked = false;
            EnableRigidbody(false);
        }
        else
        {
            StartCoroutine(KnockbackCountdown(1.0f, retries-1));
        }
    }

    void EnableRigidbody(bool enable)
    {
        rb.isKinematic = !enable;
        agent.enabled = !enable;
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

    #region Entity Functions
    
    public void ReceieveDamage(int damage, Entity source)
    {
        m_HP -= damage;
        if (m_HP <= 0)
        {
            source.KillReward(GetXPReward(), GetCashReward(), GetScoreReward());
            Die();
        }
    }

    public int GetScoreReward()
    {
        return scoreReward;
    }

    public void DealKnockback(float knockback, Entity source)
    {
        FlingAwayFromPoint(source.GetGameObject().transform.position, knockback);
    }

    public int GetHP()
    {
        return m_HP;
    }

    public int GetXPReward()
    {
        return xpReward;
    }

    public int GetCashReward()
    {
        return cashReward;
    }
    
    public void KillReward(int xp, int cash, int score)
    {
        Debug.Log("ENEMY GOT A KILL LOL!");
        throw new NotImplementedException();
    }
    

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    #endregion
}
