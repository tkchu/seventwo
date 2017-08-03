﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorld : MonoBehaviour {
    public float gridSize = 0.5f;
    private GridItem[][] allItems;
    private int min_x = 0;
    private int min_y = 0;
    private int max_x = 0;
    private int max_y = 0;

	// Use this for initialization
	void Awake () {
        Flush();
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
        int x = GridItem_x(item) + (int)direction.x;
        int y = GridItem_y(item) + (int)direction.y;
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

        return new Vector3((x + min_x) * gridSize, (y + min_y) * gridSize, item.transform.position.z);     
    }
}