using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.SceneObjects
{
    public class LetColumn : MonoBehaviour
    {
        public List<LetCube> Cubes { get; private set; }
        public Let Let { get; private set; }

        private void Start()
        {
            Cubes = GetComponentsInChildren<LetCube>().ToList();
            Let = GetComponentInParent<Let>();
        }
        
    }
}