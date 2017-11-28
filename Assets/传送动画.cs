using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 传送动画 : MonoBehaviour {
    SpriteRenderer playersp,sp;
    Player playerjs;
	// Use this for initialization
	void Start () {
        sp = GetComponent<SpriteRenderer>();
        playersp = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        playerjs = transform.parent.gameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        sp.sortingOrder = playersp.sortingOrder+1;
        if (playerjs!=null&&playerjs.isDead) StartCoroutine(Dead());
	}
    IEnumerator Dead()
    {

        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetBool("teleport1", true);
        
    }
}
