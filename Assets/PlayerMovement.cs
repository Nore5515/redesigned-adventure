using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private Transform groundCheck;
    
    [SerializeField] private float speed = 12.0f;

    private Vector3 velocity;
    private bool isGrounded;
    
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3.0f;
    public LayerMask groundMask;

    [SerializeField] private TextMeshProUGUI noteBuffer;

    private int[] noteArray = new int[10];
    private int notePosition = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * (speed * Time.deltaTime));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (notePosition < noteArray.Length)
            {
                noteArray[notePosition] = 1;
                notePosition++;
                noteBuffer.text = GetNotes();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (notePosition < noteArray.Length)
            {
                noteArray[notePosition] = 2;
                notePosition++;
                noteBuffer.text = GetNotes();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < noteArray.Length; i++)
            {
                noteArray[i] = 0;
            }
            notePosition = 0;
            noteBuffer.text = "";
        }
    }

    string GetNotes()
    {
        string note = "";
        for (int i = 0; i < noteArray.Length; i++)
        {
            if (noteArray[i] > 0)
            {
                note += noteArray[i];
            }
        }

        return note;
    }
}
