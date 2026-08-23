using System;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElevatorScripts
{
    
public class Elevator : MonoBehaviour
{
    public bool defaultOff = false;
    
    public bool movingUp = false;
    public bool movingDown = false;

    public Vector3 top;
    public float heightForTop = 10.0f;
    public Vector3 bottom;

    public float speed;

    private GameObject glowBox;
    
    [SerializeField] private GameObject ghost;

    [SerializeField] private Material glowMat;
    [SerializeField] private Material flatMat;

    
    public string levelDestination;
    
    private void Start()
    {
        Initalize();
        ghost.SetActive(false);
        if (defaultOff)
        {
            SetDest(null);
        }
    }

    public void UpdateLevel(string newLevel)
    {
        levelDestination = newLevel;
    }

    void Initalize()
    {
        glowBox = transform.Find("GlowBox").gameObject;
        bottom = transform.position;
        top = new Vector3(bottom.x, bottom.y + heightForTop, bottom.z);
    }

    public void ButtonPress()
    {
        SceneManager.LoadScene(levelDestination);
    }

    public void SetDest([CanBeNull] string newDest)
    {
        if (newDest != null)
        {
            glowBox.GetComponent<Renderer>().material = glowMat;
            glowBox.GetComponent<ElevatorButton>().disabled = false;
        }
        else
        {
            glowBox.GetComponent<Renderer>().material = flatMat;
            glowBox.GetComponent<ElevatorButton>().disabled = true;
        }   
        Debug.Log("New Level is " + newDest);
        levelDestination = newDest;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SummonElevator();
        }

        if (movingUp && movingDown)
        {
            Debug.Log("Moving up AND down!");
        }

        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, top, speed * Time.deltaTime);
            if ((transform.position - top).magnitude < 0.05f)
            {
                movingUp = false;
            }
        }
        if (movingDown && !movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, bottom, speed * Time.deltaTime);
            if ((transform.position - bottom).magnitude < 0.05f)
            {
                movingDown = false;
            }
        }
    }

    public void SummonElevator()
    {
        if ((transform.position - bottom).magnitude < 0.05f)
        {
            //
        }
        else
        {
            //Debug.LogError("ELEVATOR ERROR!");
        }
        movingUp = true;
        movingDown = false;
    }
    
    [Button("Send Ghost Up")]
    [ExecuteInEditMode]
    public void GhostUp()
    {
        Initalize();
        ghost.transform.position = new Vector3(top.x, top.y + 3.5f, top.z);
    }
    
    [Button("Send Ghost Down")]
    [ExecuteInEditMode]
    public void GhostDown()
    {
        Initalize();
        ghost.transform.position = bottom;
    }
    
    

    public void MoveDown()
    {
        
    }
}
}
