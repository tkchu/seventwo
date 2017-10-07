﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelSetUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Weapon weapon = FindObjectOfType<Weapon>();
        weapon.gunNow = 0;
        weapon.loadCount = weapon.loadTime + 1;
        weapon.GetComponent<Animator>().SetInteger("stat", (int)weapon.gunNow + 1);
    }

    // Update is called once per frame
    void Update () {
		
	}
}