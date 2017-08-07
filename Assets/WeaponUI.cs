using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour {
    // Use this for initialization
    public int step=0;
    SpriteRenderer shouqiang,xiandanqiang,dao;

	void Start () {
        shouqiang = GameObject.Find("shouqiang").GetComponent<SpriteRenderer>();
        xiandanqiang = GameObject.Find("xiandanqiang").GetComponent<SpriteRenderer>();
        dao=GameObject.Find("dao").GetComponent<SpriteRenderer>();


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
