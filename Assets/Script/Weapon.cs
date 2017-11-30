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
        loadCount = gunInfo[gunNow][2];
        GetComponent<Animator>().SetInteger("stat", (int)gunNow);
    }

    private void Update() {

    }

    public void PickGun(Guns gun) {
        gunNow = gun;
        loadCount = gunInfo[gun][2];
        GetComponent<Animator>().SetInteger("stat", (int)gunNow);
        FindObjectOfType<WeaponBar>().PickGun(this);
    }

    public int loadCount = 0;

    public int[] Shoot(int[] direction) {
        
        bool shot = false;
        int[] pos = map.FindGameObject(map.itemMap, gameObject);
        GameObject[] face = map.FindGridItemInRange(pos, direction, this.range);

        foreach (GameObject item in face) {
            if (item != null &&
                (item.tag == "enemy" || item.tag == "boss"|| item.tag == "bomb")) { 
                shot = true;
                item.GetComponent<Enemy>().OneShot();
            }
        }


        //播放音效，以及动画，返回后坐力
        if (shot) {
            //map.lastHit = 0.5f;
            Vector2 directionVec2 = new Vector2(direction[0], direction[1]);
            if (directionVec2 == Vector2.left) {
                GetComponent<AnimatorControl>().HeroShot(0);
            } else if (directionVec2 == Vector2.right) {
                GetComponent<AnimatorControl>().HeroShot(1);
            } else if (directionVec2 == Vector2.up) {
                GetComponent<AnimatorControl>().HeroShot(2);
            } else if (directionVec2 == Vector2.down) {
                GetComponent<AnimatorControl>().HeroShot(3);
            }
            if (gunNow == Guns.pistol) {
                FindObjectOfType<SoundManager>().Play("pistolShot");
            }else if(gunNow == Guns.shotgun) {
                FindObjectOfType<SoundManager>().Play("shotGunShot");
            }else if(gunNow == Guns.jumpgun) {
                FindObjectOfType<SoundManager>().Play("jumpGunShot");
            }
            int [] result = new int[] { 1, backForce };
            loadCount -= 1;
            if (loadCount <= 0) {
                StartCoroutine(NoWeapon());
            }
            return result;
        }else {
            return new int[] { 0, backForce };
        }
    }

    IEnumerator NoWeapon() {
        yield return new WaitWhile(() => GetComponent<Animator>().GetBool("isShoot"));
        if (loadCount <= 0) {
            gunNow = Guns.empty;
            GetComponent<Animator>().SetInteger("stat", (int)gunNow);
            loadCount = gunInfo[gunNow][2];
        }
        yield return new WaitForEndOfFrame();
    }
}