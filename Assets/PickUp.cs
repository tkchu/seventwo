﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public Guns haveGun;

    public void Meet(GridItem item) {
        if (item.gridItemType == GridItemType.player) {
            FindObjectOfType<SoundManager>().Play("pickup");
            Destroy(gameObject);
        }
    }
}