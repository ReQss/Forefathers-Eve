using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject ghostPrefab;
    public List<Transform> spawnPosition;
    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPosition.Count);
        Instantiate(ghostPrefab, spawnPosition[randomIndex].position, Quaternion.identity);
    }
}
