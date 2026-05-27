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
        public List<Trigger> triggers;
        public List<Effect> effects;
    }
}