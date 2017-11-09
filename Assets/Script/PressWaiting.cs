using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class PressWaiting : MonoBehaviour {
    public bool waiting;
    public bool restart=false;
    public GameObject titlebgm;
    public bool over;
    SpriteRenderer WaitingSR, UIcoverSR,EndSR,UIheroSR;
    public GameObject Logo;
    public GameObject Waiting;
    public GameObject UIcover;
    public GameObject story;
    Vector3 vlogo;
    public GameObject UIhero;
    // Use this for initialization
    void Start () {
        
        waiting = true;
        WaitingSR = Waiting.GetComponent<SpriteRenderer>();
        UIcoverSR=UIcover.GetComponent<SpriteRenderer>();
       // EndSR = End.GetComponent<SpriteRenderer>();
        UIheroSR = UIhero.GetComponent<SpriteRenderer>();
        vlogo = Logo.transform.position;
        
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey &&e.keyCode==KeyCode.S) waiting = false;
        
    }
    void Begin()
    {

        //WaitingSR.enabled = false;
        Sequence start = DOTween.Sequence();
        start.Append(
            WaitingSR.DOFade(0, 1f)
            ).Append(
            Logo.transform.DOMoveY(5f, 1.5f)
            ).AppendCallback(() =>
            {
                // FindObjectOfType<Camera>().orthographicSize = 3.6f;
                Logo.SetActive(false);
            });
        DOTween.Sequence().Append(
                titlebgm.GetComponent<AudioSource>().DOFade(0, 2.5f)
                )
            .AppendCallback(
            () => {
                UIheroSR.DOFade(0, 0.5f);
            }).AppendCallback(() => {
                titlebgm.SetActive(false);
                story.SetActive(true);
                

            });
        
        
    }


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
    // Update is called once per frame
    void Update()
    {

        if (!waiting)
        {
            waiting = true;

            Begin();
        }
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
        
    }
}
