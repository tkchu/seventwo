using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieImage : MonoBehaviour {
    public Image introImage;
    public Image enemyImage;
    private float secondsBefore = 0.85f;
    private float secondsContinue = 1f;

    public void Show(Sprite enemySprite, Sprite introSprite) {
        StartCoroutine(ShowC(enemySprite, introSprite));
    }
    IEnumerator ShowC(Sprite enemySprite, Sprite introSprite) {
        yield return new WaitForSeconds(secondsBefore);
        GetComponent<Image>().enabled = true;
        introImage.enabled = true;
        introImage.sprite = introSprite;
        enemyImage.enabled = true;
        enemyImage.sprite = enemySprite;
        yield return new WaitForSeconds(secondsContinue);
        GetComponent<Image>().enabled = false;
        introImage.enabled = false;
        enemyImage.enabled = false;
    }
}
