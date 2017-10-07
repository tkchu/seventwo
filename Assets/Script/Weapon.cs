﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Guns {
    empty,
    pistol,
    shotgun,
    jumpgun,
}

public class Weapon : MonoBehaviour {
    static Dictionary<Guns, int[]> gunInfo = new Dictionary<Guns, int[]>() {
        {Guns.empty, new int[] {0, 1, 99999} },
        {Guns.pistol, new int[] { 1, 0, 5 } },
        {Guns.shotgun, new int[] { 2, -1, 3} },
        {Guns.jumpgun, new int[] {4, 4, 2} },
    };
    public Guns gunNow;

    public int range {
        get { return gunInfo[gunNow][0]; }
    }
    public int backForce {
        get { return gunInfo[gunNow][1]; }
    }
    public int loadTime {
        get { return gunInfo[gunNow][2]; }
    }

    Map map;
    void Start() {
        map = FindObjectOfType<Map>();
        GetComponent<Animator>().SetInteger("stat", (int)gunNow);
    }

    public void PickGun(Guns gun) {
        gunNow = gun;
        loadCount = gunInfo[gun][2];
        GetComponent<Animator>().SetInteger("stat", (int)gunNow);
    }

    public int loadCount = 0;

    public int[] Shoot(int[] direction) {
        bool shot = false;
        int[] pos = map.FindGameObject(map.itemMap, gameObject);
        GameObject[] face = map.FindGridItemInRange(pos, direction, this.range);

        foreach (GameObject item in face) {
            if (item != null &&
                (item.tag == "enemy" || item.tag == "boss")) { 
                shot = true;
                if (item.GetComponent<knifeEnemy>()) {
                    item.GetComponent<knifeEnemy>().noDie = false;
                }
            }
        }

        //播放音效，返回后坐力
        if (shot) {
            if(gunNow == Guns.pistol) {
                FindObjectOfType<SoundManager>().Play("pistolShot");
            }else if(gunNow == Guns.shotgun) {
                FindObjectOfType<SoundManager>().Play("shotGunShot");
            }else if(gunNow == Guns.jumpgun) {
                FindObjectOfType<SoundManager>().Play("jumpGunShot");
            }
            loadCount -= 1;
            if(loadCount <= 0) {
                gunNow = Guns.empty;
                GetComponent<Animator>().SetInteger("stat", (int)gunNow);
                loadCount = 0;
            }
            return new int[] { 1, backForce };
        }else {
            return new int[] { 0, backForce };
        }
    }    
}