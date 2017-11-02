using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;


public class UIMain :UIBehaviour {


	 void  Start () 
	{
		//让TimeScale = 0
		Time.timeScale = 0;

		Image image = transform.Find("Image").GetComponent<Image>();
		//调用DOmove方法来让图片移动
		Tweener tweener = image.rectTransform.DOMove(Vector3.zero,1f);
		//设置这个Tween不受Time.scale影响
		tweener.SetUpdate(true);
		//设置移动类型
		tweener.SetEase(Ease.Linear);/*
		tweener.onComplete = delegate() {
			Debug.Log("移动完毕事件");
		};
		image.material.DOFade(0,1f).onComplete = delegate() {
			Debug.Log("褪色完毕事件");
		};*/
	}



	private void OnDrag(GameObject go){
		Debug.Log(go.name);

	}
}
