﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    Transform player;

    private void Start() {
        player = FindObjectOfType<Player>().transform;
    }

    float Xdistance {
        get { return Mathf.Abs(player.position.x - transform.position.x); }
    }
    float Ydistance {
        get { return Mathf.Abs(player.position.y - transform.position.y); }
    }
    
    private void Update() {
        if (Xdistance >= 2.4 || Ydistance >= 1.2) {
            StartCoroutine(Following());
        }
    }

    IEnumerator Following() {
        float precent = 0;
        float start_pos_x = transform.position.x;
        float start_pos_y = transform.position.y;
        float end_pos_x = player.position.x;
        float end_pos_y = player.position.y - 1.2f;

        while (precent < 1f) {
            transform.position = new Vector3(Mathf.Lerp(start_pos_x, end_pos_x, precent),
                Mathf.Lerp(start_pos_y, end_pos_y, precent),
                transform.position.z);
            precent += 0.1f;
            yield return new WaitForFixedUpdate();
        }
    }
}