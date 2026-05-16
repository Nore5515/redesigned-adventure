using Interfaces;
using UnityEngine;
using UnityEngine.Rendering;

namespace Door
{
    public class ButtonScript : MonoBehaviour, Interactable
    {
        DoorScript doorScript;

        public void Start()
        {
            doorScript = transform.parent.GetComponent<DoorScript>();
        }
        
        public void ButtonPressed()
        {
            doorScript.ButtonPressed();
        }

        public void Interact(PlayerMovement p)
        {
            ButtonPressed();
        }
    }
}