﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    Animator heroani, bossshieldani;
    private void Start()
    {
        if (GameObject.Find("shield") != null)
            bossshieldani = GameObject.Find("shield").GetComponent<Animator>();
    }
    void Update()
        {if(heroani==null&& GameObject.FindWithTag("Player"))
            heroani = GameObject.FindWithTag("Player").GetComponent<Animator>();
        }

    public void OneAction() {
        if (GetComponent<Bomb>() && GetComponent<Bomb>().isReady) {
            ;
        }else {
            if (GetComponent<NormalMove>() && GetComponent<NormalMove>().enabled) {
                GetComponent<NormalMove>().OneMove();
            }else if (GetComponent<LameMove>() && GetComponent<LameMove>().enabled) {
                GetComponent<LameMove>().OneMove();
            }else if (GetComponent<DiagonalMove>() && GetComponent<DiagonalMove>().enabled) {
                GetComponent<DiagonalMove>().OneMove();
            }
        }
        
        if (GetComponent<Bomb>()) {
            GetComponent<Bomb>().OneAction();
        }

        if (GetComponent<Boss>()) {
            GetComponent<Boss>().OneAction();
        }
    }
    
    public void OneShot() {
        if (GetComponent<knifeEnemy>()) {
            GetComponent<knifeEnemy>().Die();
        }
        if (GetComponent<Bomb>()) {
            GetComponent<Bomb>().InstantDie();
        }

        if (GetComponent<Lame>()) {
            GetComponent<Lame>().Die();
        }
        if (GetComponent<Diagonal>()) {
            GetComponent<Diagonal>().Die();
        }

        if (GetComponent<BossPart>() == null) {
            //gw.Destroy(GetComponent<GridItem>());
        } else {
            Debug.Log("Boss 挨了一枪，但毫发无损！");
            if (FindObjectOfType<Boss>().unbeatable)
                return;
            if (heroani.GetInteger("stat")==1)
                FindObjectOfType<SoundManager>().Play("missshotgun");
            if (heroani.GetInteger("stat")==2)
                FindObjectOfType<SoundManager>().Play("misslonggun");
            bossshieldani.SetBool("hited", true);            
        }
        /*
        if (GetComponent<MoveEnemy>() != null) {
            GetComponent<MoveEnemy>().willAction = false;
        }*/
    }
    public void Meet(GameObject g) {
        Debug.Log(gameObject.name + " meet " + g.name);
    }
}