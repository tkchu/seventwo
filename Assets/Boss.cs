﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public int hp = 3;

    public GridWorld gw;
    public int Ylength;
    public int Xlength;

    public GameObject bomb;
    public GameObject spine;
    public GameObject moveBomb;
    public GameObject knfieEnemy;

    private void Start() {
        gw = FindObjectOfType<GridWorld>();
        Ylength = gw.allItems.Length;
        Xlength = gw.allItems[0].Length;
    }

    public void OneAction() {
        int x = Random.Range(1, Xlength - 1);
        int y = Random.Range(1, Ylength - 1);
        GridItem item = gw.GridItemAt(x, y);
        while (item != null) {
            x = Random.Range(1, Xlength - 1);
            y = Random.Range(1, Ylength - 1);
            item = gw.GridItemAt(x, y);
        }
        float pos_x = -4.2f + x*gw.gridSize;
        float pos_y = -1.2f + y*gw.gridSize;
        Instantiate(bomb, new Vector3(pos_x, pos_y, bomb.transform.position.z), Quaternion.identity, transform);
    }

    public void OneHit() {
        Debug.Log("hp--");
        hp -= 1;
        if (hp <= 0) {
            Debug.Log("Winner!");
        }
    }
}