using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Vampirism", menuName = "Effect/Vampirism")]

    public class Vampirism : Effect
    {
        [SerializeField] private int vampirismStrength;
        
        public override void Apply(Entity caster, Entity[] targets)
        {
            
            if (targets.Length == 0)
            {
                return;
            }
            
            // Heal 
            caster.TakeDamageFromSource(-vampirismStrength, targets[0]);
            
            // Damage Boost
            foreach (Entity e in targets)
            {
                e.TakeDamageFromSource(vampirismStrength, caster);
            }
        }
    }
}