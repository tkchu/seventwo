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
            if (GetComponent<NormalMove>()) {
                GetComponent<NormalMove>().OneMove();
            }else if (GetComponent<LameMove>()) {
                GetComponent<LameMove>().OneMove();
            }else if (GetComponent<DiagonalMove>()) {
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
            GetComponent<Bomb>().Die();
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
        }

        if (GetComponent<MoveEnemy>() != null) {
            GetComponent<MoveEnemy>().willAction = false;
        }
    }
    public void Meet(GameObject g) {
        Debug.Log(gameObject.name + " meet " + g.name);
    }
}