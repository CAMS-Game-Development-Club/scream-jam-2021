using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Pathfinding;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int xmin = -20, xmax = 20, ymin = -20, ymax = 20;
    [SerializeField] private float spawnInterval = 3.0f; // Number of seconds between each enemy spawn
    [SerializeField] private GameObject enemyHolder;
    [SerializeField] private RectTransform targetTransform;

    private void Start() {
        InvokeRepeating("SpawnEnemy", 0.0f, spawnInterval);
    }

    private void SpawnEnemy() { // Spawn an enemy within a random given range
        GameObject newEnemy = Instantiate(enemyPrefab, GetRandomPosition(xmin, xmax, ymin, ymax), Quaternion.identity);
        newEnemy.transform.SetParent(enemyHolder.transform);
        newEnemy.GetComponent<AIDestinationSetter>().target = targetTransform;
    }

    private Vector3 GetRandomPosition(float xmin, float xmax, float ymin, float ymax) {
        return new Vector3(Random.Range(xmin, xmax), Random.Range(ymin, ymax), 0);
    }

}
