using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "AuraEffect", menuName = "Effect/AuraEffect")]

    public class AuraEffect : Effect
    {

        public float range;
        public int dmg;
        public int kbForce;
        public Effect[] appliedEffects;
        
        public override void Apply(Entity caster, Entity[] targets)
        {
            Collider[] hits = Physics.OverlapSphere(caster.GetGameObject().transform.position, range, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            foreach (var hit in hits)
            {
                if (hit.gameObject == caster.GetGameObject()) continue;
                if (hit.GetComponent<Entity>() == null) continue;
                Debug.Log(hit.gameObject.name);
                hit.GetComponent<Entity>().DealKnockback(kbForce, caster);
                foreach (var effect in appliedEffects)
                {
                    effect.Apply(caster, new []{hit.GetComponent<Entity>()});
                }
            }        
        }
    }
}