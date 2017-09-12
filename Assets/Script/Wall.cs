﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("fuck");
    }
    private void OnCollisionExit2D(Collision2D collision) {
        Debug.Log("fuck out");
    }
}