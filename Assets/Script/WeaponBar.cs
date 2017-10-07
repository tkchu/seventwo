﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBar : MonoBehaviour {
    public Sprite[] weaponBar;
    private void Update() {
        Weapon weapon = FindObjectOfType<Weapon>();

        if (weapon == null) {
            GetComponent<Image>().enabled = false;
        } else if (weapon.gunNow == Guns.pistol) {
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = weaponBar[0];
        }
        else if (weapon.gunNow == Guns.shotgun) {
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = weaponBar[1];

        } else if (weapon.gunNow == Guns.jumpgun) {
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = weaponBar[2];
        } else {
            GetComponent<Image>().enabled = false;
        }

    }

}