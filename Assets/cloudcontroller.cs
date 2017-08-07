using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudcontroller : MonoBehaviour {
    public GameObject locat1, locat2;
    public GameObject cloud1, cloud2;
    Vector3 leftup;
    Vector3 leftdown;
    float time = 0;
	// Use this for initialization
	void Start () {
        spawn();
         leftup = locat1.transform.position;
         leftdown = locat2.transform.position;
        
	}
    
	void spawn()
    {
        float c = Random.Range(0, 2);
        float a = Random.Range(0, leftup.y - leftdown.y);
        GameObject.Instantiate(c==1?cloud1:cloud2, leftdown+new Vector3(0,a,0),new Quaternion());
       // Debug.Log(b);

    }
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > 20)
        {
            spawn();
            time = 0;
        }
	}
}
