using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Storytell : MonoBehaviour {
    public bool tell=false;
    public GameObject s1, s2, s3, s4, s5,t1,t2,t4,t5,bt,bb,sbgm;
    Vector3 ts1, ts2, ts4, ts5;
    Sequence sq ;
    // Use this for initialization
    void Start () {
        //sq = DOTween.Sequence();
        ts1 = t1.transform.position;
        ts2 = t2.transform.position;
        ts4 = t4.transform.position;
        ts5 = t5.transform.position;

        PlayerPrefs.SetInt("lastPlayedLevel", 0);

    }
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode == KeyCode.Escape)
        {
            sq.Kill();
            sbgm.GetComponent<AudioSource>().Stop();
            SceneManager.LoadScene("levelEditor");
        }
        
    }
    // Update is called once per frame
    void Update () {
        if (tell)
        {
            tell = false;
            sbgm.SetActive(true);
            sq=DOTween.Sequence().Append(s1.transform.DOMove(ts1, 4f))
                .Append(s2.transform.DOMove(ts2, 4f))
                .Append(s3.GetComponent<SpriteRenderer>().DOFade(1, 9f))
                .Append(s4.transform.DOMove(ts4, 4f))
                .Append(s5.transform.DOMove(ts5, 4f))
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
                .AppendCallback(() =>
                {
                    sbgm.GetComponent<AudioSource>().Stop();
                })
                .Append(bt.GetComponent<SpriteRenderer>().DOFade(0, 1f))
                .AppendCallback(()=> {
                    SceneManager.LoadScene("levelEditor"); }
                );
                
        }
    }
}
