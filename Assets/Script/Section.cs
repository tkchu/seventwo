﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {
    public string prefabName;
    Transform player;
    float gridSize;

    private void Start() {
        player = FindObjectOfType<Player>().transform;
        gridSize = FindObjectOfType<GridWorld>().gridSize;
    }

    void Update(){
        if(!begin && Mathf.Abs(transform.position.x - player.position.x) < gridSize/2 && Mathf.Abs(transform.position.y - player.position.y) < gridSize / 2) {
            Begin();
        }
    }

    public bool begin = false;
    public void Begin() {
        Debug.Log("let's fucking begin");
        foreach (Transform t in transform) {
            if (t.GetComponent<MoveEnemy>()) {
                t.GetComponent<MoveEnemy>().willAction = true;
            }
        }
        begin = true;
        transform.parent.GetComponent<Reseter>().sectionNow = this;
        transform.parent.GetComponent<Reseter>().playerBegin = player.position;
        transform.parent.GetComponent<Reseter>().loadCount = player.GetComponent<Weapon>().loadCount;

    }
}