﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridItemType {
    enemy,
    boss,
    player,
    wall,
    pickup,
}

public class GridItem : MonoBehaviour {
    public GridItemType gridItemType;
    void Start() {
        GridWorld gw = FindObjectOfType<GridWorld>();
        int x = gw.GridItem_x(GetComponent<GridItem>());
        int y = gw.GridItem_y(GetComponent<GridItem>());
    }

    public void Go(Vector2 direction) {
        GridWorld gw = FindObjectOfType<GridWorld>();
        gw.Go(this, direction);
    }

    public void OneAction() {

    }

    public void Meet(GridItem itemMeet) {

    }
}