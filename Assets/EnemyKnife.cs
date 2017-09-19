using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyKnife : MonoBehaviour
{
    private SpriteRenderer parentsr;
    private Transform ts,parentTransform ;
    private Vector3 offset1, offset2;
	// Use this for initialization
	void Start ()
	{
	    ts = transform;
	    parentTransform = ts.parent;
	    offset1 = ts.position-parentTransform.position; 
        offset2 = new Vector3(ts.position.x - parentTransform.position.x, parentTransform.position.y - ts.position.y, 0);
        parentsr = parentTransform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (parentsr.flipX)
	    {
	        ts.position = parentTransform.position - offset2;
	        ts.localScale = new Vector3(-1, 1, 1);
	    }
	    else
	    {
	        ts.position = parentTransform.position + offset1;
            ts.localScale=new Vector3(1,1,1);
	    }
		
	}
}
