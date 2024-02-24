using System.Collections;
using System.Collections.Generic;
using Core.Generation;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private LevelInfo level;
    [SerializeField] private int startLevelsCount;
    void Start()
    {
        if (level.types.Count > 0 && level.length > 0)
        {
            for (int i = 0; i < startLevelsCount; i++)
            {
                SpawnNewLevel(level);
            }
        } 
    }

    public static Transform SpawnNewLevel(LevelInfo level)
    {
        var template = level.types[Random.Range(0, level.types.Count)];
        var newLevel = Instantiate(template, level.nextSpawnPos, new Quaternion());
        level.nextSpawnPos.z += level.length;
        newLevel.GetComponentInChildren<LevelTrigger>().level = level;
        return newLevel;
    }
  
    void Update()
    {
        
    }
}
