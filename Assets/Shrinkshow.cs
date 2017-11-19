using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shrinkshow : MonoBehaviour {
    public bool trigger;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(trigger)
        {
            trigger = false;
            transform.DOScaleY(0, 0.5f);
        }
    }
}
