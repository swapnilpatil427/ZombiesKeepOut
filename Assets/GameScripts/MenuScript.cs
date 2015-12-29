using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
	public GameObject Menu;
	public GameObject Loading;

	private AsyncOperation async = null; // When assigned, load is in progress.
	private IEnumerator LoadALevel(int level) {
		async = Application.LoadLevelAsync(level);
		yield return async;
	}
	// Use this for initialization
	public void Start()
	{
		Loading.SetActive (false);
	}

	public void OnPlayClick() {
		Menu.SetActive (false);
		Loading.SetActive (true);
		StartCoroutine (LoadALevel(1));
	}

	public void ExitClick(){
		Application.Quit ();
	}
	// Update is called once per frame

}
