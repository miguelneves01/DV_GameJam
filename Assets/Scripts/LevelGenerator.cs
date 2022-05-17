using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private const float SPAWN_DISTANCE = 100f;

    [SerializeField] private Transform startLevelPart;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Transform player;

    private Vector3 lastEndPoint;
    private void Awake()
    {
        lastEndPoint = startLevelPart.Find("EndPoint").position;

        int startingParts = 5;
        for (int i = 0; i < startingParts; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, lastEndPoint) < SPAWN_DISTANCE)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0,levelPartList.Count)];
        Transform lastLevelPart = SpawnLevelPart(chosenLevelPart,lastEndPoint);
        lastEndPoint = lastLevelPart.Find("EndPoint").position;
    }
    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform =  Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;

    }
}
