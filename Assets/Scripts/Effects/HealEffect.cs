using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "HealEffect", menuName = "Effect/HealEffect")]

    public class HealEffect : Effect
    {

        public int healAmount = 1;

        public override void Apply(Entity caster, Entity[] targets)
        {
            GameObject casterGo = caster.GetGameObject();
            if (casterGo.GetComponent<PlayerHandler>() == null) return;
            PlayerHandler ph = casterGo.GetComponent<PlayerHandler>();
            ph.DealDamage(-healAmount);
        }
    }
}