using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    MapEditor mapEditor;
	// Use this for initialization
	void Start () {
        mapEditor = FindObjectOfType<MapEditor>();
	}
	
	public void Meet(GameObject g) {
        if(g.tag == "Player") {
            mapEditor.mapID += 1;
            mapEditor.Load();
        }
    }
}
