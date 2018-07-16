using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MapEditor : MonoBehaviour {
    //编辑地图信息
    //地图分两层,地面层和地上层（包括墙）
    public int mapID;
    public bool save = false;
    public bool load = false;
    public int nextMapID;

    public GameObject basicTilePRefab;
    public Vector2 mapSize;
    public Vector2 tileSize;
    public Vector3 leftBottomPos;

    [Space]
    public GameObject[] groundPrefabs;
    public GameObject[] itemPrefabs;
    public GameObject[] movePrefabs;
    public GameObject[] decoratePrefabs;
    [Space]
    public GameObject[,] basicMap;

    IEnumerator Start() {
        //Debug.Log(Application.persistentDataPath);
        if (ExistFile("map" + mapID.ToString())) {
            SetMapIDAsLastPlayed();
            yield return new WaitForSeconds(0f);
            Load();
        } else {
            basicMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
            for (int i = 0; i < mapSize.x; i++) {
                for (int j = 0; j < mapSize.y; j++) {
                    Vector3 pos = new Vector3(i * tileSize.x, j * tileSize.y, 0) + leftBottomPos;
                    GameObject g = Instantiate(basicTilePRefab, pos, Quaternion.identity, transform);
                    basicMap[i, j] = g;
                }
            }
            GetComponent<Map>().groundMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
            GetComponent<Map>().itemMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
            GetComponent<Map>().decorateMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
        }
    }
    

    void MouseDown(BasicTile bt) {
        if (release)
            return;
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
        } else if (Input.GetKey(KeyCode.Z))
        {
            //眼睛、影子
            collectionTo = GetComponent<Map>().decorateMap;
            prefabs = decoratePrefabs;
        }

        else {
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
            Vector3 pos = basicMap[xy[0], xy[1]].transform.localPosition;
            if (prefabs == movePrefabs) {
                pos += new Vector3(0, tileSize.y / 3, 0);
            }
            GameObject newOne = Instantiate(prefabs[prefabIndex], pos, prefabs[prefabIndex].transform.rotation, transform);
            newOne.name = prefabs[prefabIndex].name;
            collectionTo[xy[0], xy[1]] = newOne;
        }
        GetComponent<Map>().UpdateSortOrder();
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
        GameObject[,] decorateMap = GetComponent<Map>().decorateMap;
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
        foreach (GameObject g in decorateMap)
        {
            if (g != null)
            {
                value += g.name + ",";
            }
            else
            {
                value += "null" + ",";
            }
        }
        WriteFile(key, value);
    }
    private void SetMapIDAsLastPlayed() {
        int lastPlayedLevel = PlayerPrefs.GetInt("lastPlayedLevel", -1);
        if (lastPlayedLevel != -1) {
            mapID = lastPlayedLevel;
        }
    }
    public void Load() {

        FindObjectOfType<Map>().playerPos = new int[] { -4, -4 };
        foreach(Transform t in transform) {
            Destroy(t.gameObject);
        }

        PlayerPrefs.SetInt("lastPlayedLevel", mapID);
        if (mapID == 22 && SceneManager.GetActiveScene().name != "boss") {
            SceneManager.LoadScene("boss");
        }
        else if (mapID != 22 && SceneManager.GetActiveScene().name == "boss")
        {
            SceneManager.LoadScene("levelEditor");
        }
        //Player.revivelife = 0f;
        string key = "map" + mapID.ToString();
        string value = ReadFile(key);
        string[] strs = value.Split(',');
        mapSize = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
        basicMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
        if (release) {
            basicTilePRefab.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        for (int i = 0; i < mapSize.x; i++) {
            for (int j = 0; j < mapSize.y; j++) {
                Vector3 pos = new Vector3(i * tileSize.x, j * tileSize.y, 0) + leftBottomPos;
                GameObject g = Instantiate(basicTilePRefab, pos, Quaternion.identity, transform);
                basicMap[i, j] = g;
            }
        }
        GameObject[,] groundMap = GetComponent<Map>().groundMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
        GameObject[,] itemMap = GetComponent<Map>().itemMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
        GameObject[,] decorateMap = GetComponent<Map>().decorateMap = new GameObject[(int)(mapSize.x), (int)(mapSize.y)];
        //FindObjectOfType<MapMist>().Init();

        int istr = 2;
        //处理地板
        for (int i = 0; i < groundMap.GetLength(0); i++) {
            for (int j = 0; j < groundMap.GetLength(1); j++) {
                for (int pi = 0; pi < groundPrefabs.Length; pi++) {
                    if (groundPrefabs[pi].name == strs[istr]) {
                        var temp = groundPrefabs[pi];
                        var temp2 = basicMap[i, j];
                        GameObject newOne = Instantiate(groundPrefabs[pi], basicMap[i, j].transform.localPosition, Quaternion.identity, transform);
                        newOne.name = strs[istr];
                        groundMap[i, j] = newOne;
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
                        GameObject newOne = Instantiate(itemPrefabs[pi], basicMap[i, j].transform.localPosition, itemPrefabs[pi].transform.rotation, transform);
                        newOne.name = strs[istr];
                        itemMap[i, j] = newOne;
                        break;
                    }
                }
                for (int pi = 0; pi < movePrefabs.Length; pi++) {
                    if (movePrefabs[pi].name == strs[istr]) {
                        GameObject newOne = Instantiate(movePrefabs[pi], basicMap[i, j].transform.localPosition + new Vector3(0,tileSize.y/3,0), movePrefabs[pi].transform.rotation, transform);
                        newOne.name = strs[istr];
                        itemMap[i, j] = newOne;
                        break;
                    }
                }
                istr += 1;
            }
        }
        for (int i = 0; i < decorateMap.GetLength(0); i++)
        {
            for (int j = 0; j < decorateMap.GetLength(1); j++)
            {
                if (istr >= strs.Length)
                    continue;
                for (int pi = 0; pi < decoratePrefabs.Length; pi++)
                {
                    if (decoratePrefabs[pi].name == strs[istr])
                    {
                        GameObject newOne = Instantiate(decoratePrefabs[pi], basicMap[i, j].transform.localPosition, Quaternion.identity, transform);
                        newOne.name = strs[istr];
                        decorateMap[i, j] = newOne;
                        break;
                    }
                }
                istr += 1;
            }
        }
        GetComponent<Map>().UpdateSortOrder();
        FindObjectOfType<CameraCenter>().Trigger();
        if (FindObjectOfType<EnemyTriggerManager>()) {
            FindObjectOfType<EnemyTriggerManager>().Load();
        }
        GetComponent<Map>().lastHit = 0.5f;
        FindObjectOfType<Radar>().Initial();
    }

    public bool release = false;
    bool ExistFile(string fileName) {
        if (release) {
            return true;
        }
        string path = Application.persistentDataPath + fileName;
        return File.Exists(path);
    }

    void WriteFile(string fileName, string content) {
        if (release)
            return;
        string path = Application.persistentDataPath + fileName;
        File.WriteAllText(path, content);
    }
    string ReadFile(string fileName) {
        if (release) {
            string filePath = "Maps/" + fileName;Debug.Log(filePath);
            TextAsset ta = Resources.Load<TextAsset>(filePath);
            return ta.text;
        }

        string path = Application.persistentDataPath + fileName;
        return File.ReadAllText(path);
    }


}
