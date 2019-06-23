using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float minSpawnInterval;
    public float maxSpawnInterval;
    public GameObject enemyPrefab;

    public ListenableInt enemiesByZone;
    private int enemiesToSpawn;

    private Queue<SpawnZone> spawnZones;
    private SpawnZone currentSpawnZone;

    private void Awake() {
        spawnZones = GetEnemiesSpawnZoneAndSetCurrentSpawnZone();
        ResetEnnemiesToSpawn();
    }

    private void OnEnable() {
        StartSpawn();
    }

    private void OnDisable() {
        StopCoroutine(SpawnCoroutine());
    }

    public bool NextZone() {
        if(spawnZones.Count > 0) {
            currentSpawnZone = spawnZones.Dequeue();
            return true;
        } else {
            return false;
        }
    }

    private void ResetEnnemiesToSpawn() {
        enemiesToSpawn = enemiesByZone.Value;
    }

    private Queue<SpawnZone> GetEnemiesSpawnZoneAndSetCurrentSpawnZone() {
        GameObject[] enemiesSpawnZones = GameObject.FindGameObjectsWithTag("EnemySpawn");
        if(enemiesSpawnZones.Length == 0) {
            Debug.Log("No enemySpawnZone");
            return null;
        }
        System.Array.Sort(enemiesSpawnZones, CompareObNames);
        Queue<SpawnZone> queue = new Queue<SpawnZone>(enemiesSpawnZones.Length);

        // For each spawn zone : get the beginning and the ending of the zone, put it in an SpawnZone object and add it to the queue
        foreach (GameObject EnemySpawnZone in enemiesSpawnZones) {
            SpawnZone spawnZone = (SpawnZone) SpawnZone.CreateInstance("SpawnZone");
            Vector3 beginningPosition = EnemySpawnZone.transform.localPosition;
            spawnZone.beginning = new Vector2(beginningPosition.x, beginningPosition.y);
            Vector3 zoneDimensions = EnemySpawnZone.transform.localScale;
            spawnZone.ending = new Vector2(beginningPosition.x + zoneDimensions.x, beginningPosition.y + zoneDimensions.y);
            queue.Enqueue(spawnZone);
        }

        currentSpawnZone = queue.Dequeue();
        return queue;
    }

    private int CompareObNames(GameObject x, GameObject y) {
        return x.name.CompareTo(y.name);
    }

    private Vector3 GetRandomPositionInZone() {
        return new Vector3(Random.Range(currentSpawnZone.beginning.x, currentSpawnZone.ending.x), Random.Range(currentSpawnZone.beginning.y, currentSpawnZone.ending.y));
    }

    private GameObject Spawn () {
        enemiesToSpawn--;
        return Poolable.Instantiate(enemyPrefab, GetRandomPositionInZone(), transform.rotation);
    }
	
    public void StartSpawn() {
        ResetEnnemiesToSpawn();
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine() {
        while (true) {
            // If there are still some ennemies to spawn
            if (enemiesToSpawn > 0) {
                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
                Spawn();
            } else {
                Debug.Log("All enemies spawned in the zone");
                yield break;
            } 
        }
    }

}