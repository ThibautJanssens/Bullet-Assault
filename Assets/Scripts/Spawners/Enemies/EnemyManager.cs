using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyManager : ScriptableObject {

    public ListenableInt remainingEnemies;
    public ListenableInt enemiesByZone;
    private EnemySpawner enemySpawner = null;

    private void OnEnable() {
        InitialiseRemainingEnemies();
    }

    public void InitialiseRemainingEnemies () {
        remainingEnemies.Value = enemiesByZone.Value;
    }

    public void DecrementRemainingEnemies() {
        remainingEnemies.Value--;
        // Initialise the enemySpawner the first time
        if (enemySpawner == null) {
            GameObject gameObject = GameObject.FindGameObjectWithTag("EnemySpawner");
            enemySpawner = (EnemySpawner) gameObject.GetComponent(typeof(EnemySpawner));
        }
        if (remainingEnemies.Value <= 0){
            if (enemySpawner.NextZone()) {
                Debug.Log("Next zone");
                InitialiseRemainingEnemies();
                enemySpawner.StartSpawn();
            } else {
                LevelManager.Instance.Win();
                Debug.Log("No more zones");
            }
        }
    }
}
