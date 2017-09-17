using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour {
    public void OnMouseDown() {
        transform.parent.SendMessage("MouseDown", this);
    }
}
