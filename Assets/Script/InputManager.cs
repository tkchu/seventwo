using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public Player player;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            FindObjectOfType<ExitTrigger>().GetComponent<ExitTrigger>().enabled = true;
            Debug.Log(FindObjectOfType<ExitTrigger>().name);
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            FindObjectOfType<FullscreenTrigger>().GetComponent<FullscreenTrigger>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            player.Go(new int[]{0,1});
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            player.Go(new int[] { 0, -1 });
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            player.Go(new int[] { -1, 0 });
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            player.Go(new int[] { 1, 0 });
        }

    }
}
