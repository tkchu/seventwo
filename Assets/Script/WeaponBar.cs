﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBar : MonoBehaviour {
    Weapon weapon;
    public OneWeaponBar red;
    public OneWeaponBar blue;
    public OneWeaponBar green;

    private void Start() {
        weapon = FindObjectOfType<Weapon>();
        red.gameObject.SetActive(false);
        blue.gameObject.SetActive(false);
        green.gameObject.SetActive(false);
    }

    private void Update() {
        /*
        if (weapon.gunHave.Contains(Guns.pistol)) {
            red.gameObject.SetActive(true);
        }
        if (weapon.gunHave.Contains(Guns.shotgun)) {
            blue.gameObject.SetActive(true);
        }
        if (weapon.gunHave.Contains(Guns.jumpgun)) {
            green.gameObject.SetActive(true);
        }
        */

        if(weapon.gunNow == Guns.pistol) {
            red.Select();
            red.Show(weapon.loadCount / (float)weapon.loadTime);
            green.UnSelect();
            blue.UnSelect();
        }
        if (weapon.gunNow == Guns.shotgun) {
            red.UnSelect();
            blue.Select();
            blue.Show(weapon.loadCount / (float)weapon.loadTime);
            green.UnSelect();
        }
        if (weapon.gunNow == Guns.jumpgun) {
            red.UnSelect();
            blue.UnSelect();
            green.Select();
            green.Show(weapon.loadCount / (float)weapon.loadTime);
        }

    }

}