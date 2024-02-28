using System.Collections.Generic;
using System.Linq;
using Core.Generation;
using UnityEngine;

public class GenerateDecoration : MonoBehaviour
{
    [SerializeField] private int blocks_count;
    [SerializeField] private int lets_count;
    [SerializeField] private Transform block;
    [SerializeField] private List<Transform> letsVariants;
    private LevelInfo level;
    private List<Vector3> cubeMoneysPositionsList = new List<Vector3>();
    private List<Vector3> letsPositionsList = new List<Vector3>();
    void Start()
    {
        level = GetComponentInChildren<LevelTrigger>().level;
        GenerateCubeMoneys();
        GenerateLets();
        cubeMoneysPositionsList.Clear();
        letsPositionsList.Clear();
    }
    
    private void GenerateCubeMoneys()
    {
        var maxCubesCount = level.linesCount * level.length;
        if (blocks_count > 0 && blocks_count < maxCubesCount)
        {
            while (cubeMoneysPositionsList.Count < blocks_count)
            {
                var newVec = new Vector3 (
                    level.lineWidth * (int)(level.linesCount / 2) - Random.Range(0, level.linesCount) * level.lineWidth,
                    0.9f,
                    (level.length / 2 - level.lineWidth) * -1 + Random.Range(0, 25) * level.lineWidth + transform.position.z
                        );
                if (cubeMoneysPositionsList.Count(vec => Vector3.Distance(vec, newVec) < 3) < 1)
                {
                    cubeMoneysPositionsList.Add(newVec);
                }
            }

            foreach (var t in cubeMoneysPositionsList)
            {
                Instantiate(block,  t, new Quaternion(), transform);
            }
        }
    }

    private void GenerateLets()
    {
        var maxLetsCount = (int) level.length / 2;
        if (lets_count > 0 && lets_count < maxLetsCount)
        {
            while (letsPositionsList.Count < lets_count)
            {
                var newVec = new Vector3 (
                    0,
                    0.9f,
                    transform.position.z + Random.Range((level.length - 1) / 2, -(level.length-1) / 2)
                );
                if (letsPositionsList.Count(vec => Vector3.Distance(vec, newVec) < 6) < 1 && 
                    cubeMoneysPositionsList.Count(vec => Vector3.Distance(vec, newVec) < 2) < 1)
                {
                    letsPositionsList.Add(newVec);
                }
            }

            foreach (var t in letsPositionsList)
            {
                Instantiate(letsVariants[Random.Range(0, letsVariants.Count)],  t, new Quaternion(), transform);
            }
        }
    }
}
