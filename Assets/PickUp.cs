﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public Guns haveGun;

    public void Meet(GridItem item) {
        Debug.Log("Meet");

        if (item.gridItemType == GridItemType.player) {
            Destroy(gameObject);
        }
    }
}