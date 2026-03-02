using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Door
{
    public class DoorScript : MonoBehaviour
    {
        private GameObject door;
        private NavMeshSurface navMesh;

        public void Start()
        {
            for (int x = 0; x < transform.childCount; x++)
            {
                if (transform.GetChild(x).name == "Door")
                {
                    door = transform.GetChild(x).gameObject;
                }
            }
        }
        
        public void ButtonPressed()
        {
            door.SetActive(!door.activeSelf);
        }
        
        
    }
}