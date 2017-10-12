﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBar : MonoBehaviour {
    public Sprite[] weaponBar;
    public void PickGun(Weapon weapon) {
        StartCoroutine(Show(weapon.gunNow));

    }

    IEnumerator Show(Guns gun) {
        float alpha = 1f;
        if (gun == Guns.pistol) {
            GetComponent<Image>().sprite = weaponBar[0];
        } else if (gun == Guns.shotgun) {
            GetComponent<Image>().sprite = weaponBar[1];
        } else if (gun == Guns.jumpgun) {
            GetComponent<Image>().sprite = weaponBar[2];
        }
        Color oldColor = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
        yield return new WaitForSeconds(1f);
        while (alpha >= 0f) {
            GetComponent<Image>().color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
            yield return new WaitForSeconds(0.1f);
            alpha -= 0.1f;
        }
    }

}