using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PressWaiting : MonoBehaviour {
    public bool waiting;
    private float time=0;
    SpriteRenderer WaitingSR;
    public GameObject Logo;
    public GameObject Waiting;
    public GameObject End;
    // Use this for initialization
    void Start () {
        waiting = true;
       WaitingSR = Waiting.GetComponent<SpriteRenderer>();
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey) waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            time += Time.deltaTime;
            if (time > 1) time = 0;
            if (time > 0.5f)
            {
                WaitingSR.DOFade(.3f, .5f);
            }
            else
                WaitingSR.DOFade(1, .5f);
        }
        else
        {
            
            WaitingSR.enabled = false;
            Logo.transform.DOMoveY(2.5f, 1f);
            

        }
        
    }
}
