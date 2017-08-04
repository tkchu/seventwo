using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private void Start()
    {
        if (gameObject.name.Equals("StoryBGM"))
        {
            Destroy(gameObject, GetComponent<AudioSource>().clip.length);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.name.Equals("StoryBGM"))
        {
            LoadingScene.ShowLoadingScene("start",true);
        }
    }
}
