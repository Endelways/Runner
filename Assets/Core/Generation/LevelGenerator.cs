using Core.Generation;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelInfo level;
    public LevelInfo Level => level;
    [SerializeField] private int startLevelsCount;
    void Start()
    {
        Generate();
    }

    public static Transform SpawnNewLevel(LevelInfo level)
    {
        var template = level.types[Random.Range(0, level.types.Count)];
        var newLevel = Instantiate(template, level.nextSpawnPos, new Quaternion());
        level.nextSpawnPos.z += level.length;
        newLevel.GetComponentInChildren<LevelTrigger>().level = level;
        level.previousSpawnedLevels.Add(newLevel);
        return newLevel;
    }

    public void Generate()
    {
        if (level.types.Count > 0 && level.length > 0)
        {
            for (int i = 0; i < startLevelsCount; i++)
                SpawnNewLevel(level);
        } 
    }
    public void ResetLevel()
    {
        level.Clear();
        level.nextSpawnPos = Vector3.zero;
        Generate();
    }
    
}
