using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Behaviours/DestroyBehaviour")]
public class DestroyBehaviour : ObjectBehaviour {
    public override void Execute(GameObject obj) {
        Poolable.Destroy(obj);
    }
}