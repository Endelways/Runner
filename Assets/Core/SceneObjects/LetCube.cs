using System.Linq;
using UnityEngine;

namespace Core.SceneObjects
{
    public class LetCube : MonoBehaviour
    {
        public LetColumn Column { get; private set; }
        

        private void Start()
        {
            Column = GetComponentInParent<LetColumn>();
        }
        private void OnTriggerEnter(Collider other)
        {
            var cubeMoney = other.GetComponent<CubeMoney>();
            var anotherColumns = Column.Let.Columns.Where(column => column != Column);
            foreach(var column in anotherColumns)
            {
                foreach (var cubeCollider in column.GetComponentsInChildren<Collider>())
                {
                    cubeCollider.isTrigger = false;
                }
               
            }

            GetComponent<Collider>().isTrigger = false;
            if (cubeMoney != null && cubeMoney.isCharacterCube)
            { 
                InteractEvents.OnLetInteract(this, cubeMoney);
            }
            
        }
    }
}