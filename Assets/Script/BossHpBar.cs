﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour {
    public Image fillImage;
    Boss boss;
	void Start () {
        boss = FindObjectOfType<Boss>();
	}

	// Update is called once per frame
	void Update () {
        fillImage.fillAmount = boss.hp / 5f;
	}
}