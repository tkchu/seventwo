using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeTrigger : MonoBehaviour {
    public float duringTime = 1f;
    public float offsetMax = 5f;

    private IEnumerator coroutine = null;

    IEnumerator Shake() {
        float precent = 1f;
        Vector3 startPos = transform.localPosition;

        while(precent > 0) {
            precent -= Time.deltaTime / duringTime;
            Vector2 offset_temp = Random.insideUnitCircle * offsetMax * precent;
            Vector3 offset = new Vector3(offset_temp.x, offset_temp.y, 0);
            transform.localPosition = startPos + offset;
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = startPos;
        GetComponent<ScreenShakeTrigger>().enabled = false;
    }

    private void OnEnable() {
        if (coroutine != null) {
            StopCoroutine(coroutine);
        }
        coroutine = Shake();
        StartCoroutine(coroutine);
    }
}
