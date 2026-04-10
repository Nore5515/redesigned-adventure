using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "SummonWallEffect", menuName = "Effect/SummonWallEffect")]

    public class SummonWallEffect : Effect
    {
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private Material wallMat;
        
        public override void Apply(Entity caster, Entity[] targets)
        {
            Vector3 wallPos = caster.GetGameObject().transform.position;
            wallPos += caster.GetGameObject().transform.forward * 10;
            GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.identity);
            wall.GetComponent<WallObject>().Init(100, wallMat, false);
            wall.transform.forward = caster.GetGameObject().transform.forward;       
        }
    }
}