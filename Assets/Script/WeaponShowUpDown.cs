using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShowUpDown : MonoBehaviour
{
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
    void Start()
    {
        animator = this.GetComponent<Animator>();
        ts = this.transform;
        sr = this.GetComponent<SpriteRenderer>();
        offset1 = ts.position - herots.position;
        offset2 = new Vector3(ts.position.x - herots.position.x, 5*(herots.position.y - ts.position.y), 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (hero == null)
        {
            return;
        }
        animator.SetInteger("stat", heroAnimator.GetInteger("stat"));
        if (heroAnimator.GetBool("isShoot"))
        {

            animator.SetBool("isShoot", true);

           // Debug.Log("heroShoot!");
        }else
            animator.SetBool("isShoot", false);
        if (!herosr.flipX)
        {
            ts.position = herots.position + offset1;
            sr.flipX = false;
            //Debug.Log("BAD");
        }
        else
        {
            //Debug.Log(ts.localScale.ToString() + ts.name);
            ts.position = herots.position + offset2;
            sr.flipX = true;
            
        }
    }
}
