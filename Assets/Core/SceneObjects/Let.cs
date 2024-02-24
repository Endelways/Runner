using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.SceneObjects
{
    public class Let : MonoBehaviour
    {
        public List<LetColumn> Columns { get; set; }
        public Transform Ground { get; set; }

        private void Start()
        {
            Columns = GetComponentsInChildren<LetColumn>().ToList();
            Ground = transform.parent;
        }

        public void Disable()
        {
            var cubeColliders = GetComponentsInChildren<Collider>();
            foreach (var col in cubeColliders)
            {
                col.isTrigger = false;
            }
        }
    }
}