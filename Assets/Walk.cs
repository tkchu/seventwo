using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {
    public  bool flag = true,flag1=true;
    double time=0,lasttime;
    Vector3 pos;
    // Use this for initialization
    void Start() {
        pos = transform.localPosition;
        lasttime = Time.fixedDeltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.fixedDeltaTime;
        if(flag)
        transform.localPosition += new Vector3(0.75f * Time.fixedDeltaTime, 0, 0);
        else transform.localPosition += new Vector3(1.5f * Time.fixedDeltaTime, 0, 0);
        if (time >= 2&&flag)
        {
            if (flag1)
            {

                time = 0;
                transform.localPosition = pos;
            }
        }
        if (time >= 4)
        {
            if (!flag)
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
        
    }
}
