﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource reload;
    public AudioSource pickup;
    // Use this for initialization

    Dictionary<string, AudioSource> dict;
	void Start () {
        dict = new Dictionary<string, AudioSource>() {
            {"pickup", pickup },
            {"reload", reload },
        };
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void Play(string soundName) {
        dict[soundName].Play();
    }
}