using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "Effect/DamageEffect")]

    public class DamageEffect : Effect
    {

        public int damage;

        public override void Apply(Entity caster, Entity[] targets)
        {
            foreach (var target in targets)
            {
                target.ReceieveDamage(damage, caster);
            }       
        }
    }
}