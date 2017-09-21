using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour {

    public GameObject[,] groundMap;
    public GameObject[,] itemMap;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
                    itemMap[i, j].GetComponent<SpriteRenderer>().sortingOrder = 100 + (itemMap.GetLength(1) - j) * 10;
                    if(itemMap[i,j].name == "KnifeEnemy") {
                        itemMap[i,j].transform.Find("KnifeEnemyKnife").GetComponent<SpriteRenderer>().sortingOrder = 101 + (itemMap.GetLength(1) - j) * 10;
                    }
                }
            }
        }
    }
}
