using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lame : MonoBehaviour {
    public bool noDie = true;

    public void Die() {
        FindObjectOfType<Map>().RemoveGameObject(gameObject);
        noDie = false;
        Destroy(gameObject);
    }
}
