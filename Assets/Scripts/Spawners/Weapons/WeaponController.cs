using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
   private bool isShooting = false;
    private Coroutine fireCR;

    public void StartShooting(Weapon weap) {
        StopShooting();
        isShooting = true;
        fireCR = StartCoroutine(FireCoroutine(weap));
    }

    public void StopShooting() {
        isShooting = false;
        if (fireCR != null) {
            StopCoroutine(fireCR);
        }
    }

    public void ShootOnce(Weapon weap) {
        StartShooting(weap);
        StopShooting();
    }

    private IEnumerator FireCoroutine(Weapon weap) {
        while (isShooting) {
            if(gameObject.tag == "Player") {
                if (weap.currentAmmo.Value > 0) {
                    weap.currentAmmo.Value--;
                    weap.Fire();
                    yield return new WaitForSeconds(weap.fireInterval.Value);
                }
                else {
                    weap.Reload();
                    yield return new WaitForSeconds(weap.reloadTime.Value);
                }
            }
            else {
                weap.Fire();
                yield return new WaitForSeconds(weap.fireInterval.Value);
            }
        }
    }
}