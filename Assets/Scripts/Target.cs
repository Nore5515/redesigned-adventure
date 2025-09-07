using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject playerObj;
    public float speed;
    private Rigidbody rb;

    private int HP = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        rb.linearVelocity = direction * speed;
        
        Debug.DrawRay(transform.position, transform.forward * 20.0f, Color.red);
    }
}
