using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject projectile;

    public ListenableInt maxAmmoClip;
    public ListenableInt currentAmmo;
    public ListenableFloat reloadTime;
    public ListenableFloat fireInterval;
    public ListenableInt totalAmmoLeft;
    public ListenableInt totalMaxAmmoCarried;
    public AudioSource reloadSound;
    public AudioSource shootingSound;

    public void Fire() {
        shootingSound.Play();
        GameObject obj = Poolable.Instantiate(projectile, transform.position, transform.rotation);
        obj.GetComponent<DirectionnalMovement>().direction = DirectionCalc();
        foreach (Transform item in obj.GetComponentsInChildren<Transform>()) {
            item.gameObject.tag = gameObject.tag;
        }
    }

    public void Reload() {
        reloadSound.Play();
        if(totalAmmoLeft.Value <= 0) {
            return;
        }
        else if (totalAmmoLeft.Value < maxAmmoClip.Value) {
            currentAmmo.Value = totalAmmoLeft.Value;
            totalAmmoLeft.Value = 0;
        }
        else {
            totalAmmoLeft.Value -= (maxAmmoClip.Value - currentAmmo.Value);
            currentAmmo.Value = maxAmmoClip.Value;
        }
    }

    public void PickUp() {
        if(totalAmmoLeft.Value + maxAmmoClip.Value >= totalMaxAmmoCarried.Value) {
            totalAmmoLeft.Value = totalMaxAmmoCarried.Value;
        }
        else {
            totalAmmoLeft.Value += maxAmmoClip.Value;
        }
    }

    private Vector3 DirectionCalc() {
        Player player = GameObject.FindObjectOfType(typeof(Player)) as Player;

        if (gameObject.tag.Equals("Player")) {
        //en fonction de sa direction change la direction des balles
            if (!player.IsFacingRight()) {
                return new Vector3(-1, 0, 0);
            }
            else {
                return new Vector3(1, 0, 0);
             }
        }
        else {
            if (player.transform.position.x > transform.position.x) {
                return new Vector3(1, 0, 0);
            }
            else {
                return new Vector3(-1, 0, 0);
            }
        }
    }
}