using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cartoon : MonoBehaviour {
    public GameObject enemy, teleporter, road, player,symbol;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnEnable()
    {
        DOTween.Sequence().Append(enemy.transform.DOMoveY(-1.2f, 0.3f))
            .AppendCallback(()=> { symbol.GetComponent<Animator>().SetBool("active", true); })
            .AppendInterval(1f)
            .Append(enemy.transform.DOMoveX(-3.91f,2f))
           // .Insert(teleporter.transform.DOMoveX(1,1))
            ;

    }
    private void OnDisable()
    {
    
        enemy.transform.localPosition += new Vector3(0, 2.79f+0.8354049f, 0);
    }
}
