﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //先移动
        if (move == 0) {
            haveMove = false;
        } else {
            transform.position = gw.Go(GetComponent<GridItem>(), direction * Mathf.Abs(move) * (move < 0 ? -1 : 1));
            haveMove = true;
        }
        //再触发敌人死亡
        if (shot == 1) {
            GridItem[] face = gw.FindGridItemInRange(pos_x, pos_y, direction, GetComponent<Weapon>().range);

            foreach (GridItem item in face) {
                if (item != null &&
                    (item.gridItemType == GridItemType.enemy || item.gridItemType == GridItemType.boss)) {
                    item.GetComponent<Enemy>().OneShot();
                }
            }
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
            StartCoroutine(Restart());
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

    IEnumerator Restart() {
        yield return new WaitForSeconds(0.5f);

        if (SceneManager.GetActiveScene().name == "BossLevel") {
            SceneManager.LoadScene("BossLevel");
        }else {
            FindObjectOfType<Reseter>().reset = true;
        }
    }

}