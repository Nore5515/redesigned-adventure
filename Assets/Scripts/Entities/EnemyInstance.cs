using System;
using System.Collections;
using Entities;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInstance : MonoBehaviour, Entity
{
    public GameObject playerObj;
    private PlayerHandler playerHandler;

    [SerializeField] private GameObject meshObj;
    [SerializeField] private GameObject healthbar;
    
    // Stats Loaded from Enemy Scriptable Object
    public EnemySO enemySO;
    private float m_Speed;
    private float m_Acceleration;
    public int hp { get; set; }
    public int maxHP { get; set; }
    private int xpReward;
    private int cashReward;
    private int scoreReward;

    private NavMeshAgent agent;

    [SerializeField] private float watchBoxColliderSize = 4.0f;

    public bool isWatched = false;

    public Rigidbody rb;

    public bool knockbacked = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_Speed = enemySO.m_Speed;
        m_Acceleration = enemySO.m_Acceleration;
        hp = enemySO.m_HP;
        maxHP = enemySO.m_HP;
        xpReward = enemySO.xpReward;
        cashReward = enemySO.cashReward;
        scoreReward = enemySO.scoreReward;
        
        // Mesh Scaling
        meshObj.GetComponent<Renderer>().material.color = enemySO.color;
        meshObj.transform.localScale = enemySO.localScale;
        Vector3 tempPos = meshObj.transform.position;
        meshObj.transform.position = tempPos + new Vector3(0.0f, enemySO.localScale.y - 1, 0.0f);

        MeshRenderer mr = meshObj.GetComponent<MeshRenderer>();
        float healthbarY = mr.bounds.size.y * 0.5f;
        // take the height, half it, then add 0.5f for a nice floating HP bar
        healthbar.transform.position = new Vector3(meshObj.transform.position.x, meshObj.transform.position.y + healthbarY + 0.5f, meshObj.transform.position.z);
        
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
        Debug.DrawRay(transform.position, transform.forward * 20.0f, Color.red);
        
        if (!knockbacked)
        {
            MovementLogic(playerObj.transform.position);
        }
    }

    void MovementLogic(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        Vector3 playerFeet = new Vector3(pos.x, pos.y-2.5f, pos.z);
        if (NavMesh.CalculatePath(transform.position, playerFeet, NavMesh.AllAreas, path)) {
            agent.SetDestination(pos);
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
    
    public void TakeDamageFromSource(int damage, Entity source)
    {
        hp -= damage;
        if (hp <= 0)
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
        return hp;
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