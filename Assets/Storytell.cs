using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Storytell : MonoBehaviour {
    public bool tell=false;
    public GameObject s1, s2, s3, s4, s5,t1,t2,t4,t5,bt,bb;
    Vector3 ts1, ts2, ts4, ts5;
	// Use this for initialization
	void Start () {
        ts1 = t1.transform.position;
        ts2 = t2.transform.position;
        ts4 = t4.transform.position;
        ts5 = t5.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (tell)
        {
            tell = false;

            DOTween.Sequence().Append(s1.transform.DOMove(ts1, 3f))
                .Append(s2.transform.DOMove(ts2, 3f))
                .Append(s3.GetComponent<SpriteRenderer>().DOFade(1, 3f))
                .Append(s4.transform.DOMove(ts4, 3f))
                .Append(s5.transform.DOMove(ts5, 3f))
                .Append(bt.GetComponent<SpriteRenderer>().DOFade(1, 2f))
                .AppendCallback(() =>
                {
                    Destroy(s1);
                    Destroy(s2);
                    Destroy(s3);
                    Destroy(s4);
                    Destroy(s5);
                    Destroy(bb);
                })
                .Append(bt.GetComponent<SpriteRenderer>().DOFade(0,2f));
        }
    }
}
