using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawn : MonoBehaviour {
    [SerializeField]
    private GameObject prefab;

    private void OnEnable() {
        Poolable.Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
    }
}
