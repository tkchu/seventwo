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
    [Space]
    public GameObject[,] basicMap;
    
    void Start () {
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

        //GameObject g = Instantiate(playerPrefab, basicMap[xy[0], xy[1]].transform.localPosition, Quaternion.identity, transform);
        //GetComponent<Map>().itemMap[xy[0], xy[1]] = g;
        Debug.Log(xy);
        if (Input.GetKey(KeyCode.LeftShift)){
            //按住shift是铺地面
            //如果之前这里已经有地面了，那么先找出要铺的地面类型
            GameObject oldGround = GetComponent<Map>().groundMap[xy[0], xy[1]];
            int groundPrefabIndex = 0;
            if (oldGround != null) {
                for (int i = 0; i < groundPrefabs.Length; i++) {
                    if (groundPrefabs[i].name == oldGround.name) {
                        groundPrefabIndex = i + 1;
                        break;
                    }
                }
                //销毁地面
                Destroy(oldGround);
            }
            if (groundPrefabIndex < groundPrefabs.Length) {
                GameObject newGround = Instantiate(groundPrefabs[groundPrefabIndex], basicMap[xy[0], xy[1]].transform.localPosition, Quaternion.identity, transform);
                newGround.name = groundPrefabs[groundPrefabIndex].name;
                GetComponent<Map>().groundMap[xy[0], xy[1]] = newGround;
            }
        } else {
            //否则是在铺地面以上的物品
        }
    }
}
