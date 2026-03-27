using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Abilities
{
    public class DamageAoE : Effect
    {
        public float radius = 10.0f;
        public int damage = 10;
        public LayerMask targetLayers;
        
        public override void Apply(GameObject caster, GameObject[] targets)
        {
            Collider[] hits = Physics.OverlapSphere(caster.transform.position, radius, targetLayers);
            foreach (var hit in hits)
            {
                if (hit.gameObject == caster) continue;
                Debug.Log("Target hit! " + hit.gameObject.name);
            }        
        }
    }
}