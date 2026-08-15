using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Entities;
using Interfaces;
using Passives;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Debug = UnityEngine.Debug;

public class PlayerEntity : MonoBehaviour, Entity
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private Transform groundCheck;
    
    [SerializeField] private float speed = 12.0f;

    [SerializeField] private float knockbackForce = 200.0f;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject auraExplosionPrefab;
    
    [SerializeField] private SwordPicHandler swordPicHandler;

    [SerializeField] public GameObject forwardAnchor;
    
    private BanjoNoteHandler banjoNoteHandler;
    private GameObject banjoImage;
    [SerializeField]
    public PlayerHandler playerHandler;
    
    private Vector3 velocity;
    private bool isGrounded;

    private float dist = -25.0f;
    public float swordRange = 5.0f;
    public int swordDamage = 1;
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
    
    public LevelSelectPanel levelSelectPanel;
    public ShopPanelUI shopPanel;
    public InvPanelUI invPanel;
    
    SaveObject saveObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveObject = GameObject.FindGameObjectWithTag("save").GetComponent<SaveObject>();
        
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

    #region Entity Functions

    public int hp { get; set; } = 8;
    public int maxHP { get; set; } = 8;

    public void TakeDamageFromSource(int dmg, Entity source)
    {
        playerHandler.DealDamage(dmg);
        if (hp <= 0)
        {
            source.KillReward(GetXPReward(), GetCashReward(), GetScoreReward());
        }
    }

    public int GetScoreReward()
    {
        return 1000;
    }

    public void DealKnockback(float knockback, Entity source)
    {
        Debug.Log("No knockback for player!");
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetXPReward()
    {
        return playerHandler.playerStats.level * 100;
    }

    public int GetCashReward()
    {
        // TODO: LOL
        return 10;
    }

    // Entities call this when slaying enemies
    public void KillReward(int xp, int cash, int score)
    {
        playerHandler.AddXp(xp);
        playerHandler.playerStats.cash += cash;
        playerHandler.playerStats.score += score;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    #endregion
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Camera cam = Camera.main;
            Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 10.0f);
            if (hit.collider is not null)
            {
                if (hit.collider.GetComponent<Interactable>() is not null)
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    interactable.Interact(this);
                }
            }   
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (levelSelectPanel != null)
            {
                if (levelSelectPanel.gameObject.activeSelf == false)
                {
                    Debug.Log("Opening level select");
                    levelSelectPanel.GetComponent<LevelSelectPanel>().OpenPanel();
                }
                else
                {
                    Debug.Log("Closing level select");
                    levelSelectPanel.GetComponent<LevelSelectPanel>().ClosePanel();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerHandler.playerStats.cash += 9999;
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (invPanel.gameObject.activeSelf == false)
            {
                invPanel.OpenInv();
            }
            else
            {
                invPanel.CloseInv();
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

        float moveSpeed = playerHandler.playerStats.speed * playerHandler.playerStats.statMods.speedMultiplier;
        controller.Move(move * (moveSpeed * Time.deltaTime));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.L))
        {
            KillReward(100, 100, 100);
        }

        if (!isSlowMo && Input.GetMouseButtonDown(0))
        {
            SwordSwing();
        }
    }

    void SwordSwing()
    {
        swordPicHandler.TriggerSword();
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitSword, swordRange, targetMask);
        if (hitSword.collider != null)
        {
            Entity entity = hitSword.collider.gameObject.GetComponent<Entity>();
            FireTrigger(TriggerTypes.OnSwordHit, this, new []{entity});
            
            if (entity.GetHP() <= swordDamage)
            {
                FireTrigger(TriggerTypes.OnSwordKill, this, new []{entity});
            }
            
            entity.TakeDamageFromSource(swordDamage, this);
        }
    }

    private void FireTrigger(TriggerTypes trigger, Entity caster, Entity[] targets)
    {
        if (playerHandler.HasTrigger(trigger))
        {
            playerHandler.TriggerAllOfType(trigger, caster, targets);
        }
    }
}
