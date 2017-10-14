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

    public AudioSource teleporter;
    public AudioSource bossfail;
    public AudioSource bossattack;
    public AudioSource missshotgun;
    public AudioSource misslonggun;

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
            {"teleporter", teleporter },
            {"bossfail",bossfail },
            {"bossattack",bossattack },
            {"missshotgun",missshotgun },
            {"misslonggun",misslonggun }
        };
    }

    public void Play(string soundName) {
        if(soundName == "move") {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = dict["move"].clip;
            source.volume = dict["move"].volume;
            source.pitch = dict["move"].pitch;
            source.Play();
            Destroy(source, source.clip.length);
        } else {
            dict[soundName].Play();
        }
    }
}