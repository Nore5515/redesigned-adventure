using Entities;
using UnityEngine;

namespace Abilities
{
    public abstract class Effect : ScriptableObject
    {
        public abstract void Apply(Entity caster, Entity[] targets);
    }
}