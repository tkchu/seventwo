﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
    private float gridSize;
    private GridWorld gw;
    private Camera cameraFollow;

    private void Start() {
        gw = FindObjectOfType<GridWorld>();
        gridSize = gw.gridSize;
        cameraFollow = FindObjectOfType<Camera>();
        cameraFollow.transform.position = new Vector3(transform.position.x, transform.position.y, cameraFollow.transform.position.z);
    }

    public void Go(Vector2 direction) {
        int[] moveMult = GetComponent<Weapon>().Shoot(direction);
        int shot = moveMult[0];
        int backForce = moveMult[1];

        bool haveMove;
        int pos_x = gw.GridItem_x(GetComponent<GridItem>());
        int pos_y = gw.GridItem_y(GetComponent<GridItem>());
        int move = 1;
        if (shot == 1) {
            move = backForce;
        }
        GridItem[] back = gw.FindGridItemInRange(pos_x, pos_y, direction * (backForce < 0 ? -1 : 1), Mathf.Abs(move));
        if (back.Length == 0) {
            haveMove = false;
        } else {
            //TODO:检测碰撞到的敌人
            transform.position = gw.Go(GetComponent<GridItem>(), direction * back.Length);
            haveMove = true;
        }

        if (haveMove || shot == 1) {
            Debug.Log("OneAction");
            GridItem[] items = FindObjectsOfType<GridItem>();
            foreach (GridItem item in items) {
                item.gameObject.SendMessage("OneAction");
            }
            gw.Flush();
        }

        cameraFollow.transform.position = new Vector3(transform.position.x, transform.position.y, cameraFollow.transform.position.z);
    }
}