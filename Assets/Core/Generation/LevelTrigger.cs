using System;
using System.Linq;
using UnityEngine;

namespace Core.Generation
{
    public class LevelTrigger : MonoBehaviour
    {
        public static GameObject lastCompletedLevel;
        public LevelInfo level;
        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject.GetComponent<Collider>());
            if (lastCompletedLevel != null)
            {
                level.previousSpawnedLevels.Remove(lastCompletedLevel.transform);
                Destroy(lastCompletedLevel);
            }
            lastCompletedLevel = transform.parent.gameObject;
            LevelGenerator.SpawnNewLevel(level);
            Debug.Log($"Collision {gameObject.name} and {other.gameObject.name}");
        }
    }
}