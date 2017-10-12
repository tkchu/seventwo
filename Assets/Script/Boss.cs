﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
    public int hp = 5;
    public bool tick;
    public Map map;
    public MapEditor mapEditor;

    public int[][][] enemypoint;
    public int[][] weaponpoint;
    public int[][] bombpoint;
    public GameObject[] bomb;
    public GameObject[] enemy;
    public GameObject[] weapon;

    private void Start() {
        map = FindObjectOfType<Map>();
        mapEditor = FindObjectOfType<MapEditor>();
        weaponpoint = new int[][] { new int[] { 9, 2 } ,new int[] { 2,4},new int[] { 17, 6 } };
        bombpoint = new int[][] { new int[] { 5, 3 }, new int[] { 6, 6 }, new int[] { 12, 2 }, new int[]{15, 4}};
        enemypoint[0] = new int[3][] { new int[2] { 1, 1 }, new int[2] { 1, 2 }, new int[2] { 1, 3 }};
        enemypoint[1] = new int[4][] { new int[2] { 1, 7 }, new int[2] { 1, 8 }, new int[2] { 2, 7 },new int[2] { 2, 8 } };
        enemypoint[2] = new int[2][] { new int[2] { 6, 1 }, new int[2] { 7, 1 } };
        enemypoint[3] = new int[][] { new int[] { 18, 7 }, new int[] { 18, 8 }, new int[] { 19, 7 }, new int[] { 19, 8 } };
        enemypoint[4] = new int[][] { new int[] { 18, 1 }, new int[] { 19, 1 } };


    }
    private void Update()
    {
        if (tick)
        {
            OneAction();
            tick = false;
        }
    }

    void Create(int[] xy,GameObject[] prefab)
    {
        GameObject old = map.itemMap[xy[0], xy[1]];
        Vector3 pos = mapEditor.basicMap[xy[0], xy[1]].transform.localPosition;
        pos += new Vector3(0, mapEditor.tileSize.y / 3, 0);
        if (old != null && old != gameObject && old.tag != "Player")
            Destroy(old);
        var pb = prefab[Random.Range(0, prefab.Length)];
        map.itemMap[xy[0], xy[1]] = Instantiate(pb, pos,pb.transform.rotation);
    }

    public void OneAction() {

        if (GameObject.FindWithTag("enemy") == null)
            CreateRandomBomb();
        CreateRandomWeapon();
    }
    public void CreateRandomBomb()
    {
        GetComponent<Animator>().SetBool("left", true);
        foreach (int[] a in bombpoint)
        {
           Create(a,bomb);
        };
    }
    
    public void CreateRandomEnemy(int[][] en)
    {
        GetComponent<Animator>().SetBool("right", true);
        foreach (int[] a in en)
        {

            Create(a,enemy);
        };
    }
    public void CreateRandomWeapon()
    {
        foreach (int[] a in weaponpoint)
        {

            Create(a,weapon);
        };
    }



    /*
    public void CreatePresetBomb() {
        int[] bombXMayBe = new int[] { 6, 6, 6, 7, 8, 9, 9, 9 };
        int[] bombYMayBe = new int[] { 5, 4, 3, 3, 3, 3, 4, 5 };
        int indexStartIndex = Random.Range(0, bombXMayBe.Length);
        for (int i = 0; i < bombXMayBe.Length; i++) {
            int temp = (indexStartIndex + i) % bombXMayBe.Length;
            GridItem item_temp = gw.GridItemAt(bombXMayBe[temp], bombYMayBe[temp]);
            if (item_temp == null) {
                CreatePrefabAt(bomb, bombXMayBe[temp], bombYMayBe[temp]);
                break;
            }
        }
    }
    public void CreateRandomBomb() {
        int x = Random.Range(1, Xlength);
        int y = Random.Range(1, Ylength - 1);
        GridItem item = gw.GridItemAt(x, y);
        while (item != null) {
            x = Random.Range(1, Xlength);
            y = Random.Range(1, Ylength);
            item = gw.GridItemAt(x, y);
        }
        CreatePrefabAt(bomb, x, y);
    }

    public void CreateRandom1() {
        int x = Random.Range(1, Xlength);
        int y = 5;//Random.Range(1, Ylength - 1);
        GridItem item = gw.GridItemAt(x, y);
        while (item != null) {
            x = Random.Range(1, Xlength);
            y = 5;//Random.Range(1, Ylength);
            item = gw.GridItemAt(x, y);
        }
        CreatePrefabAt(moveBomb, x, y);
    }
    */
    private void CreatePrefabAt(GameObject prefab, int x, int y) {
        Vector3 pos = mapEditor.basicMap[x, y].transform.localPosition;
        
            pos += new Vector3(0,mapEditor.tileSize.y / 3, 0);
        
        Instantiate(prefab,pos, Quaternion.identity, transform);
        
    }

    public GameObject flamePrefab;
    public GameObject[] parts;
    public void OneHit() {
        Debug.Log("hp--");
        GetComponent<Animator>().SetBool("ishited", true);
        hp -= 1;
        if (hp <= 0) {
            StartCoroutine(Explode());
            Debug.Log("Winner!");
        }
    }

    IEnumerator Explode() {
        parts=GameObject.FindGameObjectsWithTag("boss");
        int time = 10;
        while (time>0) {
            time -= 1;
            Vector3 position = parts[Random.Range(0, parts.Length)].transform.position;
            Instantiate(flamePrefab, position + Vector3.back * 50, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        SceneManager.LoadScene("Ending");
    }
}