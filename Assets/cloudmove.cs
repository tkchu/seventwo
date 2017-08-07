using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cloudmove : MonoBehaviour {
    public float movetime = 9f;
    public float dis = 8f;
    // Use this for initialization
    void Start() {
       
        // Debug.Log("CloudeStart");
        //this.transform.Rotate(0, 0, Random.Range(0, 360f));
        DOTween.Sequence().Append(
        gameObject.transform.DOMoveX(dis, movetime)
        //gameObject.transform.DOMoveX(new cloudcontroller().dis.x*2.4f, movetime)
        ).AppendInterval(1f).AppendCallback(()=> { //Debug.Log("cloudmove");
            GameObject.Destroy(this.gameObject); });
    }
        // Update is called once per frame
        void Update() {

        }
 }

