using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireshow : MonoBehaviour {
    Animator animator;
    Animator heroAnimator {
        get { return transform.parent.GetComponent<Animator>(); }
    }
    GameObject hero {
        get { return transform.parent.gameObject; }
    }
    private Transform ts;
    private Transform herots {
        get { return transform.parent; }
    }
    private SpriteRenderer sr;
    private SpriteRenderer herosr {
        get { return transform.parent.GetComponent<SpriteRenderer>(); }
    }
    Vector3 offset1, offset2, position;
    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        ts = this.transform;
        sr = this.GetComponent<SpriteRenderer>();
        offset1 = ts.position - herots.position;
        offset2 = new Vector3 (ts.position.x - herots.position.x,herots.position.y-ts.position.y, 1);
	
        animator.SetInteger("stat", heroAnimator.GetInteger("stat"));
        
        if (!herosr.flipX)
	    {
	        ts.position = herots.position + offset1;
            sr.flipX = false;
        }
	    else
	    {
            if (name=="Fire(Clone)")
	            ts.position = herots.position - offset2;
            else
                ts.position = herots.position + new Vector3(0,0.5f,0);
            sr.flipX = true;
        }
    }
}
    