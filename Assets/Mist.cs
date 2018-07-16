using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mist : MonoBehaviour
{
    public bool trigger = false;
    public bool cover = true;
    public bool detected = false;
    SpriteRenderer sprite;
    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!trigger) return;
        if(detected&&gameObject.tag=="wall")
        {
            sprite.DOFade(1, 0.5f);
            return;
        }
        if (cover && !detected)
        {
            sprite.DOFade(1, 0.5f);
        }
        if (!cover)
        {
            detected = true;
            GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        }
        trigger = false;
    }
}