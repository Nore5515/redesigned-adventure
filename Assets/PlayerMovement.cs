using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private Transform groundCheck;
    
    [SerializeField] private float speed = 12.0f;

    [SerializeField] private float knockbackForce = 200.0f;

    [SerializeField] private GameObject explosionPrefab;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3.0f;
    public LayerMask groundMask;
    public LayerMask targetMask;

    [SerializeField] private TextMeshProUGUI noteBuffer;

    private int[] noteArray = new int[10];
    private int notePosition = 0;
    
    public Volume postProcessVolume;
    private MotionBlur motionBlur;
    private LensDistortion lensDistortion;

    private bool isSlowMo = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        postProcessVolume.profile.TryGet(out motionBlur);
        postProcessVolume.profile.TryGet(out lensDistortion);
        motionBlur.intensity.value = 0.0f;
    }

    public void SetSlowMotion(bool timeSlow)
    {
        isSlowMo = timeSlow;
        Time.timeScale = timeSlow ? 0.2f : 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        if (motionBlur is not null)
        {
            motionBlur.intensity.value = timeSlow ? 1.0f : 0.0f;
            lensDistortion.intensity.value = timeSlow ? -0.25f : 0.0f;
            Debug.Log(motionBlur.intensity.value);
        }
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetSlowMotion(true);
            Time.timeScale = 0.25f;
            // Camera.main.fieldOfView = 
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SetSlowMotion(false);
            Time.timeScale = 1.0f;
        }
        
        if (isSlowMo && Input.GetKeyDown(KeyCode.W))
        {
            if (notePosition < noteArray.Length)
            {
                noteArray[notePosition] = 1;
                notePosition++;
                noteBuffer.text = GetNotes();
            }
        }
        if (isSlowMo && Input.GetKeyDown(KeyCode.A))
        {
            if (notePosition < noteArray.Length)
            {
                noteArray[notePosition] = 2;
                notePosition++;
                noteBuffer.text = GetNotes();
            }
        }
        if (isSlowMo && Input.GetKeyDown(KeyCode.S))
        {
            if (notePosition < noteArray.Length)
            {
                noteArray[notePosition] = 3;
                notePosition++;
                noteBuffer.text = GetNotes();
            }
        }
        if (isSlowMo && Input.GetKeyDown(KeyCode.D))
        {
            if (notePosition < noteArray.Length)
            {
                noteArray[notePosition] = 4;
                notePosition++;
                noteBuffer.text = GetNotes();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClearNotes();
        }

        if (!isSlowMo && Input.GetMouseButtonDown(0))
        {
            if (IsSpell1())
            {
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, targetMask);
                if (hit.collider is not null)
                {
                    Instantiate(explosionPrefab, hit.point, Quaternion.identity);
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitGround, Mathf.Infinity, groundMask);
                    if (hitGround.collider is not null)
                    {
                        Instantiate(explosionPrefab, hitGround.point, Quaternion.identity);
                    }
                }
            }
            
            ClearNotes();
        }
    }

    bool IsSpell1()
    {
        int firstNumber = noteArray[0];
        for (int x = 0; x < 4; x++)
        {
            if (noteArray[x] == 0 || noteArray[x] != firstNumber)
            {
                return false;
            }
        }
        return true;
    }

    void ClearNotes()
    {
        for (int i = 0; i < noteArray.Length; i++)
        {
            noteArray[i] = 0;
        }
        notePosition = 0;
        noteBuffer.text = "";
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
