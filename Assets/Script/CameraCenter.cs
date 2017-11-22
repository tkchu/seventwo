using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour {
    public void Trigger() {
        MapEditor mapEditor = FindObjectOfType<MapEditor>();
        transform.position = mapEditor.basicMap[0,0].transform.position+
            new Vector3(mapEditor.mapSize.x*mapEditor.tileSize.x/2f, mapEditor.mapSize.y * mapEditor.tileSize.y/2+0.25f*mapEditor.tileSize.y,0)+Vector3.back*10;
    }
}
