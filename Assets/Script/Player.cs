﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    Map map;
    void Start() {
        map = FindObjectOfType<Map>();
    }

    public void Go(int[] direction) {
        int[] moveMult = GetComponent<Weapon>().Shoot(direction);
        int shot = moveMult[0];//是否打中了
        int backForce = moveMult[1];

        bool haveMove;
        int[] pos = map.FindGameObject(map.itemMap, gameObject);

        int move = 1;//移动的乘值，有后坐力为负
        if (shot == 1) {
            move = backForce;
        }

        GameObject[] back = map.FindGridItemInRange(pos,
            new int[]{
                direction[0] * (move < 0 ? -1 : 1),
                direction[1] * (move < 0 ? -1 : 1),
            },
            Mathf.Abs(move));

        move = back.Length * (move < 0 ? -1 : 1);
        for (int i = 0; i < back.Length; i++) {
            if(back[i] != null && back[i].tag == "boss") {
                move = i * (move < 0 ? -1 : 1);
                break;
            }
        }

        //移动
        if (move == 0) {
            haveMove = false;
        } else {
            GameObject itemMeet = map.MoveItem(gameObject,
                new int[]{
                    pos[0] + direction[0] * Mathf.Abs(move) * (move < 0 ? -1 : 1),
                    pos[1] +  direction[1] * Mathf.Abs(move) * (move < 0 ? -1 : 1)
                });
            if (itemMeet != null && itemMeet.tag == "teleporter")
                return;
            haveMove = true;
        }


        if(shot == 0 && haveMove) {
            FindObjectOfType<SoundManager>().Play("move");
        }

        if (haveMove || shot == 1) {
            if (Mathf.Abs(direction[0]) >0f) {
                GetComponent<SpriteRenderer>().flipX = direction[0] > 0;
            }

            Enemy[] items = FindObjectsOfType<Enemy>();
            foreach (Enemy item in items) {
                item.OneAction();
            }
            /*
            if (FindObjectOfType<Boss>()) {
                FindObjectOfType<Boss>().OneAction();
            }*/
        }

    }


    public Color redGun;
    public Color blueGun;
    public Color greenGun;
    public GameObject pointPrefab;
    public void CreateAimPoint() {
        GameObject[] pointBefores = GameObject.FindGameObjectsWithTag("aimPoint");
        foreach(GameObject pointBefore in pointBefores) {
            Destroy(pointBefore);
        }
        // 设置指示点
        int[] pos = map.GetPlayerPos();
        if (pos == null)
            return;

        int[][] allDirections = new int[][] {
            new int[]{-1,0},
            new int[]{1,0},
            new int[]{0,1},
            new int[]{0,-1},
         };

        Color now_color = Color.white;
        Guns now_gun = GetComponent<Weapon>().gunNow;
        if(now_gun == Guns.pistol) {
            now_color = redGun;
        }else if(now_gun == Guns.shotgun) {
            now_color = blueGun;
        }else if(now_gun == Guns.jumpgun) {
            now_color = greenGun;
        }

        foreach(int[] direction in allDirections) {
            GameObject[] around = map.FindGridItemInRange(pos, direction, GetComponent<Weapon>().range);
            int i = around.Length;
            while (i > 0) {
                GameObject g = Instantiate(pointPrefab, transform.position + new Vector3(0, 0.1f, 0) + i* 0.6f*(new Vector3(direction[0], direction[1],-1)), Quaternion.identity);
                g.GetComponent<SpriteRenderer>().color = now_color;
                i -= 1;
            }
        }
    }

    private void Update() {
        CreateAimPoint();
    }

    public void Meet(GameObject item) {
        if(item.GetComponent<knifeEnemy>() && !item.GetComponent<knifeEnemy>().noDie) {
            return;
        }
        if (item.GetComponent<Lame>() && !item.GetComponent<Lame>().noDie) {
            return;
        }
        if (item.GetComponent<Diagonal>() && !item.GetComponent<Diagonal>().noDie) {
            return;
        }

        if (item.tag == "enemy" || item.tag == "spine") {
            GetComponent<Animator>().SetBool("isDead", true);
            StartCoroutine(Restart());
        }

        if (item.GetComponent<knifeEnemy>()) {
            FindObjectOfType<SoundManager>().Play("dieKnife");
        }else if(item.tag == "spine") {
            FindObjectOfType<SoundManager>().Play("dieSpine");
        }

        if (item.tag == "pickup") {
            GetComponent<Weapon>().PickGun(item.GetComponent<PickUp>().haveGun);
        }
    }

    public bool isDead = false;
    IEnumerator Restart() {
        isDead = true;
        yield return new WaitForSeconds(0.5f);
        isDead = false;

        if (SceneManager.GetActiveScene().name == "BossLevel") {
            SceneManager.LoadScene("BossLevel");
        }else {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            map.GetComponent<MapEditor>().Load();
        }
    }


}