using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour {
    Vector3 offset1, offset2, offset3, offset4;
    private GameObject player = null;
    public GameObject hero {
        get {
            while (player == null)
                player = FindObjectOfType<Player>().gameObject;
            return player;
        }
    }
    public GameObject boss,fire,fireupdown;
    Transform herots
    {
        get { return hero.GetComponent<Transform>(); }
    }
    Animator heroani {
        get { return hero.GetComponent<Animator>(); }
    }
    Animator bossani;
    SpriteRenderer herosr {
        get { return hero.GetComponent<SpriteRenderer>(); }
    }
    SpriteRenderer weaponsr {
        get {return hero.transform.Find("weapon").GetComponent<SpriteRenderer>(); }
    }
    SpriteRenderer firesr {
        get { return hero.transform.Find("Fire").GetComponent<SpriteRenderer>(); }
    }
    SpriteRenderer fireupdownsr {
        get { return hero.transform.Find("FireUpDown").GetComponent<SpriteRenderer>(); }
    }
    SpriteMask heromask;

	// Use this for initialization
	void Start () {
        try
        {
            boss = GameObject.Find("Boss");
            bossani = boss.GetComponent<Animator>();
        }
        catch(System.Exception e) { }

        var ts1 = this.transform.Find("Fire");

        var ts2 = this.transform.Find("FireUpDown");
        offset1 = ts1.position - herots.position;
        offset2 = new Vector3(ts1.position.x - herots.position.x, 5 * (herots.position.y - ts1.position.y), 1);
        offset3 = ts2.position - herots.position;
        offset4 = new Vector3(ts2.position.x - herots.position.x, 5 * (herots.position.y - ts2.position.y), 1);


    }

    public void HeroShot(int n)
    {
        //0,1,2,3对应左，右，上，下
        switch (n)
        {
            case 0:
                herosr.flipX = false;
                weaponsr.enabled=true;
                Instantiate(fire, transform);
                /*
                firesr.enabled=true;
                fireupdownsr.enabled=false;*/
                break;
            case 1:
                
                herosr.flipX = true;
                weaponsr.enabled = true;
                Instantiate(fire, transform);
                /*
                firesr.enabled=true;
                fireupdownsr.enabled=false;*/
                break;
            case 2:
                herosr.flipX = true;
                weaponsr.enabled=false;
                Instantiate(fireupdown, transform);
                //firesr.enabled=false;
                //fireupdownsr.enabled=true;
                break;
            case 3:
                herosr.flipX = false;
                weaponsr.enabled=false;
                Instantiate(fireupdown, transform);
                // firesr.enabled=false;
                // fireupdownsr.enabled=true;
                break;
            default:break;

        }
        heroani.SetBool("isShoot", true);

        //Debug.Log("heroShoot!");
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
