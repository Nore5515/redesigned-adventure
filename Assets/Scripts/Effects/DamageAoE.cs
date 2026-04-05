using Entities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Abilities
{    
    [CreateAssetMenu(fileName = "DamageAoE", menuName = "Effect/DamageAoE")]

    public class DamageAoE : Effect
    {
        public float radius = 10.0f;
        public int damage = 10;
        public LayerMask targetLayers;
        
        public override void Apply(Entity caster, Entity[] targets)
        {
            Collider[] hits = Physics.OverlapSphere(caster.GetGameObject().transform.position, radius, targetLayers);
            foreach (var hit in hits)
            {
                if (hit.gameObject == caster.GetGameObject()) continue;
                Debug.Log("Target hit! " + hit.gameObject.name);
            }        
        }
    }
}