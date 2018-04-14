using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	public Animator textAnimator;
	public Animator uiAnimator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			textAnimator.SetTrigger ("Play");
			uiAnimator.SetTrigger ("Play");
			StartCoroutine (ChangeScene ());
		}
	}

	private IEnumerator ChangeScene () {
		yield return new WaitForSecondsRealtime(1.3f);
		SceneManager.LoadSceneAsync("PlayerSelect");
	}
}
