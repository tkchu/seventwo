﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
    public int hp = 5;

    public GridWorld gw;
    public int Ylength;
    public int Xlength;

    public GameObject bomb;
    public GameObject spine;
    public GameObject moveBomb;
    public GameObject knfieEnemy;

    private void Start() {
        gw = FindObjectOfType<GridWorld>();
        Ylength = gw.allItems.Length;
        Xlength = gw.allItems[0].Length;
    }

    public int count = 0;
    public bool createA = true;
    public void OneAction() {
        count += 1;
        if(count % 3 == 0) {
            if (createA) {
                GetComponent<Animator>().SetBool("left", true);
                if(count % 10 == 0) {
                    CreatePresetBomb();
                }
                for (int i = 0; i < 4; i++) {
                    CreateRandomBomb();
                }
            } else {
                GetComponent<Animator>().SetBool("right", true);
                for (int i = 0; i < 4; i++) {
                    CreateRandom1();
                }
            }
            createA = !createA;
        }
    }

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

    private void CreatePrefabAt(GameObject prefab, int x, int y) {
        float pos_x = -4.2f + x * gw.gridSize;
        float pos_y = -1.2f + y * gw.gridSize;
        Instantiate(prefab, new Vector3(pos_x, pos_y, -20), Quaternion.identity, transform);
        gw.Flush();
    }

    public GameObject flamePrefab;
    public Transform[] parts;
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
        int time = 10;
        while (time>0) {
            time -= 1;
            Vector3 position = parts[Random.Range(0, parts.Length)].position;
            Instantiate(flamePrefab, position + Vector3.back * 50, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        SceneManager.LoadScene("Ending");
    }
}