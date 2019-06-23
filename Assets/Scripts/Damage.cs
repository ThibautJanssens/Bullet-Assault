using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public int damage;

    private void OnTriggerEnter(Collider col) {
        GameObject obj;
        Rigidbody rb = col.attachedRigidbody;
        if (!rb)
            obj = col.gameObject;
        else
            obj = rb.gameObject;

        if (!obj.tag.Equals(gameObject.tag)) {
            Health health = obj.GetComponent<Health>();
            if (health) {
                health.TakeDamage(damage);

            }
        }        
    }
}