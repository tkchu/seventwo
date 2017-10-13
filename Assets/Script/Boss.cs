﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
    public int hp = 5;
    public Map map;
    public MapEditor mapEditor;
    public bool unbeatable = false;
    public int[][][] enemypoint=new int[5][][];
    public int[][] weaponpoint;
    public int[][] bombpoint;
    public GameObject[] movebomb;
    public GameObject[] bomb;
    public GameObject[] enemy;
    public GameObject[] weapon;

    private void Start() {
        map = FindObjectOfType<Map>();
        mapEditor = FindObjectOfType<MapEditor>();
        weaponpoint = new int[][] { new int[] { 9, 2 } ,new int[] { 2,4},new int[] { 17, 6 } };
        bombpoint = new int[][] { new int[] { 5, 3 }, new int[] { 6, 6 }, new int[] { 12, 2 }, new int[]{15, 4}};
        
        enemypoint[0] = new int[][] { new int[] { 1, 7 }, new int[] { 1, 8 }, new int[] { 2, 7 },new int[] { 2, 8 } };
        enemypoint[1] = new int[][] { new int[] { 6, 1 }, new int[] { 7, 1 } };
        enemypoint[2] = new int[][] { new int[] { 18, 7 }, new int[] { 18, 8 }, new int[] { 19, 7 }, new int[] { 19, 8 } };
        enemypoint[3] = new int[][] { new int[] { 1, 1 }, new int[] { 1, 2 }, new int[] { 1, 3 } };
        enemypoint[4] = new int[][] { new int[] { 18, 1 }, new int[] { 19, 1 } };
       

    }
    private void Update()
    {
    }

    void Create(int[] xy,GameObject[] prefab)
    {
        GameObject old = map.itemMap[xy[0], xy[1]];
        if (old!=null&&old.tag == "Player")
            return;
        Vector3 pos = mapEditor.basicMap[xy[0], xy[1]].transform.localPosition;
        pos += new Vector3(0, mapEditor.tileSize.y / 3, 0);
        if (old != null && old != gameObject && old.tag != "Player")
            Destroy(old);
        var pb = prefab[Random.Range(0, prefab.Length)];
        map.itemMap[xy[0], xy[1]] = Instantiate(pb, pos,pb.transform.rotation);
    }

    public void OneAction() {
        if (GameObject.FindWithTag("enemy") != null)
            return;
        if (!unbeatable)
        {
            for (int i = 0; i < enemypoint.Length; ++i)
                CreateRandomEnemy(enemypoint[i], movebomb);
            CreateRandomBomb();

        }

        else
        {
            for(int i=0;i<enemypoint.Length;++i)
                CreateRandomEnemy(enemypoint[i],enemy);
            CreateRandomWeapon();
            unbeatable = false;
        }
    }
    public void CreateRandomBomb()
    {
        GetComponent<Animator>().SetBool("left", true);
        foreach (int[] a in bombpoint)
        {
           Create(a,bomb);
        };
    }
    
    public void CreateRandomEnemy(int[][] en,GameObject[] pre)
    {
        GetComponent<Animator>().SetBool("right", true);
        int num=Random.Range(1, 4);
        foreach (int[] a in en)
        {
            if (num == 0) return;
            Create(a,pre);
            num--;
        };
    }
    public void CreateRandomWeapon()
    {
        foreach (int[] a in weaponpoint)
        {

            Create(a,weapon);
        };
    }


    
    private void CreatePrefabAt(GameObject prefab, int x, int y) {
        Vector3 pos = mapEditor.basicMap[x, y].transform.localPosition;
        
            pos += new Vector3(0,mapEditor.tileSize.y / 3, 0);
        
        Instantiate(prefab,pos, Quaternion.identity, transform);
        
    }

    public GameObject flamePrefab;
    public GameObject[] parts;
    public void OneHit() {
        if(GameObject.FindWithTag("enemy"))
            unbeatable = true;
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