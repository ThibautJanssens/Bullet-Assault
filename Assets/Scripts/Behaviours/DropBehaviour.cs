using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviours/Drop Behaviour")]
public class DropBehaviour : ObjectBehaviour {

    public DropTable dropTable;

    public override void Execute(GameObject obj) {

        GameObject drop = dropTable.GetItem();
        try {
            Poolable.Instantiate(drop, obj.transform.position, Quaternion.identity);
        }
        catch (System.NullReferenceException) {
        }
    }
}
