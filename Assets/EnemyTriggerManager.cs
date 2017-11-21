using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager : MonoBehaviour {
    MapEditor me;
	// Use this for initialization
	void Start () {
        me = FindObjectOfType<MapEditor>();
        Load();
	}
	
    public void Load() {
        if(transform.childCount>0)
        foreach (Transform t in transform) {
            if (t.GetComponent<EnemyMoveTrigger>().mapID != me.mapID) {
                t.gameObject.SetActive(false);
            } else {
                t.gameObject.SetActive(true);
                t.GetComponent<EnemyMoveTrigger>().Load();
            }
        }
    }
}
