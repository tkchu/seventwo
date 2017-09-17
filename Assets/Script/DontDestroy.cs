using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static bool _once=true;
	// Use this for initialization
	void Start () {
	    if (_once)
	    {

	        DontDestroyOnLoad(gameObject);
	        _once = false;
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
