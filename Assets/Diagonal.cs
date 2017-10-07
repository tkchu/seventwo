using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diagonal : MonoBehaviour {
    public bool noDie = true;

    public void Die() {
        noDie = false;
        Destroy(gameObject);
    }
}
