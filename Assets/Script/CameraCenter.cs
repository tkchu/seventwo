using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour {
    public void Trigger() {
        MapEditor mapEditor = FindObjectOfType<MapEditor>();
        transform.position = mapEditor.basicMap[(int)mapEditor.mapSize.x / 2, (int)mapEditor.mapSize.y / 2].transform.position + Vector3.back * 10;
    }
}
