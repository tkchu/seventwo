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

    }
    public void OneShot() {
        Debug.Log("I am fucked");
        if (GetComponent<knifeEnemy>()) {
            GetComponent<knifeEnemy>().Die();
        }
        if (GetComponent<Bomb>()) {
            GetComponent<Bomb>().Die();
        }
        gw.Destroy(GetComponent<GridItem>());
        if(GetComponent<MoveEnemy>() != null) {
            GetComponent<MoveEnemy>().willAction = false;
        }
    }
    public void Meet() {
        Debug.Log(gameObject.name + "meet");
    }
}