﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public int range;
    public int backForce;
    public bool isJump;
    public bool isRoll;


    private GridWorld gw;
    void Start () {
        
        gw = FindObjectOfType<GridWorld>();
        SetGun(0);
    }

    public int Go(Vector2 direction) {
        
        int pos_x = gw.GridItem_x(GetComponent<GridItem>());
        int pos_y = gw.GridItem_y(GetComponent<GridItem>());

        GridItem[] face = FindGridItemInRange(pos_x, pos_y, direction, this.range);
        GridItem[] back = FindGridItemInRange(pos_x, pos_y, direction*(this.backForce<0?-1:1), Mathf.Abs(this.backForce));

        bool shot = false;
        bool action = true;
        int move_mult;
        foreach(GridItem item in face) {
            if(item!=null&& item.gridItemType == GridItemType.enemy) {
                Destroy(item.gameObject);
                shot = true;
            }
        }
        if (!shot) {
            if (face.Length > 0) {
                move_mult = 1;
                action = true;
            }else {
                move_mult = 0;
                action = false;
            }
        }else {
            move_mult = (this.backForce < 0 ? -1 : 1) * (Mathf.Min(Mathf.Abs(backForce), back.Length));
        }

        if (action) {
            OneAction();
        }

        Vector3 new_pos = gw.Go(this.GetComponent<GridItem>(), direction * move_mult);
        Debug.Log(new_pos);
        transform.position = new_pos;
        gw.Flush();
        
        return 0;
    }

    List<int[]> gunInfo = new List<int[]>() {
        new int[] { 1, 0, 5 }, //pistol
        new int[] { 2, -1, 3}, //shotgun
        new int[] {3,3,2}, //jumpgun
    };
    int gunIndex = 0;
    int gunCount = 5;
    public void OneAction() {
        gunCount -= 1;
        if(gunCount <= 0) {
            gunIndex = (gunIndex + 1) % gunInfo.Count;
            SetGun(gunIndex);
        }

    }

    private void SetGun(int gunIndex) {
        this.range = gunInfo[gunIndex][0];
        this.backForce = gunInfo[gunIndex][1];
        this.gunCount = gunInfo[gunIndex][2];
    }

    private GridItem[] FindGridItemInRange(int pos_x, int pos_y, Vector2 direction, int range) {
        List<GridItem> result = new List<GridItem>();
        for (int i = 1; i <= range; i++) {
            GridItem temp = gw.GridItemAt(pos_x + i * (int)direction.x, pos_y + i * (int)direction.y);
            if(temp != null && temp.gridItemType == GridItemType.wall) {
                break;
            }
            result.Add(temp);
        }
        return result.ToArray();
    }
}