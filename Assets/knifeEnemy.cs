﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeEnemy : MonoBehaviour {
    GridWorld gw;
    public Animator prepareAnimator;
    public int range = 2;

    public Sprite dieSprite;
    public GameObject knife;
    private bool noDie = true;

    private void Start() {
        gw = FindObjectOfType<GridWorld>();
    }

    // Update is called once per frame
    void Update () {
        GridItem player = FindObjectOfType<Player>().GetComponent<GridItem>();
        GridItem self = GetComponent<GridItem>();

        int player_x = gw.GridItem_x(player);
        int player_y = gw.GridItem_y(player);
        int self_x = gw.GridItem_x(self);
        int self_y = gw.GridItem_y(self);

        //准备
        if (noDie) {
            if (Mathf.Abs(player_x - self_x) + Mathf.Abs(player_y - self_y) <= 2) {
                prepareAnimator.SetBool("isReady", true);
            } else {
                prepareAnimator.SetBool("isReady", false);
            }
            if (Mathf.Abs(player_x - self_x) + Mathf.Abs(player_y - self_y) <= 0.3) {
                prepareAnimator.SetBool("isAttack", true);
            } else {
                prepareAnimator.SetBool("isAttack", false);
            }
        }
        
    }

    public void Die() {
        StartCoroutine(dieC());
    }

    IEnumerator dieC() {
        noDie = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        Destroy(knife);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = dieSprite;
        int time = 6;
        while(time > 0) {
            time -= 1;
            if (time % 2 == 0) {
                sr.color = Color.clear;
            }else {
                sr.color = Color.white;
            }
            yield return new WaitForSeconds(0.3f);
        }
        Destroy(gameObject);
    }
}