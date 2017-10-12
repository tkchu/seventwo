﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public GameObject flamePrefab;

    Map map;

    bool noDie = true;
    public bool isReady = false;

    void Start() {
        map = FindObjectOfType<Map>();
    }

    public void OneAction() {
        if (isReady) {
            Die();
        }

        GridItem player = FindObjectOfType<Player>().GetComponent<GridItem>();
        GridItem self = GetComponent<GridItem>();

        int[] player_pos = map.GetPlayerPos();
        int[] self_pos = map.FindGameObject(map.itemMap, gameObject);
        if (player_pos == null || self_pos == null)
            return;

        if (Mathf.Abs(player_pos[0] - self_pos[0]) + Mathf.Abs(player_pos[1] - self_pos[1]) <= 2) {
            GetComponent<Animator>().SetBool("isReady", true);
            isReady = true;
        } else {
            GetComponent<Animator>().SetBool("isReady", false);
        }

    }

    int[] pos;
    public void Update() {
        int[] temp = map.FindGameObject(map.itemMap, gameObject);
        if (temp != null)
            pos = temp;
    }

    public void Die() {
        //保证不会多次标记死亡
        if (!noDie) {
            return;
        }else {
            noDie = false;
        }

        FindObjectOfType<SoundManager>().Play("boom");

        GameObject[] around = new GameObject[] {
            map.GetGameObjectAt(pos[0] - 1, pos[1]),
            map.GetGameObjectAt(pos[0] + 1, pos[1]),
            map.GetGameObjectAt(pos[0], pos[1] + 1),
            map.GetGameObjectAt(pos[0], pos[1] - 1),
            map.GetGameObjectAt(pos[0], pos[1]),
        };

        foreach (GameObject item in around) {
            if(item!=null && item.tag == "enemy") {
                item.GetComponent<Enemy>().OneShot();
            }else if(item != null && item.tag == "Player") {
                item.GetComponent<Player>().Meet(gameObject);
            }if(item!=null && item.tag == "boss") {
                item.GetComponent<BossPart>().OneHit();
            }
        }

        // 生成四面火焰
        Vector2[] aroundVectors = new Vector2[] {
            Vector2.left,Vector2.right, Vector2.up, Vector2.down, Vector2.zero
        };
        foreach (Vector2 vec in aroundVectors) {
            GameObject item = map.GetGameObjectAt(pos[0] + (int)vec.x, pos[1] + (int)vec.y);
            if (item == null || item.tag != "wall") {
                Instantiate(flamePrefab, transform.position + new Vector3(vec.x, vec.y, -10) * 0.6f, Quaternion.identity);
            }
        }
        FindObjectOfType<Map>().RemoveGameObject(gameObject);

        Destroy(gameObject);
    }

}