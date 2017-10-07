using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponshow : MonoBehaviour {
    Animator animator,heroAnimator;
    GameObject hero;
    private Transform ts, herots;
    private SpriteRenderer sr, herosr;
    Vector3 offset1,offset2,position;
    // Use this for initialization
    void Start () {
       animator = this.GetComponent<Animator>();
        hero = GameObject.FindGameObjectWithTag("Player");
        heroAnimator = hero.GetComponent<Animator>();
        ts = this.transform;
        herots = hero.transform;
        sr = this.GetComponent<SpriteRenderer>();
        herosr = hero.GetComponent<SpriteRenderer>();
        offset1 = ts.position - herots.position;
        offset2 = new Vector3 (ts.position.x - herots.position.x,herots.position.y-ts.position.y, 1);
	}
	
	// Update is called once per frame
	void Update () {
        hero = GameObject.FindGameObjectWithTag("Player");
        heroAnimator = hero.GetComponent<Animator>();
        herots = hero.transform;
        herosr = hero.GetComponent<SpriteRenderer>();

        animator.SetInteger("stat", heroAnimator.GetInteger("stat"));
        if(heroAnimator.GetBool("isShoot"))
            animator.SetBool("isShoot",true);
	    if (!herosr.flipX)
	    {
	        ts.position = herots.position + offset1;
	        ts.localScale = new Vector3(1, 1, 1);
	    }
	    else
	    {
	        ts.position = herots.position - offset2;
            ts.localScale=new Vector3(-1,1,1);
	    }
    }
}
    