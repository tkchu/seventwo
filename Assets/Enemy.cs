﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
    void OneAction() {
        GridItem player = FindObjectOfType<Player>().GetComponent<GridItem>();
        GridItem self = GetComponent<GridItem>();
        GridWorld gw = FindObjectOfType<GridWorld>();
        int player_x = gw.GridItem_x(player);
        int player_y = gw.GridItem_y(player);
        int self_x = gw.GridItem_x(self);
        int self_y = gw.GridItem_y(self);
    }
    public void OneShot() {
        Debug.Log("I am fucked");
        Destroy(gameObject);
    }
}