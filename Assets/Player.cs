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

        move = back.Length * (move < 0 ? -1 : 1);
        for (int i = 0; i < back.Length; i++) {
            if(back[i] != null && back[i].gridItemType == GridItemType.boss) {
                move = i * (move < 0 ? -1 : 1);
                break;
            }
        }

        if (move == 0) {
            haveMove = false;
        } else {
            transform.position = gw.Go(GetComponent<GridItem>(), direction * Mathf.Abs(move) * (move < 0 ? -1 : 1));
            haveMove = true;
        }

        if (shot == 1 && move < 0) {
            GetComponent<Animator>().SetBool("isAttacking", true);
        }

        if(shot == 0 && haveMove) {
            FindObjectOfType<SoundManager>().Play("move");
        }

        if (haveMove || shot == 1) {
            if (Mathf.Abs(direction.x) >0f) {
                GetComponent<SpriteRenderer>().flipX = direction.x > 0;
            }

            Enemy[] items = FindObjectsOfType<Enemy>();
            foreach (Enemy item in items) {
                if(item.GetComponent<knifeEnemy>()) {
                    item.OneAction();
                }
            }
            foreach (Enemy item in items) {
                if (item.GetComponent<Bomb>() != null) {
                    item.OneAction();
                }
            }

            if (FindObjectOfType<Boss>()) {
                FindObjectOfType<Boss>().OneAction();
            }
            GetComponent<Weapon>().OneAction();
        }
    }


    public void Meet(GridItem item) {
        if(item.gridItemType == GridItemType.enemy || item.gridItemType == GridItemType.spine) {
            GetComponent<Animator>().SetBool("isDead", true);
            Debug.Log("Game OVer", item);
        }

        if (item.GetComponent<knifeEnemy>() != null) {
            FindObjectOfType<SoundManager>().Play("dieKnife");
        }else if(item.gridItemType == GridItemType.spine) {
            FindObjectOfType<SoundManager>().Play("dieSpine");
        }

        if (item.gridItemType == GridItemType.pickup) {
            GetComponent<Weapon>().PickGun(item.GetComponent<PickUp>().haveGun);
        }
    }
}