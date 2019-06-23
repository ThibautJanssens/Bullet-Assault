using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drop Table")]
public class DropTable : ScriptableObject {

    public List<DropTableItem> drops;
    public int totalWeight;

    private void OnEnable() {
        totalWeight = 0;
        foreach(var item in drops){
            totalWeight += item.weight;
        }
    }

    public GameObject GetItem() {
        int roll = Random.Range(0, totalWeight) + 1; //+1 car quand tous les objets à 0, ne renvoie rien (choix d'implémentation)
        int cursor = 0;
        foreach(var item in drops){
            cursor += item.weight;
            if(cursor >= roll){
                return item.item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class DropTableItem {

    public int weight;
    public GameObject item;
}