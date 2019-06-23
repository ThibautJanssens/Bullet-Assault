using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : WeaponController {
    public Weapon weapon;
    [SerializeField]
    private Weapon[] weapList;

    private void Start() {
        weapon = weapList[WeaponSwitching.currentWeap];
        for(int i = 0; i < weapList.Length; i++) {
            weapList[i].currentAmmo.Value = weapList[i].maxAmmoClip.Value;
            weapList[i].totalAmmoLeft.Value = weapList[i].totalMaxAmmoCarried.Value;
        }
    }

    private void Update() {
        weapon = weapList[WeaponSwitching.currentWeap];

        if (Input.GetButtonDown("Fire1")) {
            StartShooting(weapon);
        }

        if (Input.GetButtonUp("Fire1") && !Input.GetButton("Fire1")) {
            StopShooting();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            if(weapon.currentAmmo.Value < weapon.maxAmmoClip.Value)
                weapon.Reload();
        }
    }
}