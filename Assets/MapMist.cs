using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMist : MonoBehaviour {
    MapEditor mapEditor;
    Map map;
    public bool init = false;
    public int[] playerpos;
    public int[,] view = new int[5,2] { { 0, 1 },{ 0, 0 },{ 1, 0 },{ 0, -1 },{ -1, 0 } }; 
    GameObject[,] mistmap=new GameObject[0,0]; 
    public GameObject mist;
	// Use this for initialization
	void Start () {
        mapEditor = GetComponent<MapEditor>();
        map=GetComponent<Map>();
        mistmap = new GameObject[0, 0];
    }
    public void Init()
    {
        mistmap = new GameObject[(int)mapEditor.mapSize.x, (int)mapEditor.mapSize.y];
        for (int i = 0; i < mapEditor.mapSize.x; i++)
        {
            for (int j = 0; j < mapEditor.mapSize.y; j++)
            {
                Vector3 pos = new Vector3(i * mapEditor.tileSize.x, j * mapEditor.tileSize.y, 0) + mapEditor.leftBottomPos;
                GameObject g = Instantiate(mist, pos, Quaternion.identity, transform);
                mistmap[i, j] = g;
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if (init)
        {
            UpdateMist();
            init = false;
        }

	}

public void UpdateMist()
    {
        playerpos = map.GetPlayerPos();
        Debug.Log(playerpos);
        for (int i = 0; i < mapEditor.mapSize.x; ++i)
            for (int j = 0; j < mapEditor.mapSize.y; ++j)
            {
                if (map.itemMap[i,j]!=null&&map.itemMap[i, j].tag == "wall") continue;
                mistmap[i, j].GetComponent<Mist>().cover = true;
                mistmap[i, j].GetComponent<Mist>().trigger = true;
            }
        for(int i = 0; i < view.GetLength(0); ++i)
        {
            if((playerpos[0] + view[i, 0])>=0&&( playerpos[0] + view[i, 0])<mapEditor.mapSize.x&& (playerpos[1] + view[i, 1]>=0)&&( playerpos[1] + view[i, 1] )< mapEditor.mapSize.y)
            {
            mistmap[playerpos[0] + view[i, 0], playerpos[1] + view[i, 1]].GetComponent<Mist>().cover = false;
            mistmap[playerpos[0] + view[i, 0], playerpos[1] + view[i, 1]].GetComponent<Mist>().trigger = true;

            }
        }
        if ((playerpos[0] + 2 * map.playerface[0]) >= 0 && (playerpos[0] + 2 * map.playerface[0]) < mapEditor.mapSize.x && (playerpos[1] + 2 * map.playerface[1]) >= 0 && (playerpos[1] + 2 * map.playerface[1]) < mapEditor.mapSize.y)
        {
            mistmap[playerpos[0] + 2 * map.playerface[0], playerpos[1] + 2 * map.playerface[1]].GetComponent<Mist>().cover = false;
            mistmap[playerpos[0] + 2 * map.playerface[0], playerpos[1] + 2 * map.playerface[1]].GetComponent<Mist>().trigger = true;
        }
    }
}
