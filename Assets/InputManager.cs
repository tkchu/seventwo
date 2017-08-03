using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public Player player;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            FindObjectOfType<ExitTrigger>().GetComponent<ExitTrigger>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            FindObjectOfType<FullscreenTrigger>().GetComponent<FullscreenTrigger>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            player.Go(new Vector2(0, 1));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            player.Go(new Vector2(0, -1));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            player.Go(new Vector2(-1, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            player.Go(new Vector2(1, 0));
        }

    }
}
