using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
    
    // Use this for initialization

    IEnumerator restart()
    {
        yield return new WaitForSeconds(19.5f);
        SceneManager.LoadScene("start");
    }

    void OnEnable()
    {
        StartCoroutine(restart());
        
    }
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("start");

    }
}
