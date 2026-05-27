using System;
using Entities;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public abstract class Effect : ScriptableObject
    {
        public abstract void Apply(Entity caster, Entity[] targets);
    }
}