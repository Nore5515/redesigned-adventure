using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "TemplateEffect", menuName = "Effect/TemplateEffect")]

    public class TemplateEffect : Effect
    {

        public string message;

        public override void Apply(Entity caster, Entity[] targets)
        {
            Debug.Log(message);
        }
    }
}