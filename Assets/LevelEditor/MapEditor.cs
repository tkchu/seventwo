using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour {
    //编辑地图信息
    //地图分两层,地面层和地上层（包括墙）

    public GameObject basicTilePRefab;
    public Vector2 mapSize;
    public Vector2 tileSize;
    public Vector3 leftBottomPos;

    [Space]
    public GameObject[] groundPrefabs;
    public GameObject[] itemPrefabs;
    public GameObject[] movePrefabs;
    [Space]
    public GameObject[,] basicMap;

    void Start() {
        basicMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
        GetComponent<Map>().groundMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
        GetComponent<Map>().itemMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
        for (int i = 0; i < mapSize.x; i++) {
            for (int j = 0; j < mapSize.y; j++) {
                Vector3 pos = new Vector3(i * tileSize.x, j * tileSize.y, 0) + leftBottomPos;
                GameObject g = Instantiate(basicTilePRefab, pos, Quaternion.identity, transform);
                basicMap[i, j] = g;
            }
        }
    }

    void MouseDown(BasicTile bt) {
        int[] xy = GetComponent<Map>().FindGameObject(basicMap, bt.gameObject);
        GameObject[,] collectionTo;
        GameObject[] prefabs;

        if (Input.GetKey(KeyCode.LeftShift)) {
            //铺地板
            collectionTo = GetComponent<Map>().groundMap;
            prefabs = groundPrefabs;
        } else if (Input.GetKey(KeyCode.LeftControl)) {
            //铺墙
            collectionTo = GetComponent<Map>().itemMap;
            prefabs = itemPrefabs;
        } else {
            //铺怪
            collectionTo = GetComponent<Map>().itemMap;
            prefabs = movePrefabs;
        }
        //如果之前这里已经有了，那么先找出要铺的类型
        GameObject old = collectionTo[xy[0], xy[1]];
        int prefabIndex = 0;
        if (old != null) {
            for (int i = 0; i < prefabs.Length; i++) {
                if (prefabs[i].name == old.name) {
                    prefabIndex = i + 1;
                    break;
                }
            }
            //销毁地面
            Destroy(old);
        }

        if (prefabIndex < prefabs.Length) {
            GameObject newOne = Instantiate(prefabs[prefabIndex], basicMap[xy[0], xy[1]].transform.localPosition, Quaternion.identity, transform);
            newOne.name = prefabs[prefabIndex].name;
            collectionTo[xy[0], xy[1]] = newOne;
        }
    }
}
