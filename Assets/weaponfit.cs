using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponfit : MonoBehaviour {
    public GameObject player;
    SpriteRenderer playersr,thisr;
    Vector3 pos;

    bool flip;
	// Use this for initialization
	void Start () {
        pos = this.transform.position - player.transform.position;
        playersr = player.GetComponent<SpriteRenderer>();
        thisr = this.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        this.GetComponent<Animator>().SetBool("isShoot", player.GetComponent<Animator>().GetBool("isAttacking"));
        this.GetComponent<Animator>().SetInteger("stat", player.GetComponent<Animator>().GetInteger("Stat"));
        thisr.flipX = playersr.flipX;
        if (thisr.flipX)
        {
            thisr.transform.position=playersr.transform.position+new Vector3(-pos.x,pos.y,0);
        }
        else {
            thisr.transform.position = player.transform.position + pos;
        }

    }
}
