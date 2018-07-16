﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    Map map;
    MapMist mapMist;
    //public static float revivelife = 0.4f;
    void Start() {
        map = FindObjectOfType<Map>();
        mapMist = FindObjectOfType<MapMist>();
        //yield return new WaitForSeconds(revivelife);
        transform.Find("传送动画").gameObject.SetActive(true);
        
        //revivelife = 0f;
    }

    public void Go(int[] direction) {
        if (isDead) return;
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
        }
        else {
            GameObject itemMeet = map.MoveItem(gameObject,
                new int[]{
                    pos[0] + direction[0] * Mathf.Abs(move) * (move < 0 ? -1 : 1),
                    pos[1] +  direction[1] * Mathf.Abs(move) * (move < 0 ? -1 : 1)
                });
            if (itemMeet != null && itemMeet.tag == "teleporter")
                return;
            haveMove = true;
        }


        if (haveMove || shot == 1)
        {
            if (Mathf.Abs(direction[0]) > 0f)
            {
                GetComponent<SpriteRenderer>().flipX = direction[0] > 0;
            }

            Enemy[] items = FindObjectsOfType<Enemy>();
            foreach (Enemy item in items)
            {
                item.OneAction();
            }
            /*
            if (FindObjectOfType<Boss>()) {
                FindObjectOfType<Boss>().OneAction();
            }*/
        }

        if (shot == 0 && haveMove) {
            FindObjectOfType<SoundManager>().Play("move");
            GetComponent<Animator>().SetBool("isMoving", true);
        }
        //mapMist.UpdateMist();
        FindObjectOfType<Radar>().Step();

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

    public Sprite[] introSprites;
    public void Meet(GameObject item) {
        if (isDead) return;
        Boss boss = FindObjectOfType<Boss>();
       if (FindObjectOfType<Boss>())
            if (FindObjectOfType<Boss>().hp == 0)
                return;
        if(item.GetComponent<knifeEnemy>() && !item.GetComponent<knifeEnemy>().noDie) {
            return;
        }
        if (item.GetComponent<Lame>() && !item.GetComponent<Lame>().noDie) {
            return;
        }
        if (item.GetComponent<Diagonal>() && !item.GetComponent<Diagonal>().noDie) {
            return;
        }

        if (item.tag == "enemy" || item.tag == "spine"|| item.tag == "bomb")
        {
            if(Random.Range(0,2)==1) 
                FindObjectOfType<SoundManager>().Play("beattacked0");
            else
                FindObjectOfType<SoundManager>().Play("beattacked1");
            GetComponent<Animator>().SetBool("isDead", true);
            //item.GetComponent<Animator>().SetBool("isAttacking", true);
            Sprite enemySprite = item.GetComponent<SpriteRenderer>().sprite;
            Sprite introSprite = null;
            if (item.GetComponent<Bomb>()) {
                item.GetComponent<Bomb>().InstantDie();
                introSprite = introSprites[0];
            }else if (item.GetComponent<Lame>()) {
                introSprite = introSprites[1];
            } else if (item.GetComponent<Diagonal>()) {
                introSprite = introSprites[2];
            } else if(item.GetComponent<knifeEnemy>()) {
                introSprite = introSprites[3];
            } else if(item.tag == "spine") {
                introSprite = introSprites[4];
            }
            FindObjectOfType<DieImage>().Show(item.GetComponent<SpriteRenderer>().sprite, introSprite);
            transform.Find("Fire").gameObject.SetActive(false);
            transform.Find("FireUpDown").gameObject.SetActive(false);
            transform.Find("weapon").gameObject.SetActive(false);
            StartCoroutine(Restart());
        }

        if (item.GetComponent<knifeEnemy>()) {
            FindObjectOfType<SoundManager>().Play("dieKnife");
        }else if(item.tag == "spine") {
            FindObjectOfType<SoundManager>().Play("dieSpine");
        }
        /*
        if (item.tag == "pickup") {
            GetComponent<Weapon>().PickGun(item.GetComponent<PickUp>().haveGun);
        }
        */
    }

    public bool isDead = false;
    IEnumerator Restart()
    {
        map.acceptInput = false;
        isDead = true;
        //yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1.2f);
        isDead = false;

        yield return new WaitForSeconds(0.65f);
        if (SceneManager.GetActiveScene().name == "boss") {
            SceneManager.LoadScene("boss");
        }else {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            map.GetComponent<MapEditor>().Load();
            
        }
        map.acceptInput = true;
    }


}