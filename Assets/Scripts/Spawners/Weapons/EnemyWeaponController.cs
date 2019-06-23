using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : WeaponController {

    public Weapon weapon;
    public ListenableFloat fireInterval;
    private EnemyMovement enemyMovement;

    private void OnEnable() {
        enemyMovement = GetComponentInParent(typeof(EnemyMovement)) as EnemyMovement;
        // To prevent the shoot of a bullet when the enemy spawns
        enemyMovement.isSeeingEnemy = false;
        StartCoroutine(FireCoroutine());
    }

    private void OnDisable() {
        StopCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine() {
        while (true) {
            if (enemyMovement.isSeeingEnemy) {
                ShootOnce(weapon);
            }
            yield return new WaitForSeconds(fireInterval.Value);
        }
        
    }
}
