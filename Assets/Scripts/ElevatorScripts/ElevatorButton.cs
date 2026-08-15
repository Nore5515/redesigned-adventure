using Interfaces;
using UnityEngine;

namespace ElevatorScripts
{
    
    public class ElevatorButton : MonoBehaviour, Interactable
    {
        [SerializeField] private Elevator elevator;

        public void Interact(PlayerEntity p)
        {
            elevator.ButtonPress();
        }
    }
}