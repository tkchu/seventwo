﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {
    public bool willAction = false;
    public int range;
    
    GridWorld gw;
    private void Start() {
        gw = FindObjectOfType<GridWorld>();
    }

    public void OneAction() {
        if (willAction) {
            GridItem player = FindObjectOfType<Player>().GetComponent<GridItem>();
            GridItem self = GetComponent<GridItem>();

            int player_x = gw.GridItem_x(player);
            int player_y = gw.GridItem_y(player);
            int self_x = gw.GridItem_x(self);
            int self_y = gw.GridItem_y(self);

            bool haveMoved = false;

            if (player_x < self_x) {
                GridItem[] left = gw.FindGridItemInRange(self_x, self_y, Vector2.left, 1);
                if(left.Length > 0 && (left[0] == null || left[0].gridItemType == GridItemType.player)) {
                    Vector3 new_pos = gw.Go(GetComponent<GridItem>(), Vector2.left);
                    transform.position = new_pos;
                    haveMoved = true;
                }
            }
            if (!haveMoved && player_x > self_x) {
                GridItem[] right = gw.FindGridItemInRange(self_x, self_y, Vector2.right, 1);
                if (right.Length > 0 && (right[0] == null || right[0].gridItemType == GridItemType.player)) {
                    Vector3 new_pos = gw.Go(GetComponent<GridItem>(), Vector2.right);
                    transform.position = new_pos;
                    haveMoved = true;
                }
            }
            if (!haveMoved && player_y < self_y) {
                GridItem[] down = gw.FindGridItemInRange(self_x, self_y, Vector2.down, 1);
                if (down.Length > 0 && (down[0] == null || down[0].gridItemType == GridItemType.player)) {
                    Vector3 new_pos = gw.Go(GetComponent<GridItem>(), Vector2.down);
                    transform.position = new_pos;
                    haveMoved = true;
                }
            }
            if (!haveMoved && player_y > self_y) {
                GridItem[] up = gw.FindGridItemInRange(self_x, self_y, Vector2.up, 1);
                if (up.Length > 0 && (up[0] == null || up[0].gridItemType == GridItemType.player)) {
                    Vector3 new_pos = gw.Go(GetComponent<GridItem>(), Vector2.up);
                    transform.position = new_pos;
                    haveMoved = true;
                }
            }
            

        }
    }
}