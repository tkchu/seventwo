using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMove : MonoBehaviour {
    Map map;
	// Use this for initialization
	void Start () {
        map = FindObjectOfType<Map>();
	}

    public void OneMove() {
        int[] playerPos = map.GetPlayerPos();
        int[] selfPos = map.GetItemPos(gameObject);
        if (playerPos == null || selfPos == null)
            return;

        int[][] nextPos = new int[][] {
            new int[]{selfPos[0]+1, selfPos[1] },
            new int[]{selfPos[0]-1, selfPos[1] },
            new int[]{selfPos[0], selfPos[1] +1 },
            new int[]{selfPos[0], selfPos[1] -1},
        };
        bool[] leftRightUpDown = new bool[] {
            playerPos[0] > selfPos[0],
            playerPos[0] < selfPos[0],
            playerPos[1] > selfPos[1],
            playerPos[1] < selfPos[1],
        };
        for (int i = 0; i < 4; i++) {
            leftRightUpDown[i] = leftRightUpDown[i]
                && (map.GetGameObjectAt(nextPos[i][0], nextPos[i][1]) == null
                || (nextPos[i][0] == playerPos[0] && nextPos[i][1] == playerPos[1])
                );
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
