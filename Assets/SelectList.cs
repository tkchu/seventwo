using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectList : MonoBehaviour {
    public int a=0,b=0,c=0;
    public Transform arrow, logo;
    public AudioSource titlebgm;
    public SpriteRenderer cover;
    public GameObject story;
    public Animator a1, a2, a3;
    void Begin()
    {
        switch (a){
            case 0:
                DOTween.Sequence().Append(logo.DOMoveY(3,1f))
                    .Append(titlebgm.DOFade(0,1f))
                    .Append(cover.DOFade(1,1f))
                    .AppendCallback(() => { story.SetActive(true); });
                
                break ;
            case 1:
                DOTween.Sequence().Append(logo.DOMoveY(3, 1f))
                    .Append(cover.DOFade(1, 1f))
                    .AppendCallback(() => { SceneManager.LoadScene("levelEditor"); });
                break;
            case 2:Application.Quit();break;

        }
    }
    private void OnGUI()
    {
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
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
                case 0:  a1.SetBool("isSelected", false); break;
                case 1:  a2.SetBool("isSelected", false); break;
                case 2:  a3.SetBool("isSelected", false); break;
            }
            switch (a)
            {
                case 0: arrow.DOMoveY(-0.4977778f, 0.3f); a1.SetBool("isSelected",true); break;
                case 1: arrow.DOMoveY(-1.066667f, 0.3f); a2.SetBool("isSelected", true); break;
                case 2: arrow.DOMoveY(-1.635556f, 0.3f); a3.SetBool("isSelected", true); break;
            }
            b = a;
        }
    }
}
