using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {
    public SpriteRenderer clip;
    public Sprite[] ammoClip;

    public SpriteRenderer filled;
    public Sprite[] ammoFilled;

    private void Update() {
        Weapon weapon = transform.parent.GetComponent<Weapon>();
        switch (weapon.gunNow) {
            case Guns.empty:
                clip.enabled = false;
                filled.enabled = false;
                break;
            case Guns.pistol:
                clip.enabled = true;
                filled.enabled = true;
                clip.sprite = ammoClip[0];
                filled.sprite = ammoFilled[0];
                filled.transform.localScale = new Vector3(1, weapon.loadCount / 5f, 1);
                break;
            case Guns.shotgun:
                clip.enabled = true;
                filled.enabled = true;
                clip.sprite = ammoClip[1];
                filled.sprite = ammoFilled[1];
                filled.transform.localScale = new Vector3(1, weapon.loadCount / 3f, 1);
                break;
            case Guns.jumpgun:
                clip.enabled = true;
                filled.enabled = true;
                clip.sprite = ammoClip[2];
                filled.sprite = ammoFilled[2];
                filled.transform.localScale = new Vector3(1, weapon.loadCount / 2f, 1);
                break;
        }
    }
}
