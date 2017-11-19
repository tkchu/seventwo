using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cartoon : MonoBehaviour {
    public bool enable;
    public GameObject enemy, teleporter, road, player,symbol,selectList;
    public SpriteRenderer arrow;
    public Text a1, a2, a3;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (enable)
        {
            enable = false;
            On();
        }
		
	}
    private void On()
    {
        DOTween.Sequence().Append(enemy.transform.DOMoveY(-1.2f, 0.3f))
            .AppendCallback(() => { symbol.GetComponent<Animator>().SetBool("active", true); })
            .AppendInterval(1f).AppendCallback(() => { road.GetComponent<Walk>().flag = false; })
            .Append(enemy.transform.DOMoveX(-4.5f, 1.5f))
            .AppendCallback(() => { Destroy(enemy); })
            .Insert(1.3f, teleporter.transform.DOMoveX(-3.91f, 1f))
            .Append(player.transform.DOMoveX(-4.5f, 2f))
            .AppendCallback(() => { Destroy(player); })
            .Append(teleporter.transform.DOScaleY(0, 0.5f))
            .Insert(4.8f,teleporter.transform.DOMoveY(-0.82f, 0.5f))
            .AppendCallback(()=>{ Destroy(teleporter); })
            .AppendCallback(() => { selectList.SetActive(true); })
            .Append(a1.DOFade(1, 1f))
            .Insert(3f, a2.DOFade(1, 1f)).Insert(3f, a3.DOFade(1, 1f)).Insert(3f, arrow.DOFade(1, 1f));
        ;
    }
    private void OnDisable()
    {
    
        //enemy.transform.localPosition += new Vector3(0, 2.79f+0.8354049f, 0);
    }
}
