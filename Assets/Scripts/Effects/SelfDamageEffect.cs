using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "SelfDamageEffect", menuName = "Effect/SelfDamageEffect")]

    public class SelfDamageEffect : Effect
    {

        public int damageAmount = 1;

        public override void Apply(Entity caster, Entity[] targets)
        {
            GameObject casterGo = caster.GetGameObject();
            if (casterGo.GetComponent<PlayerHandler>() == null) return;
            PlayerHandler ph = casterGo.GetComponent<PlayerHandler>();
            ph.DealDamage(damageAmount);
        }
    }
}