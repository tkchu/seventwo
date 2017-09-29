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

    public List<Guns> gunHave = new List<Guns>() {
        Guns.empty,
    };

    public int gunHaveNow;

    public Guns gunNow {
        get { return gunHave[gunHaveNow]; }
    }
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
    }

    public void PickGun(Guns gun) {
        if (gunHave.Contains(Guns.empty)) {
            gunHave.Remove(Guns.empty);
        }
        //捡枪
        gunHave.Add(gun);
        gunHaveNow = gunHave.Count - 1;
        loadCount = loadTime +1;

        GetComponent<Animator>().SetInteger("stat", gunHaveNow + 1);
    }

    public void SwitchGun() {
        //换枪
        int temp = gunHaveNow;
        gunHaveNow = (gunHaveNow + 1) % gunHave.Count;
        loadCount = loadTime;
        if(temp != gunHaveNow) {
            FindObjectOfType<SoundManager>().Play("reload");
        }
        GetComponent<Animator>().SetInteger("stat", gunHaveNow + 1);
    }

    public int loadCount = 0;
    public void OneAction() {
        loadCount -= 1;
        if(loadCount <= 0) {
            SwitchGun();
        }
    }

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
            return new int[] { 1, backForce };
        }else {
            return new int[] { 0, backForce };
        }
    }    
}