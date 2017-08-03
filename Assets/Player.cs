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
        GridItem[] back = gw.FindGridItemInRange(pos_x, pos_y, direction * (move < 0 ? -1 : 1), Mathf.Abs(move));
        if (back.Length == 0) {
            haveMove = false;
        } else {
            //TODO:检测碰撞到的敌人
            transform.position = gw.Go(GetComponent<GridItem>(), direction * back.Length * (move < 0 ? -1 : 1));
            haveMove = true;
        }

        if (haveMove || shot == 1) {
            GridItem[] items = FindObjectsOfType<GridItem>();
            foreach (GridItem item in items) {
                item.gameObject.SendMessage("OneAction");
            }
        }

        //cameraFollow.transform.position = new Vector3(transform.position.x, transform.position.y, cameraFollow.transform.position.z);
    }


    public void Meet(GridItem item) {
        if(item.gridItemType == GridItemType.enemy) {
            Debug.Log("Game OVer", item);
        }
        if(item.gridItemType == GridItemType.pickup) {
            GetComponent<Weapon>().PickGun(item.GetComponent<PickUp>().haveGun);
        }
    }
}