using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour {
    public GameObject hero, boss;
    Animator heroani, bossani;
    SpriteRenderer herosr,weaponsr,firesr,fireupdownsr;
    SpriteMask heromask;

	// Use this for initialization
	void Start () {
        hero = GameObject.FindGameObjectWithTag("Player");
        
        heroani = hero.GetComponent<Animator>();
        
        herosr = hero.GetComponent<SpriteRenderer>();
        weaponsr = GameObject.Find("weapon").GetComponent<SpriteRenderer>();
        firesr = GameObject.Find("Fire").GetComponent<SpriteRenderer>();
        fireupdownsr = GameObject.Find("FireUpDown").GetComponent<SpriteRenderer>();
        try
        {
            boss = GameObject.Find("Boss");
            bossani = boss.GetComponent<Animator>();
        }
        catch(System.Exception e) { }

        

    }

	public void HeroShot(int n)
    {
        //0,1,2,3对应左，右，上，下
        switch (n)
        {
            case 0:
                herosr.flipX = false;
                weaponsr.enabled=true;
                firesr.enabled=true;
                fireupdownsr.enabled=false;
                break;
            case 1:
                herosr.flipX = true;
                weaponsr.enabled=true;
                firesr.enabled=true;
                fireupdownsr.enabled=false;
                break;
            case 2:
                herosr.flipX = true;
                weaponsr.enabled=false;
                firesr.enabled=false;
                fireupdownsr.enabled=true;
                break;
            case 3:
                herosr.flipX = false;
                weaponsr.enabled=false;
                firesr.enabled=false;
                fireupdownsr.enabled=true;
                break;
            default:break;

        }
        heroani.SetBool("isShoot", true);
        Debug.Log("Shoot!");
    }
    public void HeroChange(int num)
    {
        heroani.SetInteger("stat", num);
    }
    public void HeroMove()
    {
        heroani.SetBool("isMoving", true);
    }
    public void HeroDie()
    {
        heroani.SetBool("isDead", true);

    }
    public void BossLeft()
    {
        bossani.SetBool("left", true);
    }
    public void BossRight()
    {
        bossani.SetBool("right", true);
    }
    public void BossDie()
    {
        bossani.SetBool("ishited", true);
    }
    public void KnifeEnemyReady(GameObject gb,bool stat)
    {
        var gbd = gb.transform.Find("小怪刀");
        if (gbd != null)
            gbd.GetComponent<Animator>().SetBool("isReady", stat);
        else Debug.Log(gb.name + "is not KnifeEnemy");
        
    }
    public void KnifeEnemyAttack(GameObject gb, bool stat)
    {
        var gbd = gb.transform.Find("小怪刀");
        if (gbd != null)
            gbd.GetComponent<Animator>().SetBool("isAttack", stat);
        else Debug.Log(gb.name + "is not KnifeEnemy");

    }
    public void BombReady(GameObject gb, bool stat)
    {
      
        var gba = gb.GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("isReady", stat);
        else Debug.Log(gb.name + "is not Bomb");

    }
    public void BombBoom(GameObject gb)
    {
        var gba = gb.GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("isBoom", true);
        else Debug.Log(gb.name + "is not Bomb");
    }

    public void MoveBomberReady(GameObject gb, bool stat)
    {
        var gba = gb.GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("isReady", stat);
        else Debug.Log(gb.name + "is not MoveBomber");
          
    }
    public void MoveBomberBoom(GameObject gb)
    {
        var gba = gb.GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("isBoomed", true);
        else Debug.Log(gb.name + "is not MoveBomber");
    }

    public void LameEnemyMove(GameObject gb)
    {
        var gba = GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("isMoving", true);
        else Debug.Log(gb.name+"is not a LameEnemy");
    }
    public void DiagonalEnemyLeft(GameObject gb)
    {
        var gba = GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("left", true);
        else Debug.Log(gb.name + "is not a DiagonalEnemy");
    }
    public void DiagonalEnemyRight(GameObject gb)
    {
        var gba = GetComponent<Animator>();
        if (gba != null)
            gba.SetBool("right", true);
        else Debug.Log(gb.name + "is not a DiagonalEnemy");
    }
    // Update is called once per frame

}
