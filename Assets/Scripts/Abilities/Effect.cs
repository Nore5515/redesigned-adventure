using UnityEngine;

namespace Abilities
{
    public abstract class Effect : ScriptableObject
    {
        public abstract void Apply(GameObject caster, GameObject[] targets);
    }
}