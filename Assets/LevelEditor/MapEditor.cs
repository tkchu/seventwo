﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapEditor : MonoBehaviour {
    //编辑地图信息
    //地图分两层,地面层和地上层（包括墙）
    public int mapID;
    public bool save = false;
    public bool load = false;

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
        for (int i = 0; i < mapSize.x; i++) {
            for (int j = 0; j < mapSize.y; j++) {
                Vector3 pos = new Vector3(i * tileSize.x, j * tileSize.y, 0) + leftBottomPos;
                GameObject g = Instantiate(basicTilePRefab, pos, Quaternion.identity, transform);
                basicMap[i, j] = g;
            }
        }

        if (ExistFile("map" + mapID.ToString())) {
            Load();
        } else {
            GetComponent<Map>().groundMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
            GetComponent<Map>().itemMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
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
        Save();
    }

    public void Update() {
        if (save) {
            Save();
            save = false;
        }
        if (load) {
            Load();
            load = false;
        }
    }

    public void Save() {
        GameObject[,] groundMap = GetComponent<Map>().groundMap;
        GameObject[,] itemMap = GetComponent<Map>().itemMap;
        string key = "map"+mapID.ToString();
        string value = "";
        value += mapSize.x.ToString() + "," + mapSize.y.ToString() + ",";
        foreach(GameObject g in groundMap) {
            if (g != null) {
                value += g.name + ",";
            } else {
                value += "null" + ",";
            }
        }
        foreach (GameObject g in itemMap) {
            if (g != null) {
                value += g.name + ",";
            } else {
                value += "null" + ",";
            }
        }
        WriteFile(key, value);
    }
    public void Load() {
        string key = "map" + mapID.ToString();
        string value = ReadFile(key);
        string[] strs = value.Split(',');
        mapSize = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
        GameObject[,] groundMap = GetComponent<Map>().groundMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
        GameObject[,] itemMap = GetComponent<Map>().itemMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];

        int istr = 2;
        //处理地板
        for (int i = 0; i < groundMap.GetLength(0); i++) {
            for (int j = 0; j < groundMap.GetLength(1); j++) {
                for (int pi = 0; pi < groundPrefabs.Length; pi++) {
                    if (groundPrefabs[pi].name == strs[istr]) {
                        GameObject newOne = Instantiate(groundPrefabs[pi], basicMap[i, j].transform.localPosition, Quaternion.identity, transform);
                        newOne.name = strs[istr];
                        break;
                    }
                }
                istr += 1;
            }
        }
        //处理物体
        for (int i = 0; i < itemMap.GetLength(0); i++) {
            for (int j = 0; j < itemMap.GetLength(1); j++) {
                for (int pi = 0; pi < itemPrefabs.Length; pi++) {
                    if (itemPrefabs[pi].name == strs[istr]) {
                        GameObject newOne = Instantiate(itemPrefabs[pi], basicMap[i, j].transform.localPosition, Quaternion.identity, transform);
                        newOne.name = strs[istr];
                        break;
                    }
                }
                for (int pi = 0; pi < movePrefabs.Length; pi++) {
                    if (movePrefabs[pi].name == strs[istr]) {
                        GameObject newOne = Instantiate(movePrefabs[pi], basicMap[i, j].transform.localPosition, Quaternion.identity, transform);
                        newOne.name = strs[istr];
                        break;
                    }
                }
                istr += 1;
            }
        }
    }

    bool ExistFile(string fileName) {
        string path = Application.persistentDataPath + fileName;
        return File.Exists(path);
    }

    void WriteFile(string fileName, string content) {
        string path = Application.persistentDataPath + fileName;
        File.WriteAllText(path, content);
    }
    string ReadFile(string fileName) {
        string path = Application.persistentDataPath + fileName;
        return File.ReadAllText(path);
    }
}