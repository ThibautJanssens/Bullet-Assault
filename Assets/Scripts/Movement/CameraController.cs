using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private GameObject lookAt;

    private Vector3 offset = new Vector3(0,0,-6.5f);
   	
	// fonctionne comme update mais avec la frame précédente
	void LateUpdate () {
        //faire genre que la camera a le comportement d'un enfant du joueur
        transform.position = lookAt.transform.position + offset;
	}
}
