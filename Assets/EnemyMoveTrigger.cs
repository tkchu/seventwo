using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyMoveTrigger : MonoBehaviour {
    public int mapID;

    public GameObject[] willMoveEnemeys;

    public int[] willMoveEnemyXY;

    Map map;
    private void Start() {
        map = FindObjectOfType<Map>();
    }

    public bool save = false;

    private void Update() {
#if UNITY_EDITOR

        if (save)
        {
            save = false;
            Save();
        }
#endif
    }
#if UNITY_EDITOR
    public void Save() {
        mapID = FindObjectOfType<MapEditor>().mapID;
        List<int> willMoveList = new List<int>();

        foreach (GameObject g in willMoveEnemeys) {
            int[] pos = map.GetItemPos(g);
            willMoveList.Add(pos[0]);
            willMoveList.Add(pos[1]);
        }
        willMoveEnemyXY = willMoveList.ToArray();
        GameObject manager = transform.parent.gameObject;
        PrefabUtility.ReplacePrefab(manager, PrefabUtility.GetPrefabParent(manager), ReplacePrefabOptions.ConnectToPrefab);
    }
#endif
    public void Load() {
        StartCoroutine(LoadC());
    }

    IEnumerator LoadC() {
        yield return new WaitForSeconds(0.25f);
        List<GameObject> temp = new List<GameObject>();
        for(int i =0;i<willMoveEnemyXY.Length;i+=2) {
            GameObject g = map.GetGameObjectAt(willMoveEnemyXY[i], willMoveEnemyXY[i + 1]);
            if(g && g != map.wallPrefab) {
                temp.Add(map.GetGameObjectAt(willMoveEnemyXY[i], willMoveEnemyXY[i + 1]));
            }
        }
        willMoveEnemeys = temp.ToArray();
        foreach(GameObject g in willMoveEnemeys) {
            if (g && g.GetComponent<DiagonalMove>()) {
                g.GetComponent<DiagonalMove>().enabled = false;
            }else if (g && g.GetComponent<LameMove>()) {
                g.GetComponent<LameMove>().enabled = false;
            }else if (g && g.GetComponent<NormalMove>()) {
                g.GetComponent<NormalMove>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(! (other.tag == "Player")) {
            return;
        }
        foreach (GameObject g in willMoveEnemeys) {
            if (g.GetComponent<DiagonalMove>()) {
                g.GetComponent<DiagonalMove>().enabled = true;
            } else if (g.GetComponent<LameMove>()) {
                g.GetComponent<LameMove>().enabled = true;
            } else if (g.GetComponent<NormalMove>()) {
                g.GetComponent<NormalMove>().enabled = true;
            }
        }
        gameObject.SetActive(false);
    }
}

