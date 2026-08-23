using Interfaces;
using UnityEngine;

namespace ElevatorScripts
{
    
    public class ElevatorButton : MonoBehaviour, Interactable
    {
        [SerializeField] private Elevator elevator;

        public bool disabled = false;
        
        public void Interact(PlayerEntity p)
        {
            if (!disabled)
            {
                elevator.ButtonPress();
            }
        }
    }
}