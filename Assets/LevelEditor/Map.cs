using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour {

    public GameObject[,] groundMap = new GameObject[0, 0] { };
    public GameObject[,] itemMap = new GameObject[0, 0] { };
    public GameObject[,] decorateMap = new GameObject[0, 0] { };

    GameObject player = null;
    int[] playerPos = { 0, 0};

    public int[] GetPlayerPos() {
        if (player != null) {
            int[] find = FindGameObject(itemMap, player);
            if (find != null) {
                playerPos = find;
            }
        }
        return playerPos;
    }

    public void RemoveGameObject(GameObject g) {
        for (int i = 0; i < itemMap.GetLength(0); i++) {
            for (int j = 0; j < itemMap.GetLength(1); j++) {
                if (itemMap[i, j] == g) {
                    itemMap[i, j] = null;
                    return;
                }
            }
        }
    }

    public GameObject GetGameObjectAt(int x, int y) {
        if(x<0 || x>=itemMap.GetLength(0) || y<0 || y >= itemMap.GetLength(1)) {
            return null;
        } else {
            return itemMap[x, y];
        }
    }

    public int[] GetItemPos(GameObject g) {
        return FindGameObject(itemMap, g);
    }

    public GameObject MoveItem(GameObject g, int[] pos) {
        //返回碰到的东西
        int[] now = FindGameObject(itemMap, g);
        GameObject itemMeet = null;
        if(now == null) {
            return itemMeet;
        } else {
            if(itemMap[pos[0], pos[1]]) {
                itemMeet = itemMap[pos[0], pos[1]];
                if (itemMeet.tag == "teleporter") {
                    itemMeet.GetComponent<Teleporter>().Trigger();
                    return itemMeet;
                }
                try
                {
                    itemMeet.SendMessage("Meet", g);
                    g.SendMessage("Meet", itemMeet);
                }
                catch(System.Exception e) {
                    throw e;
                }
            }
            itemMap[pos[0], pos[1]] = g;
            MapEditor me = GetComponent<MapEditor>();
            g.transform.position = new Vector3(pos[0] * me.tileSize.x, pos[1] * me.tileSize.y, 0) + me.leftBottomPos + new Vector3(0, 0.6f / 3, 0);
            itemMap[now[0], now[1]] = null;
            return itemMeet;
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
        return null;
    }

    public void UpdateSortOrder() {
        //基础地板为10
        //物体为100 + 10
        //这里只更新itemMap中的物体，因为地板是static的，在地图编辑时修改
        for (int i = 0; i < itemMap.GetLength(0); i++) {
            for (int j = 0; j < itemMap.GetLength(1); j++) {
                if (itemMap[i,j]) {
                    if (itemMap[i, j].GetComponent<Player>()) {
                        itemMap[i, j].GetComponent<SpriteRenderer>().sortingOrder = 100 + (itemMap.GetLength(1) - j) * 10 -1;
                    } else {
                        if(itemMap[i, j].GetComponent<SpriteRenderer>()!=null)
                            itemMap[i, j].GetComponent<SpriteRenderer>().sortingOrder = 100 + (itemMap.GetLength(1) - j) * 10 ;
                        else
                        {

                            itemMap[i, j].transform.Find("武器_影子_大").GetComponent<SpriteRenderer>().sortingOrder = 100 + (itemMap.GetLength(1) - j) * 10;
                            itemMap[i, j].transform.Find("漂浮").GetComponent<SpriteRenderer>().sortingOrder = 100 + (itemMap.GetLength(1) - j) * 10;
                        }
                    }
                    if (itemMap[i,j].name == "KnifeEnemy") {
                        Transform t = itemMap[i, j].transform.Find("KnifeEnemyKnife");
                        if (t) {
                            t.GetComponent<SpriteRenderer>().sortingOrder = 101 + (itemMap.GetLength(1) - j) * 10;
                        }
                    }
                }
            }
        }
    }

    public GameObject[] FindGridItemInRange(int[] pos, int[] direction, int range) {
        if (pos == null || direction == null)
            return null;
        List<GameObject> result = new List<GameObject>();
        for (int i = 1; i <= range; i++) {
            int x = pos[0] + i * direction[0];
            int y = pos[1] + i * direction[1];
            if(x < 0 || x >= itemMap.GetLength(0) || y < 0 || y >= itemMap.GetLength(1)) {
                break;
            }
            GameObject temp = GetGameObjectAt(x, y);
            if (temp != null && temp.tag == "wall") {
                break;
            }
            result.Add(temp);
        }
        return result.ToArray();
    }

    // Update is called once per frame
    float lastHit = 0.05f;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.fullScreen = false;
        }
        lastHit -= Time.deltaTime;
        var temp = FindObjectOfType<Player>();
        if (temp != null && !temp.isDead && lastHit <=0) {
            player = temp.gameObject;
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
                player.GetComponent<Player>().Go(new int[] { 0, 1 });
                lastHit = 0.05f;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
                player.GetComponent<Player>().Go(new int[] { 0, -1 });
                lastHit = 0.05f;
            } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                player.GetComponent<Player>().Go(new int[] { -1, 0 });
                lastHit = 0.05f;
            } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                player.GetComponent<Player>().Go(new int[] { 1, 0 });
                lastHit = 0.05f;
            }
        }

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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            FindObjectOfType<ExitTrigger>().GetComponent<ExitTrigger>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            FindObjectOfType<FullscreenTrigger>().GetComponent<FullscreenTrigger>().enabled = true;
        }

        UpdateSortOrder();

    }
}
