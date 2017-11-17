﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeEnemy : MonoBehaviour {
    Animator prepareAnimator;
    public int range = 2;
    public GameObject knife;
    public bool noDie = true;

    Map map;

    void Start() {
        map = FindObjectOfType<Map>();
        prepareAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        int[] player_pos = map.GetPlayerPos();
        int[] self_pos = map.FindGameObject(map.itemMap, gameObject);

        if (player_pos == null || self_pos == null)
            return;

        //准备
        if (noDie) {
            if (Mathf.Abs(player_pos[0] - self_pos[0]) + Mathf.Abs(player_pos[1] - self_pos[1]) <= 2) {
                prepareAnimator.SetBool("isReady", true);
            } else {
                prepareAnimator.SetBool("isReady", false);
            }
            if (Mathf.Abs(player_pos[0] - self_pos[0]) + Mathf.Abs(player_pos[1] - self_pos[1]) <= 0.3) {
                prepareAnimator.SetBool("isAttacking", true);
            } else {
                prepareAnimator.SetBool("isAttacking", false);
            }
        }
        
    }

    public void Die() {
        map.RemoveGameObject(gameObject);
        StartCoroutine(dieC());
    }

    IEnumerator dieC() {
        noDie = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        Destroy(knife);
        prepareAnimator.SetBool("isDead",true);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
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