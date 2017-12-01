﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
    public int hp = 5;
    public Map map;
    public MapEditor mapEditor;
    public bool unbeatable = false,callenemy=false;
    public int[][][] enemypoint=new int[4][][];
    public int[][] weaponpoint;
    public int[][] bombpoint;
    public GameObject[] movebomb;
    public GameObject[] bomb;
    public GameObject[] enemy;
    public GameObject[] weapon;
    public GameObject tele;
    public Color[] cl;
    public Animator shield;
    public int step = 0;
    public bool test = false;
    private void Start() {
        Destroy(GameObject.Find("gbgm"));
        shield = transform.Find("shield").GetComponent<Animator>();
        map = FindObjectOfType<Map>();
        mapEditor = FindObjectOfType<MapEditor>();
        mapEditor.mapID = 22;
        weaponpoint = new int[][] { new int[] { 10, 1 } ,new int[] { 6,4},new int[] { 14, 6 } };
        bombpoint = new int[][] { new int[] { 6, 5 }, new int[] { 13, 2 }, new int[] { 13, 5 }};
        
        enemypoint[0] = new int[][] { new int[] { 1, 2 } };
        enemypoint[1] = new int[][] { new int[] { 1, 8 }, new int[] { 2, 8 } };
        enemypoint[2] = new int[][] { new int[] { 18, 1 }, new int[] { 19, 1 } };
        enemypoint[3] = new int[][] { new int[] { 19, 8 }};

    }
    private void Update()
    {
        if (test)
        {
            test = false;
            CreateRandomBomb();

        }
    }

    IEnumerator Create(int[] xy,GameObject[] prefab)
    {
        GameObject old = map.itemMap[xy[0], xy[1]];
        if (old != null && old != gameObject)
        {
            Debug.Log("1");
            yield break;
        }
        Vector3 pos = mapEditor.basicMap[xy[0], xy[1]].transform.localPosition;
        pos += new Vector3(0, mapEditor.tileSize.y / 3, 0);
        
        var pb = prefab[Random.Range(0, prefab.Length)];
        GameObject a= Instantiate(tele, pos-new Vector3(0, 0.25f * mapEditor.tileSize.y,0), Quaternion.identity);
        map.itemMap[xy[0], xy[1]] = a;
        if (prefab==enemy)a.GetComponent<SpriteRenderer>().color = cl[0];
        if(pb == weapon[0])a.GetComponent<SpriteRenderer>().color = cl[1];
        if (pb == weapon[1]) a.GetComponent<SpriteRenderer>().color = cl[2];
        if (pb == weapon[2]) a.GetComponent<SpriteRenderer>().color = cl[3];
        yield return new WaitForSeconds(0.35f);
        old = map.itemMap[xy[0], xy[1]];
        if (old != null && old.tag != "wall")
        {
            Debug.Log("2");
            yield break;
        }
        map.itemMap[xy[0], xy[1]] = Instantiate(pb, pos, pb.transform.rotation);
        //Debug.Log(xy[0].ToString() + ' ' + xy[1].ToString() + ' ' + pb.name);

    }

    public void OneAction() {
        if (hp <= 0) return;

        step += 1;
        if (step == 5) step = 0;

        if (hp >= 3&&step==0)
        {

            FindObjectOfType<SoundManager>().Play("bossattack");
            
            GetComponent<Animator>().SetBool("right", true);
            for (int i = 0; i < enemypoint.Length; ++i)
                CreateRandomMoveBomb(enemypoint[i]);
            CreateRandomBomb();
        }

        if (hp <= 2)
        {

            if (callenemy)
            {
                FindObjectOfType<SoundManager>().Play("bossmiss");
                //FindObjectOfType<SoundManager>().Play("bossattack");
                callenemy = false;
                GetComponent<Animator>().SetBool("left", true);
                for (int i = 0; i < enemypoint.Length; ++i)
                    CreateRandomEnemy(enemypoint[i]);
                CreateRandomWeapon();

            }

            else if (GameObject.FindWithTag("enemy") == null && step == 0)
            {

                FindObjectOfType<SoundManager>().Play("bossattack");
                if (GameObject.FindWithTag("enemy") != null)
                    return;
                GetComponent<Animator>().SetBool("right", true);
                for (int i = 0; i < enemypoint.Length; ++i)
                    CreateRandomMoveBomb(enemypoint[i]);
                CreateRandomBomb();
                unbeatable = false;

                shield.SetBool("unbeatable", false);
            }
            else
            {

            }
        }
    }
    public void CreateRandomBomb()
    {
        foreach (int[] a in bombpoint)
        {
           StartCoroutine( Create(a,bomb));
        };
    }
    
    public void CreateRandomEnemy(int[][] en)
    {
        int num=Random.Range(1, 3);
        int[][] tem = new int[en.Length][];
        en.CopyTo(tem, 0);
        int len = tem.Length;
        for(; len>num;len--)
        {
            tem[Random.Range(0, len)] = tem[len-1];
        }
        //Debug.Log(len);
        for (int i = 0; i < len; ++i)
            StartCoroutine( Create(tem[i], enemy));
    }
    public void CreateRandomMoveBomb(int[][] en)
    {
        int num = Random.Range(1, 3);
        int[][] tem = new int[en.Length][];
        en.CopyTo(tem, 0);
        int len = tem.Length;
        
        for (; len > num; len--)
        {
            tem[Random.Range(0, len)] = tem[len-1];
        }

        //Debug.Log("lenth is"+len);
        for (int i = 0; i < len; ++i)
        {
            //Debug.Log(tem[i][0].ToString() + ' ' + tem[i][1].ToString());
            StartCoroutine(Create(tem[i], movebomb));

        }
 
    }
    public void CreateRandomWeapon()
    {
        foreach (int[] a in weaponpoint)
        {

            StartCoroutine(Create(a, weapon));
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

        if (unbeatable)
            return;
        hp -= 1;
        Debug.Log("hp--");
        GetComponent<Animator>().SetBool("ishited", true);
        if (hp <= 0)
        {
            FindObjectOfType<Map>().GetComponent<Map>().enabled = false;
            StartCoroutine(Explode());
            Debug.Log("Winner!");
            return;
        }
        if (hp <= 2)
        {

        unbeatable = true;
        shield.SetBool("unbeatable", true);
        callenemy = true;
        }
        
    }

    IEnumerator Explode() {
        //FindObjectOfType<Map>().GetComponent<Map>().enabled = false;    
        GameObject.Find("GameBGM").SetActive(false);
        FindObjectOfType<SoundManager>().Play("bossfail");
        parts =GameObject.FindGameObjectsWithTag("boss");
        int time = 9;
        GetComponent<Animator>().SetBool("isdead", true);
        while (time>0) {
            time -= 1;
            Vector3 position = parts[Random.Range(0, parts.Length)].transform.position;
            Instantiate(flamePrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Ending");
    }
}