﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalMove : MonoBehaviour {
    Map map;
    // Use this for initialization
    void Start() {
        map = FindObjectOfType<Map>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            OneMove();
        }
    }

    void OneMove() {
        int[] playerPos = map.GetPlayerPos();
        int[] selfPos = map.GetItemPos(gameObject);

        int[][] nextPos = new int[][] {
            new int[]{selfPos[0]+1, selfPos[1] },
            new int[]{selfPos[0]-1, selfPos[1] },
            new int[]{selfPos[0], selfPos[1] +1 },
            new int[]{selfPos[0], selfPos[1] -1},
        };
        bool[] leftRightUpDown = new bool[] {
            playerPos[0] - selfPos[0] > 0,
            playerPos[0] - selfPos[0] < 0,
            playerPos[1] - selfPos[1] > 0,
            playerPos[1] - selfPos[1] < 0,
        };
        for (int i = 0; i < 4; i++) {
            leftRightUpDown[i] = leftRightUpDown[i]
                && (map.GetGameObjectAt(nextPos[0][0], nextPos[0][1]) == null
                || (nextPos[0][0] == playerPos[0] && nextPos[0][1] == playerPos[1]));
        }

        int nextPosI;
        if (Random.value < 0.5f) {
            nextPosI = 0;
            for (; nextPosI < 4; nextPosI++) {
                if (leftRightUpDown[nextPosI]) {
                    break;
                }
            }
        } else {
            nextPosI = 3;
            for (; nextPosI >= 0; nextPosI--) {
                if (leftRightUpDown[nextPosI]) {
                    break;
                }
            }
        }
        if (nextPosI >= 0 && nextPosI < 4) {
            map.MoveItem(gameObject, nextPos[nextPosI]);
        }
    }
}