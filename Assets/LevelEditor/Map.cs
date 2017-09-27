using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour {

    public GameObject[,] groundMap;
    public GameObject[,] itemMap;

    GameObject player = null;
    int[] playerPos = { 0, 0};

    public int[] GetPlayerPos() {
        if (player == null) {
            player = FindObjectOfType<Player>().gameObject;
        }
        if (player != null) {
            int[] find = FindGameObject(itemMap, player);
            if (find != null) {
                playerPos = find;
            }
        }
        return playerPos;
    }

    public GameObject GetGameObjectAt(int x, int y) {
        if(x<0 || x>itemMap.GetLength(0) || y<0 || y > itemMap.GetLength(1)) {
            return null;
        } else {
            return itemMap[x, y];
        }
    }

    public int[] GetItemPos(GameObject g) {
        return FindGameObject(itemMap, g);
    }

    public bool MoveItem(GameObject g, int[] pos) {
        int[] now = FindGameObject(itemMap, g);
        if(now == null) {
            return false;
        } else {
            itemMap[pos[0], pos[1]] = g;
            g.transform.position = groundMap[pos[0], pos[1]].transform.position + new Vector3(0, 0.6f / 3, 0);
            itemMap[now[0], now[1]] = null;
            return true;
        }
    }

    public int[] FindGameObject(GameObject[,] collection, GameObject g) {
        for (int i = 0; i < collection.GetLength(0); i++) {
            for (int j = 0; j < collection.GetLength(1); j++) {
                if (collection[i,j] == g) {
                    return new int[] { i, j };
                }
            }
        }
        return playerPos;
    }

    public void UpdateSortOrder() {
        //基础地板为10
        //物体为100 + 10
        //这里只更新itemMap中的物体，因为地板是static的，在地图编辑时修改
        for (int i = 0; i < itemMap.GetLength(0); i++) {
            for (int j = 0; j < itemMap.GetLength(1); j++) {
                if (itemMap[i,j]) {
                    itemMap[i, j].GetComponent<SpriteRenderer>().sortingOrder = 100 + (itemMap.GetLength(1) - j) * 10;
                    if(itemMap[i,j].name == "KnifeEnemy") {
                        itemMap[i,j].transform.Find("KnifeEnemyKnife").GetComponent<SpriteRenderer>().sortingOrder = 101 + (itemMap.GetLength(1) - j) * 10;
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (LameMove lm in FindObjectsOfType<LameMove>()) {
                lm.OneMove();
            }
            foreach (NormalMove nm in FindObjectsOfType<NormalMove>()) {
                nm.OneMove();
            }
            foreach(DiagonalMove dm in FindObjectsOfType<DiagonalMove>()) {
                dm.OneMove();
            }
        }
    }
}
