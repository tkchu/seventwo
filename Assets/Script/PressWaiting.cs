using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class PressWaiting : MonoBehaviour {
    public bool waiting;
    public bool restart=false;
    public bool over;
    SpriteRenderer WaitingSR;
    public GameObject Waiting;
    public GameObject UIhero, SelectList;
    public Cartoon cartoon;
    // Use this for initialization
    void Start () {
        
        waiting = true;
        WaitingSR = Waiting.GetComponent<SpriteRenderer>();
       // EndSR = End.GetComponent<SpriteRenderer>();
        
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey &&e.keyCode==KeyCode.S&&waiting) waiting = false;
        
    }
    void Begin()
    {

        //WaitingSR.enabled = false;
        Sequence start = DOTween.Sequence();
        start.Append(WaitingSR.DOFade(0, 1f))
            .AppendCallback(() => { cartoon.enable = true;Destroy(gameObject); });
        
        
    }

    /*
    void Over()
    {
        //snow.SetActive(true);
        UIhero.GetComponent<Animator>().SetBool("isDead", true);
        Sequence over = DOTween.Sequence();
        over.Append(
            UIcoverSR.DOFade(1, 1f)
            ).Append(
            UIheroSR.DOFade(1, 2f)
            ).Append(
            EndSR.DOFade(1, 2f)
            );

    }

    void Restart()
    {
        over = false;

        //snow.SetActive(false);
        EndSR.DOFade(0f, 0.1f);
        Sequence restart = DOTween.Sequence();
        restart.Append(
           EndSR.DOFade(0f, 1f)
           ).Append(
            UIheroSR.DOFade(0, 1f)
            ).AppendInterval(0.5f).AppendCallback(() => {
               UIhero.GetComponent<Animator>().SetBool("isDead", false);
           }
           ).Append(
           UIheroSR.DOFade(1, 1f)
           ).Append(
           Logo.transform.DOMove(vlogo, 1f)
           ).Append(
           UIcoverSR.DOFade(1, 1f)
           ).Append(
           WaitingSR.DOFade(1, 1f)
         );
    }
    */
    // Update is called once per frame
    void Update()
    {

        if (!waiting)
        {
            waiting = true;

            Begin();
        }
        /*
        if(over)
        {
            over = false;
            Over();

        }
        if (restart)
        {
            restart = false;
            Restart();

        }
        */
    }
}
