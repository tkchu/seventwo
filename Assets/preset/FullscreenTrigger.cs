using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenTrigger : MonoBehaviour {
    void OnEnable() {
        Screen.fullScreen = !Screen.fullScreen;
        GetComponent<FullscreenTrigger>().enabled = false;
    }
}
