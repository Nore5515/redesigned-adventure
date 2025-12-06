using System;
using System.Linq;
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
    [SerializeField] private GameObject auraExplosionPrefab;

    
    
    private BanjoNoteHandler banjoNoteHandler;
    private GameObject banjoImage;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3.0f;
    public LayerMask groundMask;
    public LayerMask targetMask;

    [SerializeField] private TextMeshProUGUI noteBuffer;

    private char[] noteArray = new char[10];

    private char[] spell2Array = {'W','W','S','S','W','W', '\0', '\0', '\0', '\0'};
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

        banjoNoteHandler = GameObject.FindGameObjectWithTag("banjo_note_handler").GetComponent<BanjoNoteHandler>();
        banjoImage = GameObject.FindGameObjectWithTag("banjo_image");
        banjoImage.SetActive(false);
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
            // Debug.Log(motionBlur.intensity.value);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Camera cam = Camera.main;
            Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 10.0f);
            if (hit.collider is not null)
            {
                if (hit.collider.GetComponent<StaticCharacter>() is not null)
                {
                    StaticCharacter staticCharacter = hit.collider.GetComponent<StaticCharacter>();
                    GameObject.FindGameObjectWithTag("dialogue_box").GetComponent<DialogueBox>().LoadLines(staticCharacter.dialogue.lines);
                }
            }   
        }
        
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
            banjoImage.SetActive(true);
            Time.timeScale = 0.25f;
            // Camera.main.fieldOfView = 
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            banjoImage.SetActive(false);
            SetSlowMotion(false);
            Time.timeScale = 1.0f;
        }
        
        if (isSlowMo && Input.GetKeyDown(KeyCode.W))
        {
            if (notePosition < noteArray.Length)
            {
                noteArray[notePosition] = 'W';
                notePosition++;
                noteBuffer.text = GetNotes();
                banjoNoteHandler.PlayA();
            }
        }
        if (isSlowMo && Input.GetKeyDown(KeyCode.A))
        {
            if (notePosition < noteArray.Length)
            {
                // noteArray[notePosition] = 'A';
                // notePosition++;
                // noteBuffer.text = GetNotes();
                // banjoNoteHandler.PlayB();
                RunNote('A', banjoNoteHandler.PlayB);
            }
        }

        void RunNote(char c, Action playNote)
        {
            noteArray[notePosition] = c;
            notePosition++;
            noteBuffer.text = GetNotes();
            playNote();
        }
        
        if (isSlowMo && Input.GetKeyDown(KeyCode.S))
        {
            if (notePosition < noteArray.Length)
            {
                noteArray[notePosition] = 'S';
                notePosition++;
                noteBuffer.text = GetNotes();
                banjoNoteHandler.PlayE();
            }
        }
        if (isSlowMo && Input.GetKeyDown(KeyCode.D))
        {
            if (notePosition < noteArray.Length)
            {
                noteArray[notePosition] = 'D';
                notePosition++;
                noteBuffer.text = GetNotes();
                banjoNoteHandler.PlayG();
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
                    hit.collider.gameObject.GetComponent<EnemyInstance>().Die();
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
			else if (IsSpell2()){
                Instantiate(auraExplosionPrefab, transform.position, Quaternion.identity);
			}
            
            ClearNotes();
        }
    }

    bool IsSpell1()
    {
        char firstNumber = noteArray[0];
        for (int x = 0; x < 4; x++)
        {
            if (noteArray[x] == 0 || noteArray[x] != firstNumber)
            {
                return false;
            }
        }
        return true;
    }

	bool IsSpell2(){
        return CompareCharArrays(noteArray, spell2Array);
        
        char firstNumber = noteArray[0];
        for (int x = 0; x < 2; x++)
        {
            if (noteArray[x] == 0 || noteArray[x] != 'W')
            {
                return false;
            }
        }
        for (int x = 2; x < 4; x++)
        {
            if (noteArray[x] == 0 || noteArray[x] != 'S')
            {
                return false;
            }
        }
        for (int x = 4; x < 6; x++)
        {
            if (noteArray[x] == 0 || noteArray[x] != 'W')
            {
                return false;
            }
        }
		return true;	
	}

    /// <summary>
    ///  Returns true if the two arrays are equal.
    /// </summary>
    bool CompareCharArrays(char[] array1, char[] array2)
    {
        if (array1.Length != array2.Length)
        {
            return false;
        }

        for (int x = 0; x < array1.Length; x++)
        {
            if (array1[x] != array2[x])
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
            noteArray[i] = '\0';
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
