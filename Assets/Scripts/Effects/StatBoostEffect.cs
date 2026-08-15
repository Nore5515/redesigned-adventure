using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "StatBoostEffect", menuName = "Effect/StatBoostEffect")]

    public class StatBoostEffect : Effect
    {
        
        [Header("Additive")]
        public float manaRegenBoost = 0.0f;
        public float hpRegenBoost = 0.0f;
        public float meleeDamageBoost = 0.0f;
        public float meleePoisonDamage = 0.0f;
        public float spellDamageBoost = 0.0f;
        
        [Header("Multipliers")]
        public float speedBoost = 0.0f;
        public float jumpHeightBoost = 0.0f;

        public override void Apply(Entity caster, Entity[] targets)
        {
            Debug.Log("Apply on stat boost called!");
            // TODO: Swap this out with an entity version. That would be fun.
            PlayerHandler ph = caster.GetGameObject().GetComponent<PlayerEntity>()?.playerHandler;
            if (ph == null) return;
            ph.ApplyStatBoosts(this);
        }
    }
}