﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    GridWorld gw;
    void Start() {
        gw = FindObjectOfType<GridWorld>();
    }

    public void OneShot() {
        int x = gw.GridItem_x(GetComponent<GridItem>());
        int y = gw.GridItem_y(GetComponent<GridItem>());
        GridItem[] around = new GridItem[] {
            gw.GridItemAt(x - 1, y),
            gw.GridItemAt(x + 1, y),
            gw.GridItemAt(x, y+1),
            gw.GridItemAt(x, y-1),
        };
        foreach(GridItem item in around) {
            if(item!=null && item.gridItemType == GridItemType.enemy) {
                item.GetComponent<Enemy>().OneShot();
            }else if(item != null && item.gridItemType == GridItemType.player) {
                item.GetComponent<Player>().Meet(GetComponent<GridItem>());
            }
        }
    }
}