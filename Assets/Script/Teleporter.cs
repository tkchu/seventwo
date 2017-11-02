using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {
    MapEditor mapEditor;
	// Use this for initialization
	void Start () {
        mapEditor = FindObjectOfType<MapEditor>();
	}
	
	public void Trigger() {
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        soundManager.Play("teleporter");
        mapEditor.mapID += 1;
        mapEditor.Load();
    }
}
