﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    GridWorld gw;
    private void Start() {
        gw = FindObjectOfType<GridWorld>();
    }

    void OneAction() {

    }
    public void OneShot() {
        Debug.Log("I am fucked");
        Destroy(gameObject);
        gw.Destroy(GetComponent<GridItem>());
    }
}