﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneWeaponBar : MonoBehaviour {
    public GameObject select;
    public GameObject unSelect;
    public Image countPointer;

    public void Select() {
        select.SetActive(true);
        countPointer.gameObject.SetActive(true);
        unSelect.SetActive(false);
    }

    public void UnSelect() {
        select.SetActive(false);
        countPointer.gameObject.SetActive(false);
        unSelect.SetActive(true);
        countPointer.fillAmount = 1f;
    }

    public void Show(float precent) {
        countPointer.fillAmount = precent;
    }
}