using System.Collections;
using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "SummonWallEffect", menuName = "Effect/SummonWallEffect")]

    public class SummonWallEffect : Effect
    {
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private Material wallMat;
        [SerializeField] private float duration = 10.0f;

        private GameObject wallInstance;
        
        IEnumerator WallDespawn()
        {
            yield return new WaitForSeconds(duration);
            Destroy(wallInstance);       
        }
        
        public override void Apply(Entity caster, Entity[] targets)
        {
            wallInstance = caster.GetGameObject();
            Vector3 wallPos = wallInstance.transform.position;
            wallPos += wallInstance.transform.forward * 10;
            GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.identity);
            wall.GetComponent<WallObject>().Init(100, wallMat, false, 10.0f);
            wall.transform.forward = wallInstance.transform.forward;
            wall.GetComponent<WallObject>().StartCoroutine(WallDespawn());
        }
    }
}