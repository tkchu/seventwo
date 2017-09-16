using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour {
    GameObject hero,boss;
    Animator heroani, bossani;
	// Use this for initialization
	void Start () {
        hero = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.Find("Boss");
	}


	void HeroShot()
    {
        heroani.SetBool("isShoot", true);
    }
    void HeroChange(int num)
    {
        heroani.SetInteger("stat", num);
    }
    void HeroMove()
    {
        heroani.SetBool("isMoving", true);
    }
    void HeroDie()
    {
        heroani.SetBool("isDead", true);

    }
    void BossLeft()
    {
        bossani.SetBool("left", true);
    }
    void BossRight()
    {
        bossani.SetBool("right", true);
    }
    void BossDie()
    {
        bossani.SetBool("ishited", true);
    }
    void KnifeEnemyReady(GameObject gb,bool stat)
    {
        var gbd = gb.transform.Find("小怪刀");
        if (gbd != null)
            gbd.GetComponent<Animator>().SetBool("isReady", stat);
        else Debug.Log(gb.name + "is not KnifeEnemy");
        
    }
    void KnifeEnemyAttack(GameObject gb, bool stat)
    {
        var gbd = gb.transform.Find("小怪刀");
        if (gbd != null)
            gbd.GetComponent<Animator>().SetBool("isAttack", stat);
        else Debug.Log(gb.name + "is not KnifeEnemy");

    }
    void BombReady(GameObject gb, bool stat)
    {
      
        var gba = gb.GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("isReady", stat);
        else Debug.Log(gb.name + "is not Bomb");

    }
    void BombBoom(GameObject gb)
    {
        var gba = gb.GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("isBoom", true);
        else Debug.Log(gb.name + "is not Bomb");
    }

    void MoveBomberReady(GameObject gb, bool stat)
    {
        var gba = gb.GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("isReady", stat);
        else Debug.Log(gb.name + "is not MoveBomber");
          
    }
    void MoveBomberBoom(GameObject gb)
    {
        var gba = gb.GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("isBoomed", true);
        else Debug.Log(gb.name + "is not MoveBomber");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
