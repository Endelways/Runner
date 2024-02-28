using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public float length;
    public float lineWidth;
    public int linesCount;
    public Vector3 nextSpawnPos = Vector3.zero; 
    public List<Transform> types;
    public List<Transform> previousSpawnedLevels;

    public void Clear()
    {
        foreach (var level in previousSpawnedLevels)
        {
            Destroy(level.gameObject);
        }
        previousSpawnedLevels.Clear();
    }
}
