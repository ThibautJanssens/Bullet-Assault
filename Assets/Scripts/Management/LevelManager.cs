using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { set; get; }

    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private Transform playerTransform;
    public GameObject teleporter;
    private GameObject cleaner;
    private float cleanerYHeight;

    //private int playerLife = 100;
    //private int score = 0;

    private void Awake() {
        Instance = this;
        GetCleanerHeight();
    }

    private void Start() {
        //teleporte le joueur sur la position du spawnLocation
        playerTransform.position = spawnPosition.position;
        teleporter = GameObject.FindGameObjectWithTag("Teleport");
        teleporter.SetActive(false);
    }

    private void GetCleanerHeight() {
        cleaner = GameObject.FindGameObjectWithTag("Cleaner");
        Collider collider = cleaner.GetComponent<Collider>();
        cleanerYHeight = collider.transform.localPosition.y + collider.transform.localScale.y + playerTransform.localScale.x;
    }

    public void Win() {
        /* if(PlayerPrefs.GetInt("PlayerScore") < score) {
             PlayerPrefs.SetInt("PlayerScore", score);
         }*/

        //PlayerPrefs.SetInt("PlayerLife", playerLife);
        teleporter.SetActive(true);
    }

    public float GetCleanerYHeight() {
        return cleanerYHeight;
    }

 
}
