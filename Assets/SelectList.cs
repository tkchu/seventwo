using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectList : MonoBehaviour {
    public int a=0,b=0;
    public Color statA, statB;
    public Transform arrow, logo;
    public AudioSource titlebgm;
    public GameObject story;
    public Text a1, a2, a3;
    bool flag = true;
    void Begin()
    {
        flag = false;
        switch (a){
            case 0:
                DOTween.Sequence().Append(logo.DOMoveY(3,1f))
                    .Append(titlebgm.DOFade(0,1.5f)).AppendCallback(()=> { titlebgm.gameObject.SetActive(false); })
                    .Insert(1f,a1.DOFade(0,1f))
                    .Insert(1f, a2.DOFade(0, 1f)).Insert(1f, a3.DOFade(0, 1f))
                    .Insert(1f, arrow.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1f))
                    .AppendCallback(() => {
                        gameObject.SetActive(false); story.SetActive(true); });
                break ;
            case 1:
                DOTween.Sequence().Append(logo.DOMoveY(3, 1f))
                    .Append(titlebgm.DOFade(0, 2f)).AppendCallback(() => { titlebgm.gameObject.SetActive(false); })
                    .Insert(1f, a1.DOFade(0, 2f))
                    .Insert(1f, a2.DOFade(0, 2f)).Insert(1f, a3.DOFade(0, 2f))
                    .Insert(1f, arrow.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 2f))
                    .AppendCallback(() => {
                        gameObject.SetActive(false); SceneManager.LoadScene("levelEditor"); });
                break;
            case 2:Application.Quit();break;

        }

    }
    // Use this for initialization
    void Start () {
        a1.color = statB;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (flag)
        {

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                a -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                a += 1;
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) Begin();
            if (a > 2) a -= 3;
            else if (a < 0) a += 3;
            if (b != a)
            {
                switch (b)
                {
                    case 0: a1.color = statA; break;
                    case 1: a2.color = statA; break;
                    case 2: a3.color = statA; break;
                }
                switch (a)
                {
                    case 0: arrow.DOMoveY(-0.6757811f, 0.3f); a1.color = statB; break;
                    case 1: arrow.DOMoveY(-1.209293f, 0.3f); a2.color = statB; break;
                    case 2: arrow.DOMoveY(-1.707236f, 0.3f); a3.color = statB; break;
                }
                b = a;
            }
        }
    }
}
