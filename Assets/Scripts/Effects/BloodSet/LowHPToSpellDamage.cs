using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "AuraEffect", menuName = "Effect/AuraEffect")]

    public class LowHPToSpellDamage : Effect
    {
        public override void Apply(Entity caster, Entity[] targets)
        {
            Debug.Log("Apply on stat boost called!");
            // TODO: Swap this out with an entity version. That would be fun.
            PlayerHandler ph = caster.GetGameObject().GetComponent<PlayerEntity>()?.playerHandler;
            if (ph == null) return;
            // y = 2 - 2(hp/maxHP) will give us 2 damage at 0 HP, and close to it at 1. It will also give better boosts
            // when at higher max HP.
            // The 1.0f is the base 100% damage. 
            float zeroHealthMult = 2.0f;
            float damage = 1.0f + (zeroHealthMult - zeroHealthMult * ((1.0f * ph.playerEntity.hp) / ph.playerEntity.maxHP));
            ph.SetSpellDamage(damage);
        }
    }
}