using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform waypoint;
    public GameObject endText;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerTransform.position = waypoint.position;
            endText.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}
