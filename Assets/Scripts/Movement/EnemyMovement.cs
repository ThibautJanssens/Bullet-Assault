using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Transform player;
    private Transform parentTransform;
    private Health parentHealth;
    public float sightRange;
    public float sightCheckTime;

    public float movementSpeed;

    public bool isSeeingEnemy { get; set; }
    private bool facingRight = false;
    private float fallYCoord;

    private void OnEnable() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        parentTransform = GetComponentInParent(typeof(Transform)) as Transform;
        parentHealth = GetComponentInParent(typeof(Health)) as Health;
        fallYCoord = LevelManager.Instance.GetCleanerYHeight();
        StartCoroutine(CheckSightCoroutine());
    }

    private void OnDisable() {
        StopCoroutine(CheckSightCoroutine());
    }

    private void Update(){
        if (parentTransform.position.y < fallYCoord) {
            parentHealth.Die();
        } else if(isSeeingEnemy) {
            Vector3 playerPosition = player.position;
            Vector3 enemyPosition = transform.position;
            float differenceOfX = playerPosition.x - enemyPosition.x;
            if (differenceOfX > 0 && !facingRight) {
                Flip();
            } else if(differenceOfX < 0 && facingRight) {
                Flip();
            }
            transform.position = Vector3.MoveTowards(enemyPosition, playerPosition, (movementSpeed * Time.deltaTime));
            // Bugfix : X axis from rotation was changing when using MoveTowards
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        // Get the pistol
        Transform pistol = transform.GetChild(1).transform;
        // Invert the x of the localScale
        Vector3 localScale = pistol.localScale;
        localScale.x *= -1;
        pistol.localScale = localScale;
        // Invert the x of the position
        Vector3 position = pistol.localPosition;
        position.x *= -1;
        pistol.localPosition = position;
    }

    private bool IsPlayerInSight() {
        return Vector3.Distance(player.position, transform.position) <= sightRange;
    }

    private IEnumerator CheckSightCoroutine() {
        while(true) {
            if (IsPlayerInSight()) {
                isSeeingEnemy = true;
            } else {
                isSeeingEnemy = false;
            }
            yield return new WaitForSeconds(sightCheckTime);
        }
    }

}
