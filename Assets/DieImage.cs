using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieImage : MonoBehaviour {
    public Image playerImage;
    public Text text;
    public Image enemyImage;
    private float secondsBefore = 1;
    private float secondsContinue = 1;

    public void Show(Sprite enemySprite) {
        StartCoroutine(ShowC(enemySprite));
    }
    IEnumerator ShowC(Sprite enemySprite) {
        yield return new WaitForSeconds(secondsBefore);
        GetComponent<Image>().enabled = true;
        playerImage.enabled = true;
        text.enabled = true;
        enemyImage.enabled = true;
        enemyImage.sprite = enemySprite;
        yield return new WaitForSeconds(secondsContinue);
        GetComponent<Image>().enabled = false;
        playerImage.enabled = false;
        text.enabled = false;
        enemyImage.enabled = false;
    }
}
