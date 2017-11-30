using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Endstory : MonoBehaviour {
    public GameObject p1,p2,p3,p4,p5,p6,p7,front,endlist;
    Sequence a;
	// Use this for initialization
	void Start () {

        a=DOTween.Sequence()
            .AppendInterval(1f).AppendCallback(() => { p1.transform.Find("mask").GetComponent<Shrinkshow>().trigger = true; })
            .AppendInterval(1f).AppendCallback(() => { p2.transform.Find("mask").GetComponent<Shrinkshow>().trigger = true; })
            .AppendInterval(2f).AppendCallback(() => { p3.transform.Find("mask").GetComponent<Shrinkshow>().trigger = true; })
            .AppendInterval(2f).AppendCallback(() => { p4.transform.Find("mask").GetComponent<Shrinkshow>().trigger = true; })
            .AppendInterval(3f).AppendCallback(() => { p5.transform.Find("mask").GetComponent<Shrinkshow>().trigger = true; })
            .AppendInterval(1f).AppendCallback(() => { p6.transform.DOScaleY(0.43f, 0.5f); })
            .AppendInterval(2f).AppendCallback(() => { p7.transform.Find("mask").GetComponent<Shrinkshow>().trigger = true; })
            .AppendInterval(1.5f).Append(front.GetComponent<SpriteRenderer>().DOFade(1,1f))
            ;


    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            a.Kill(); 
            DestroyImmediate(gameObject);

        }
    }

    private void OnDestroy()
    {
        endlist.SetActive(true);
    }
}
