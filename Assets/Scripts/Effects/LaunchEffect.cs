using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "LaunchEffect", menuName = "Effect/LaunchEffect")]

    public class LaunchEffect : Effect
    {

        public float vertialForce;
        public float pushForce;
        
        public override void Apply(Entity caster, Entity[] targets)
        {
            foreach (var target in targets)
            {
                EnemyInstance enemy = target.GetGameObject().GetComponent<EnemyInstance>();
                if (enemy == null) continue;
                
                enemy.Ragdoll();
                Vector3 verticalForce = new(0.0f, vertialForce, 0.0f);
                Vector3 direction = (enemy.transform.position - caster.GetGameObject().transform.position).normalized;
                enemy.rb.AddForce(direction * pushForce + verticalForce, ForceMode.Impulse);
            }
        }
    }
}