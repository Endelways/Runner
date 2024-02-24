using System;
using AutoLayout3D;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Core.SceneObjects
{
    public class CubeMoney : MonoBehaviour
    {
        public bool isCharacterCube = false;
        [SerializeField] private Character _character;
        private void OnTriggerEnter(Collider other)
        {
            var cube = other.GetComponent<CubeMoney>();
            if (cube != null && cube.isCharacterCube && !isCharacterCube)
            { 
                _character = cube.GetComponentInParent<Character>();
                _character.AddCube(this);
                isCharacterCube = true;
                gameObject.AddComponent<LayoutElement3D>();
            }
        }
    }
}