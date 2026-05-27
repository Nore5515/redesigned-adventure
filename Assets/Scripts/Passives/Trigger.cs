using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Passives
{
    [CreateAssetMenu(fileName = "Trigger", menuName = "Scriptable Objects/Trigger")]
    [Serializable]
    public class Trigger : ScriptableObject
    {
        public TriggerTypes type;
        
        public TriggerTypes GetTriggerType()
        {
            return type;
        }

        // TODO: Enum these triggers!!!
        // What types...

        // TRIGGERED
        // On kill
        // On unit death (maybe not by you)
        // On damage (received)
        // On damgage (dealt)
        // On spell use
        // On jump
        // On landing

        // NOT TRIGGERED
        // Always On
        // At max mana
        // at max health
        // At X or less mana
        // At X or more mana
        // At X or less health
        // At X or more health
    }
}