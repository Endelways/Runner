using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public float length;
    public float lineWidth;
    public int linesCount;
    public Vector3 nextSpawnPos = Vector3.zero; 
    public List<Transform> types;
    public Transform previousSpawnedLevel;
}
