using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombpoint : MonoBehaviour {
    Map map;
    MapEditor mapEditor;
    public GameObject[] prefab;
	// Use this for initialization
	void Start () {
        mapEditor = FindObjectOfType<MapEditor>();
        map = FindObjectOfType<Map>();
	}
	
    void Create() {
        
        int [] xy= map.FindGameObject(map.itemMap, gameObject);
        Vector3 pos = mapEditor.basicMap[xy[0], xy[1]].transform.localPosition;
        pos += new Vector3(0, mapEditor.tileSize.y / 3, 0);
        if(map.itemMap[xy[0], xy[1]]!=null&& map.itemMap[xy[0], xy[1]].tag!="Player")
        Destroy(map.itemMap[xy[0], xy[1]]);
        map.itemMap[xy[0], xy[1]] = Instantiate(prefab[Random.Range(0, prefab.Length)], pos, Quaternion.identity, transform);
    }

	// Update is called once per frame
	void Update () {
        

    }
}
