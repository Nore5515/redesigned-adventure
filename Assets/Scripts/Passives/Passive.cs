using System;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Passives
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Passive", fileName = "Passive")]
    [Serializable]
    public class Passive : ScriptableObject
    {
        public Trigger trigger;
        public List<Effect> effects;
    }
}