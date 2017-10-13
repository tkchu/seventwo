﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public Guns haveGun;

    public void Meet(GameObject item) {
        item.GetComponent<Weapon>().PickGun(haveGun);
        FindObjectOfType<SoundManager>().Play("pickup");
        Destroy(gameObject);
    }
}