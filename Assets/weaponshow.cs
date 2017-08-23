using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponshow : MonoBehaviour {
    Animator animator;
    GameObject hero;
    Vector3 offset1,offset2,position;
    // Use this for initialization
    void Start () {
       animator = this.GetComponent<Animator>();
       hero = GameObject.Find("Hero");
        offset1 = this.GetComponent<Transform>().position - hero.GetComponent<Transform>().position;
        offset2 = new Vector3 (this.GetComponent<Transform>().position.x - hero.GetComponent<Transform>().position.x,hero.GetComponent<Transform>().position.y-this.GetComponent<Transform>().position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetInteger("stat", hero.GetComponent<Animator>().GetInteger("stat"));
        if (!hero.GetComponent<SpriteRenderer>().flipX) { this.GetComponent<Transform>().position = hero.GetComponent<Transform>().position + offset1; this.GetComponent<SpriteRenderer>().flipX = false; }
        else { this.GetComponent<Transform>().position = hero.GetComponent<Transform>().position - offset2; this.GetComponent<SpriteRenderer>().flipX = true; }
    }
}
    