﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public Guns haveGun;

    public void Meet(GameObject item) {
        StartCoroutine(PickWeapon());
    }
    IEnumerator PickWeapon() {
        yield return new WaitWhile(() => FindObjectOfType<Player>().GetComponent<Animator>().GetBool("isShoot"));
        FindObjectOfType<Weapon>().PickGun(haveGun);
        FindObjectOfType<SoundManager>().Play("pickup");
        Destroy(gameObject);
        yield return new WaitForEndOfFrame();
    }
}