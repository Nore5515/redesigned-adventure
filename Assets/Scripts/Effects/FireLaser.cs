using System.Collections;
using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "FireLaserEffect", menuName = "Effect/FireLaserEffect")]

    public class FireLaser : Effect
    {

        public float widthStart;
        public float widthEnd;
        public Color colorStart;
        public Color colorEnd;
        public int dmg;
        public Material material;

        IEnumerator DisableLaser(LineRenderer lineRenderer)
        {
            yield return new WaitForSeconds(0.2f);
            lineRenderer.positionCount = 0;
        }
        
        public override void Apply(Entity caster, Entity[] targets)
        {
            if (caster.GetGameObject().GetComponent<LineRenderer>() == null) return;
            Debug.Log("Laser Fired!");
            
            GameObject casterGo = caster.GetGameObject();
            
            // TODO: It would be SO FUNNY to have every entity having a line renderer...
            // No idea what it would do but having every enemy fire a laser...
            // heheheh...
            LineRenderer lineRenderer = casterGo.GetComponent<LineRenderer>();

            // Set the material
            lineRenderer.material = material;

            // Set the color
            lineRenderer.startColor = colorStart;
            lineRenderer.endColor = colorEnd;

            // Set the width
            lineRenderer.startWidth = widthStart;
            lineRenderer.endWidth = widthEnd;

            // Set the positions of the vertices
            Vector3 startPos = casterGo.transform.position;
            Vector3 targetPos;
            
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 direction = (mouseWorldPos - Camera.main.transform.position).normalized;
            
            // Create ray from caster position in the direction of the mouse
            Ray ray = new Ray(startPos, direction);
            
            targetPos = ray.origin + ray.direction * 1000.0f;
            
            RaycastHit[] hits = Physics.RaycastAll(ray);
            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.GetComponent<Entity>() != null)
                    {
                        Entity hitEntity = hit.collider.GetComponent<Entity>();
                        if (hitEntity != caster)
                        {
                            hitEntity.DealDamage(dmg);
                        }
                    }
                }
            }
            
            // Set the vertices
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, targetPos);

            casterGo.GetComponent<MonoBehaviour>().StartCoroutine((DisableLaser(lineRenderer)));
        }
    }
}