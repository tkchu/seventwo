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
        {Guns.jumpgun, new int[] {3,3,2} },
    };

    List<Guns> gunHave = new List<Guns>() {
        Guns.empty,
    };

    public int gunHaveNow;

    public Guns gunNow {
        get { return gunHave[gunHaveNow]; }
    }
    private int range {
        get { return gunInfo[gunNow][0]; }
    }
    private int backForce {
        get { return gunInfo[gunNow][1]; }
    }
    private int loadTime {
        get { return gunInfo[gunNow][2]; }
    }

    public void PickGun(Guns gun) {
        if (gunHave.Contains(Guns.empty)) {
            gunHave.Remove(Guns.empty);
        }
        //捡枪
        gunHave.Add(gun);
        gunHaveNow = gunHave.Count - 1;
        loadCount = loadTime;
    }

    public void SwitchGun() {
        //换枪
        gunHaveNow = (gunHaveNow + 1) % gunHave.Count;
        loadCount = loadTime;
    }

    public int loadCount = 0;
    public void OneAction() {
        loadCount -= 1;
        if(loadCount < 0) {
            SwitchGun();
        }
    }

    public int[] Shoot(Vector2 direction) {
        bool shot = false;
        int pos_x = gw.GridItem_x(GetComponent<GridItem>());
        int pos_y = gw.GridItem_y(GetComponent<GridItem>());
        GridItem[] face = gw.FindGridItemInRange(pos_x, pos_y, direction, this.range);

        foreach (GridItem item in face) {
            if (item != null && item.gridItemType == GridItemType.enemy) {
                item.SendMessage("OneShot");
                shot = true;
            }
        }

        if (shot) {
            return new int[] {1, backForce };
        }else {
            return new int[] { 0, backForce };
        }
    }

    private GridWorld gw;
    void Start () {
        gw = FindObjectOfType<GridWorld>();
    }
}