using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    Animator heroAnimator;
    public Player player;

    private void Start()
    {
        heroAnimator = GameObject.Find("player").GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<ExitTrigger>().GetComponent<ExitTrigger>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FindObjectOfType<FullscreenTrigger>().GetComponent<FullscreenTrigger>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            heroAnimator.SetBool("isWalk", true);
            player.Go(new Vector2(0, 1));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            heroAnimator.SetBool("isWalk", true);
            player.Go(new Vector2(0, -1));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            heroAnimator.SetBool("isWalk", true);
            player.Go(new Vector2(-1, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            heroAnimator.SetBool("isWalk", true);
            player.Go(new Vector2(1, 0));
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            heroAnimator.SetBool("isWalk", false);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            heroAnimator.SetBool("isWalk", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            heroAnimator.SetBool("isWalk", false);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            heroAnimator.SetBool("isWalk", false);
        }
    }
}

