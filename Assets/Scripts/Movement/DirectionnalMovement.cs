using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionnalMovement : MonoBehaviour {
    [SerializeField]
    private int speed;

    public Vector3 direction;

    private void Update() {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}