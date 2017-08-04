﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    GridWorld gw;
    private void Start() {
        gw = FindObjectOfType<GridWorld>();
    }

    public void OneAction() {
        if (GetComponent<Bomb>() && GetComponent<Bomb>().isReady) {
            ;
        }else {
            if (GetComponent<MoveEnemy>()) {
                GetComponent<MoveEnemy>().OneAction();
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
        Debug.Log("I am fucked");
        if (GetComponent<knifeEnemy>()) {
            GetComponent<knifeEnemy>().Die();
        }
        if (GetComponent<Bomb>()) {
            GetComponent<Bomb>().Die();
        }
        if (GetComponent<BossPart>() == null) {
            gw.Destroy(GetComponent<GridItem>());
        }

        if (GetComponent<MoveEnemy>() != null) {
            GetComponent<MoveEnemy>().willAction = false;
        }

        if (GetComponent<BossPart>()) {
            Debug.Log("Boss 挨了一枪，但毫发无损！");
        }
    }
    public void Meet() {
        Debug.Log(gameObject.name + "meet");
    }
}