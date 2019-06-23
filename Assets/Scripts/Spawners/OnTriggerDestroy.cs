using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDestroy : MonoBehaviour {
    [SerializeField]
    private bool destroySelf = false;
    [SerializeField]
    private bool destroyOther = false;

    private void OnTriggerEnter(Collider other) {
        Rigidbody rb = other.attachedRigidbody;
        GameObject obj = rb ? rb.gameObject : other.gameObject;

        if(gameObject.tag != other.tag) {
            if (destroySelf) {
                Poolable.Destroy(gameObject);
            }
            if (destroyOther) {
                Poolable.Destroy(obj);
            }
        }

        
    }
}
