﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorld : MonoBehaviour {
    public float gridSize = 0.5f;
    public GridItem[][] allItems;
    private int min_x = 0;
    private int min_y = 0;
    private int max_x = 0;
    private int max_y = 0;

	// Use this for initialization
	void Awake () {
        Flush();

        for (int i = 0; i < this.allItems.Length; i++) {
            foreach (GridItem item in this.allItems[i]) {
                if (item != null) {
                    item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y,  - 1 + i * 0.1f);
                }
            }
        }

    }

    public void Start() {
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("ground");
        foreach (GameObject ground in grounds) {
            float x = ground.transform.position.x;
            float pos_x = ((int)(Mathf.Abs(x) / gridSize + 0.5)) * (x > 0 ? 1 : -1) * gridSize;
            float y = ground.transform.position.y;
            float pos_y = ((int)(Mathf.Abs(y) / gridSize + 0.5)) * (y > 0 ? 1 : -1) * gridSize;

            ground.transform.position = new Vector3(pos_x, pos_y-0.1f, 1);
        }
    }

    private void Update() {
        GridItem[][] temp = this.allItems;
        return;
    }

    public void Flush() {
        GridItem[] items = FindObjectsOfType<GridItem>();
        Dictionary<GridItem, int> allPos_x = new Dictionary<GridItem, int>();
        Dictionary<GridItem, int> allPos_y = new Dictionary<GridItem, int>();

        foreach (GridItem item in items) {
            float x = item.transform.position.x;
            float y = item.transform.position.y;
            int pos_x = ((int)(Mathf.Abs(x) / gridSize + 0.5)) * (x > 0 ? 1 : -1);
            int pos_y = ((int)(Mathf.Abs(y) / gridSize + 0.5)) * (y > 0 ? 1 : -1);
            float xMin = pos_x * gridSize;
            float yMin = pos_y * gridSize;
            item.transform.position = new Vector3(xMin, yMin, item.transform.position.z);
            allPos_x.Add(item, pos_x);
            allPos_y.Add(item, pos_y);

            this.min_x = Mathf.Min(pos_x, this.min_x);
            this.min_y = Mathf.Min(pos_y, this.min_y);
            this.max_x = Mathf.Max(pos_x, this.max_x);
            this.max_y = Mathf.Max(pos_y, this.max_y);
        }

        int size_x = this.max_x - this.min_x + 1;
        int size_y = this.max_y - this.min_y + 1;

        this.allItems = new GridItem[size_y][];
        for (int i = 0; i < size_y; i++) {
            this.allItems[i] = new GridItem[size_x];
        }

        foreach (GridItem item in items) {
            this.allItems[allPos_y[item] - min_y][allPos_x[item] - min_x] = item;
        }

        if (FindObjectOfType<Player>()) {
            Transform player = FindObjectOfType<Player>().transform;
            int player_i = GridItem_y(player.GetComponent<GridItem>());
            player.position = new Vector3(player.position.x, player.position.y, -1 + player_i * 0.1f);
        }

    }
    public int GridItem_x(GridItem item) {
        float x = item.transform.position.x;
        return ((int)(Mathf.Abs(x) / gridSize + 0.5)) * (x > 0 ? 1 : -1) - this.min_x;
    }
    public int GridItem_y(GridItem item) {
        float y = item.transform.position.y;
        return ((int)(Mathf.Abs(y) / gridSize + 0.5)) * (y > 0 ? 1 : -1) - this.min_y;
    }
    public GridItem GridItemAt(int x, int y) {
        if(y >= this.allItems.Length || y < 0) {
            return null;
        }
        if (x >= this.allItems[y].Length || x<0) {
            return null;
        }
        return this.allItems[y][x];
    }

    public Vector3 Go(GridItem item, Vector2 direction) {
        int x_before = GridItem_x(item);
        int y_before = GridItem_y(item);
        int x = x_before + (int)direction.x;
        int y = y_before + (int)direction.y;
        if (x >= this.allItems[0].Length) {
            x = this.allItems[0].Length - 1;
        }else if(x< 0) {
            x = 0;
        }
        if(y>= this.allItems.Length) {
            y = this.allItems.Length - 1;
        }else if (y < 0) {
            y = 0;
        }

        GridItem itemMeet = this.allItems[y][x];
        if (itemMeet != null) {
            item.SendMessage("Meet", itemMeet);
            itemMeet.SendMessage("Meet", item);
        }

        this.allItems[y][x] = item;
        this.allItems[y_before][x_before] = null;

        return new Vector3((x + min_x) * gridSize, (y + min_y) * gridSize, item.transform.position.z);     
    }

    public GridItem[] FindGridItemInRange(int pos_x, int pos_y, Vector2 direction, int range) {
        List<GridItem> result = new List<GridItem>();
        for (int i = 1; i <= range; i++) {
            GridItem temp = GridItemAt(pos_x + i * (int)direction.x, pos_y + i * (int)direction.y);
            if (temp != null && temp.gridItemType == GridItemType.wall) {
                break;
            }
            result.Add(temp);
        }
        return result.ToArray();
    }

    public void Destroy(GridItem item) {
        int x = GridItem_x(item);
        int y = GridItem_y(item);
        this.allItems[y][x] = null;
    }


    public int tempPos_x = 0;
    public int tempPos_y = 0;
    public GridItem tempItem;
    public bool p = false;
    private void LateUpdate() {
        if (p) {
            Print();
        }
        p = false;
        tempItem = GridItemAt(tempPos_x, tempPos_y);
    }
    public void Print() {
        foreach(GridItem[] items in this.allItems) {
            string line = "";
            foreach(GridItem item in items) {
                if(item!= null) {
                    line += ((int)(item.gridItemType)).ToString() + ",";
                }else {
                    line += "__, ";
                }
            }
            Debug.Log(line);
        }
        Debug.Log("===========================");
    }
}