﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridItemType {
    enemy,
    boss,
    player,
    wall,
}

public class GridItem : MonoBehaviour {
    public GridItemType gridItemType;
    void Start() {
        GridWorld gw = FindObjectOfType<GridWorld>();
        int x = gw.GridItem_x(GetComponent<GridItem>());
        int y = gw.GridItem_y(GetComponent<GridItem>());

        Debug.Log(new Vector2(x, y), this);
    }

    public void Go(Vector2 direction) {
        GridWorld gw = FindObjectOfType<GridWorld>();
        gw.Go(this, direction);
    }

    public void OneAction() {

    }
}