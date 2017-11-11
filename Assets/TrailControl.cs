using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailControl : MonoBehaviour {
    Animator playeranimator;
    SpriteRenderer playersprite;
    TrailRenderer trail;
	// Use this for initialization
	void Start () {
        trail = GetComponent<TrailRenderer>();
        playeranimator = transform.parent.gameObject.GetComponent<Animator>();
        playersprite = transform.parent.gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        trail.sortingOrder = playersprite.sortingOrder;
        var a=playeranimator.GetInteger("stat");
        switch (a)
        {
            case 0: trail.startColor = trail.endColor = Color.black;
                break;
            case 1:
                trail.startColor = trail.endColor = Color.red;
                break;
            case 2:
                trail.startColor = trail.endColor = Color.blue;
                break;
            case 3:
                trail.startColor = trail.endColor = Color.green;
                break;
        }



        
	}
}
