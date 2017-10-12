﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
    public int hp = 5;
    public bool tick;
    public Map map;
    public MapEditor mapEditor;
    
    public GameObject[] enemypoint;
    public GameObject[] weaponpoint;
    public GameObject[] bombpoint;



    private void Start() {
        map = FindObjectOfType<Map>();
       
    }
    private void Update()
    {
        if (tick)
        {
            OneAction();
            tick = false;
        }
    }


    public void OneAction() {
        if (GameObject.FindWithTag("enemy") == null)
            CreateRandomBomb();
    }
    public void CreateRandomBomb()
    {
        GetComponent<Animator>().SetBool("left", true);
        foreach (GameObject a in bombpoint){
            a.SendMessage("Create");
        };
    }
    public void CreateRandomEnemy()
    {
        GetComponent<Animator>().SetBool("right", true);
        foreach (GameObject a in enemypoint)
        {
            a.SendMessage("Create");
        };
    }
    public void CreateRandomWeapon()
    {
        foreach (GameObject a in weaponpoint)
        {
            a.SendMessage("Create");
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