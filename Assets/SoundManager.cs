﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource move;
    public AudioSource pickup;
    public AudioSource reload;
    public AudioSource pistolShot;
    public AudioSource shotGunShot;
    public AudioSource jumpGunShot;

    public AudioSource dieKnife;
    public AudioSource boom;
    public AudioSource dieSpine;

    // Use this for initialization

    Dictionary<string, AudioSource> dict;
	void Start () {
        dict = new Dictionary<string, AudioSource>() {
            {"move", move },
            {"pickup", pickup },
            {"reload", reload },
            {"pistolShot", pistolShot },
            {"shotGunShot", shotGunShot },
            {"jumpGunShot", jumpGunShot},
            {"dieKnife", dieKnife},
            {"boom", boom},
            {"dieSpine", dieSpine },
        };
    }

    public void Play(string soundName) {
        dict[soundName].Play();
    }
}