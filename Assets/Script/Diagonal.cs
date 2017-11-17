using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diagonal : MonoBehaviour {
    public bool noDie = true;
    Map map;
    Animator prepareAnimator;
    // Use this for initialization
    void Start()
    {
        map = FindObjectOfType<Map>();
        prepareAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        int[] player_pos = map.GetPlayerPos();
        int[] self_pos = map.FindGameObject(map.itemMap, gameObject);

        if (player_pos == null || self_pos == null)
            return;

        //准备
        if (noDie)
        {
            if ((Mathf.Abs(player_pos[0] - self_pos[0])+Mathf.Abs(player_pos[1] - self_pos[1]) <= 1)|| (Mathf.Abs(player_pos[0] - self_pos[0])<=1&& Mathf.Abs(player_pos[1] - self_pos[1]) <= 1))
            {
                prepareAnimator.SetBool("isReady", true);
            }
            else
            {
                prepareAnimator.SetBool("isReady", false);
            }
            if (Mathf.Abs(player_pos[0] - self_pos[0]) + Mathf.Abs(player_pos[1] - self_pos[1]) <= 0.3)
            {
                prepareAnimator.SetBool("isAttacking", true);
            }
            else
            {
                prepareAnimator.SetBool("isAttacking", false);
            }
        }
    }
    public void Die() {
        FindObjectOfType<Map>().RemoveGameObject(gameObject);
        noDie = false;
        Destroy(gameObject);
    }
}
