using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour {
    Animator ani;
    float a=0,b;

    // Use this for initialization
    void Start () {
        ani = this.GetComponent<Animator>();
        b = Random.Range(3, 7);
	}
	
	// Update is called once per frame
	void Update () {
        if (!ani.GetBool("bool"))
        {
         a += Time.unscaledDeltaTime;
            if (a > b)
            {
                a = 0;
                b = Random.Range(5f, 12f);
                ani.SetBool("bool", true);
            }
        }
        
    }
}
