﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPart : MonoBehaviour {

    public void OneHit() {
        Debug.Log("受到了伤害");
        transform.parent.SendMessage("OneHit");
    }
}