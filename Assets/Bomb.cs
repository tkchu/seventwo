﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public GameObject flamePrefab;

    GridWorld gw;

    bool noDie = true;
    public bool isReady = false;

    void Start() {
        gw = FindObjectOfType<GridWorld>();
    }

    public void OneAction() {
        if (isReady) {
            Die();
        }

        GridItem player = FindObjectOfType<Player>().GetComponent<GridItem>();
        GridItem self = GetComponent<GridItem>();

        int player_x = gw.GridItem_x(player);
        int player_y = gw.GridItem_y(player);
        int self_x = gw.GridItem_x(self);
        int self_y = gw.GridItem_y(self);

        if (Mathf.Abs(player_x - self_x) + Mathf.Abs(player_y - self_y) <= 2) {
            GetComponent<Animator>().SetBool("isReady", true);
            isReady = true;
        } else {
            GetComponent<Animator>().SetBool("isReady", false);
        }

    }

    public void Die() {
        //保证不会多次标记死亡
        if (!noDie) {
            return;
        }else {
            noDie = false;
        }

        FindObjectOfType<SoundManager>().Play("boom");

        int x = gw.GridItem_x(GetComponent<GridItem>());
        int y = gw.GridItem_y(GetComponent<GridItem>());
        GridItem[] around = new GridItem[] {
            gw.GridItemAt(x - 1, y),
            gw.GridItemAt(x + 1, y),
            gw.GridItemAt(x, y+1),
            gw.GridItemAt(x, y-1),
            gw.GridItemAt(x, y),
        };

        foreach (GridItem item in around) {
            if(item!=null && item.gridItemType == GridItemType.enemy) {
                item.GetComponent<Enemy>().OneShot();
            }else if(item != null && item.gridItemType == GridItemType.player) {
                item.GetComponent<Player>().Meet(GetComponent<GridItem>());
            }if(item!=null && item.gridItemType == GridItemType.boss) {
                item.GetComponent<BossPart>().OneHit();
            }
        }

        // 生成四面火焰
        Vector2[] aroundVectors = new Vector2[] {
            Vector2.left,Vector2.right, Vector2.up, Vector2.down, Vector2.zero
        };
        foreach (Vector2 vec in aroundVectors) {
            GridItem item = gw.GridItemAt(x + (int)vec.x, y + (int)vec.y);
            if (item == null || item.gridItemType != GridItemType.wall) {
                Instantiate(flamePrefab, transform.position + new Vector3(vec.x, vec.y, -10) * gw.gridSize, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }

}