using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombpoint : MonoBehaviour {
    Map map;
    MapEditor mapEditor;
    public GameObject[] prefab;
    int[] xy;
    // Use this for initialization
    void Start () {
        map = FindObjectOfType<Map>();
        mapEditor = FindObjectOfType<MapEditor>();
        xy = map.FindGameObject(map.itemMap, gameObject);
        //Debug.Log(name + " " + xy[0]+" "+xy[1]);
        
        
	}
	
    public void Create(int[] xy) {
        GameObject old = map.itemMap[xy[0], xy[1]];
        Vector3 pos = mapEditor.basicMap[xy[0], xy[1]].transform.localPosition;
        pos += new Vector3(0, mapEditor.tileSize.y / 3, 0);
        if (old != null && old != gameObject && old.tag != "Player") 
            Destroy(old);
        map.itemMap[xy[0], xy[1]] = Instantiate(prefab[Random.Range(0, prefab.Length)], pos, Quaternion.identity, transform);
    }

	// Update is called once per frame
	void Update () {
        

    }
}
