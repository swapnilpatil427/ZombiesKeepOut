using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float health ;
	public Image healthbar;
	public Text playername;
	public GameObject gameover;
	//public GameObject PauseMenu;
	public GameObject CompletedMenu;
	public bool applicationpause;
	public int LevelTarget;
	public bool bossalive;
	public AudioClip BossDie;
	public AudioClip playerhurt;
	public AudioClip cheersound;
	public GameObject[] zombies;

	// Use this for initialization
	void Start () {
		gameover.SetActive (false);
		CompletedMenu.SetActive (false);
		applicationpause = false;
		bossalive = true;
		//PauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
//		if (applicationpause == false) {
		if (Input.GetKey (KeyCode.Escape)) {
			Cursor.visible = true;
			Application.LoadLevel(0);
//				if (applicationpause == true) {
//					//Paused ();
//				} else {
//					//Unpause ();
//				}
//			}
	}
	}

	public void UpdateLevelTarget()
	{
		LevelTarget--;

	}
	public void LevelComplete()
	{
		GetComponent<AudioSource> ().clip = cheersound;
		if(!GetComponent<AudioSource> ().isPlaying)
			GetComponent<AudioSource> ().Play ();
		if (bossalive == false) {
			CompletedMenu.SetActive (true);
			Cursor.visible = true;
			zombies = GameObject.FindGameObjectsWithTag("Zombie");
			foreach(GameObject zombie in zombies)
			{
				Destroy(zombie);
			}
		}
	}

	public void GameOver()
	{

			zombies = GameObject.FindGameObjectsWithTag("Zombie");
			foreach(GameObject zombie in zombies)
			{
				Destroy(zombie);
			}
	}
	public void levelcompleted()
	{
		GetComponent<AudioSource> ().clip = BossDie;
		if(!GetComponent<AudioSource> ().isPlaying)
			GetComponent<AudioSource> ().Play ();
		Invoke("LevelComplete",4f);
	}
	public void ExitClick()
	{
		Application.LoadLevel (0);
	}
	public void RestartClick()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
	public void NextLevelClick()
	{
		Application.LoadLevel (0);
	}
	public void SetHealth(float impact)
	{
		GetComponent<AudioSource> ().clip = playerhurt;
		if(!GetComponent<AudioSource> ().isPlaying)
		GetComponent<AudioSource> ().Play ();
		health = health - impact;
		if (health <= 0)
			Die ();
		if (health >= 0) {
			healthbar.fillAmount = health / 100;

			playername.text = "Player: " + health;
		}
	}
	public void Die()
	{
		gameover.SetActive (true);
		Cursor.visible = true;
		Invoke("GameOver",2f);
		//Debug.Log ("Player Died");
	}
	public void YouWin()
	{
		CompletedMenu.SetActive (true);
	}
}
