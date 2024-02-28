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
                InteractEvents.OnMoneyInteract(this);
            }
        }
    }
}