using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour {
    public static int currentWeap = 0;
    public Text currentAmmo;
    public Text carriedAmmo;
    public Weapon currentEquipedWeapon;

    // Use this for initialization
    void OnEnable () {
        SelectWeapon();
	}

	// Update is called once per frame
	void Update () {
        int previousWeapon = currentWeap;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if (currentWeap >= transform.childCount - 1) {
                currentWeap = 0;
            }
            else {
                currentWeap++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            if (currentWeap <= 0) {
                currentWeap = transform.childCount - 1;
            }
            else {
                currentWeap--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentWeap = 0; //pistol
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 3) {
            currentWeap = 1; //shotgun
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3) {
            currentWeap = 2; //m4
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 3) {
            currentWeap = 3; //rpg
        }

        if (previousWeapon != currentWeap) {
            SelectWeapon();
        }
	}

    void SelectWeapon() {
        int i = 0;
        foreach (Transform weapon in transform) {
            if(i == currentWeap) {
                weapon.gameObject.SetActive(true);
                currentAmmo.GetComponent<TextUpdater>().SetValue(weapon.gameObject.GetComponent<Weapon>().currentAmmo);
                carriedAmmo.GetComponent<TextUpdater>().SetValue(weapon.gameObject.GetComponent<Weapon>().totalAmmoLeft);
                currentEquipedWeapon = weapon.gameObject.GetComponent<Weapon>();
            }
            else {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
