using System.Collections.Generic;
using System.Linq;
using AutoLayout3D;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Core.SceneObjects
{
    public class LetCube : MonoBehaviour
    {
        private LetColumn Column { get; set; }
        

        private void Start()
        {
            Column = GetComponentInParent<LetColumn>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CubeMoney>())
            {
                var character = other.GetComponentInParent<Character>();
                if (character != null)
                {
                    if (Column.Cubes.Count > character.Cubes.Count - 1)
                    {
                        character.Die();
                    }
                    else
                    {
                        var cube = other.GetComponent<CubeMoney>().transform;
                        Destroy(cube.GetComponent<LayoutElement3D>());
                        cube.GetComponent<Collider>().isTrigger = false;
                        cube.transform.parent = Column.Let.Ground;
                        character.Cubes.Remove(cube); 
                        //Column.Let.Disable();
                    }
                }
            }
            
        }
    }
}