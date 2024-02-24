using System;
using UnityEngine;

namespace Core.Generation
{
    public class LevelTrigger : MonoBehaviour
    {
        public LevelInfo level;
        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject.GetComponent<Collider>());
            if(level.previousSpawnedLevel != null)
                Destroy(level.previousSpawnedLevel.gameObject);
            LevelGeneration.SpawnNewLevel(level);
            level.previousSpawnedLevel = transform.parent;
            Debug.Log($"Collision {gameObject.name} and {other.gameObject.name}");
            
        }
    }
}