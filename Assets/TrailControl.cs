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

    public Color orangeGun;
    public Color redGun;
    public Color blueGun;
    public Color greenGun;
	// Update is called once per frame
	void Update () {
        trail.sortingOrder = playersprite.sortingOrder;
        var a=playeranimator.GetInteger("stat");
        switch (a)
        {
            case 0: trail.startColor = trail.endColor = orangeGun;
                break;
            case 1:
                trail.startColor = trail.endColor = redGun;
                break;
            case 2:
                trail.startColor = trail.endColor = blueGun;
                break;
            case 3:
                trail.startColor = trail.endColor = greenGun;
                break;
        }



        
	}
}
