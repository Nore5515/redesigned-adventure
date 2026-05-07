using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "StatBoostEffect", menuName = "Effect/StatBoostEffect")]

    public class StatBoostEffect : Effect
    {

        public float manaRegenBoost = 0.0f;
        public float hpRegenBoost = 0.0f;
        public float meleeDamageBoost = 0.0f;
        public float movementSpeedBoost = 0.0f;
        public float jumpHeightBoost = 0.0f;
        public float meleePoisonDamage = 0.0f;

        public override void Apply(Entity caster, Entity[] targets)
        {
            // TODO: Swap this out with an entity version. That would be fun.
            if (caster.GetGameObject().GetComponent<PlayerMovement>() == null) return;
        }
    }
}