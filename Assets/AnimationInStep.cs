using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInStep : MonoBehaviour {
    Animator parentani,ani;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
       parentani= transform.parent.parent.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {/*
        var param = parentani.parameters;
        foreach( AnimatorControllerParameter a in param)
        {
            
            ani.SetBool(a.name, parentani.GetBool(a.name));
            //ani.SetFloat(a.name, parentani.GetFloat(a.name));
            ani.SetInteger(a.name, parentani.GetInteger(a.name));
        }*/
        ani.SetBool("isMoving", parentani.GetBool("isMoving"));
        ani.SetBool("isReady", parentani.GetBool("isReady"));
        ani.SetBool("isAttacking", parentani.GetBool("isAttacking"));
    }
}
