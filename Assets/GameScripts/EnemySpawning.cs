using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemySpawning : MonoBehaviour {
	public List<Transform>	Zombies;
	public GameObject zombie;
	public GameObject zombiecreationeffect;
	public GameObject bossObject;
	public GameObject bosscreationeffect;
	public GameObject player;
	public AudioClip spawn;
	public AudioClip zombieScream2;
	public int maxenemycount;
	public PlayerController playercontroller;
	bool createenemyflag;
	public float timebtwtwozombies;
	int enemycount;
	public List<Transform> path1;
	public List<Transform> path2;
	public List<Transform> path3;
	public List<Transform> path4;
	public List<Transform> path5;
	public List<string> zombietype;
	public List<Transform> bosspath;
	public GameObject bossaimpoint;
	public Image BossHealthBar;
	public Text warningmessagetext;
	public GameObject BossHealthBarBackground;

	// Use this for initialization
	void Start () {
		createenemyflag = false;
		playercontroller = GameObject.Find ("PlayerController").GetComponent<PlayerController> ();
		enemycount = 1;
		StartCoroutine (SpawnEnemies ());
		zombietype.Add ("FollowPlayer");
		zombietype.Add ("FollowPath");
		BossHealthBarBackground.SetActive (false);
		warningmessagetext.text = "";
		StartCoroutine(Groan());
	}
	void Update()
	{
		if (createenemyflag == true && playercontroller.health > 0) {
			StartCoroutine (SpawnEnemies ());
		}
	}

	IEnumerator Groan()
	{
		while (true) {
			
			this.gameObject.GetComponent<AudioSource>().clip = zombieScream2;
			if(!this.gameObject.GetComponent<AudioSource>().isPlaying)
				this.gameObject.GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(12f);
		}
	}
	public void setenemycount()
	{
		enemycount = enemycount - 1;
	}

	IEnumerator SpawnEnemies() {
		int random;
		int randomposition;
		float speed=zombie.GetComponent<ZombieMover>().speed;
		while (true) {
		if(playercontroller.LevelTarget <=0 )
			{
				createenemyflag = false;
				break;
			}
		if(maxenemycount < enemycount )
			{
				createenemyflag = true;
				break;
			}
			if(playercontroller.health <=0)
				break;
		
			randomposition = Random.Range(0,Zombies.Capacity-1);
			//Instantiate(zombiecreationeffect,Zombies[randomposition].position,Zombies[randomposition].rotation);
			//yield return new WaitForSeconds(0.5f);
			gameObject.GetComponent<AudioSource>().clip = spawn;
			gameObject.GetComponent<AudioSource>().Play();
			Instantiate(zombiecreationeffect,Zombies[randomposition].position,Zombies[randomposition].rotation);
				enemycount++;
		GameObject zombieobject =(GameObject) Instantiate(zombie,Zombies[randomposition].position,zombie.transform.rotation);
			//GameObject healthbar = zombieobject.transform.GetChild (2).gameObject.name;
			//zombieobject.GetComponent<ZombieMover>().tempspeed = speed;
			random = Random.Range(1,10);
			//Debug.Log(random);
			zombieobject.GetComponent<ZombieMover>().FireEffect(false);
			zombieobject.GetComponent<ZombieMover>().healthbar =zombieobject.transform.GetChild (0).gameObject.transform.GetChild (2).gameObject.GetComponent<Image>();
			zombieobject.GetComponent<ZombieMover>().player = player;
			zombieobject.GetComponent<ZombieMover>().speed = 0;

			yield return new WaitForSeconds(zombiecreationeffect.GetComponent<TimedSuicide>().m_Time);
			if(zombieobject != null)
			{
			if(random%2 != 0)
				{
					zombieobject.GetComponent<ZombieMover>().speed = speed;
				zombieobject.GetComponent<ZombieMover>().setType(zombietype[0]);
				}
			else
			{
				zombieobject.GetComponent<ZombieMover>().speed = Random.Range((3.75f*speed/4),speed/3);
				zombieobject.GetComponent<ZombieMover>().setType(zombietype[1]);
				random = Random.Range(0,4);
				if(random == 0)
					zombieobject.GetComponent<ZombieMover>().setPath(this.path1);
				else if(random == 1)
						zombieobject.GetComponent<ZombieMover>().setPath(this.path2);
				else if(random == 2)
					zombieobject.GetComponent<ZombieMover>().setPath(this.path3);
				else if(random == 3)
						zombieobject.GetComponent<ZombieMover>().setPath(this.path4);
				else 
					zombieobject.GetComponent<ZombieMover>().setPath(this.path5);
			}
				zombieobject.GetComponent<ZombieMover>().tempspeed = zombieobject.GetComponent<ZombieMover>().speed;
			}
			yield return new WaitForSeconds(timebtwtwozombies);
			
			
		}
		if (createenemyflag == false && playercontroller.health > 0) {
			int randompos = Random.Range(0,bosspath.Count-1);
			BossHealthBarBackground.SetActive (true);
			warningmessagetext.text = "Get Ready For the Boss...";
			yield return new WaitForSeconds(4f);
			warningmessagetext.text = "";
			GameObject zombiebossobject =(GameObject) Instantiate(bossObject,Zombies[randompos].position,zombie.transform.rotation);
		//	Debug.Log(bossaimpoint.name);
			zombiebossobject.GetComponent<MoveBossBullet>().player = bossaimpoint;
			zombiebossobject.GetComponent<MoveBossBullet>().path = bosspath;
			zombiebossobject.GetComponent<MoveBossBullet>().BossHealthBar = BossHealthBar;
		}
		//Debug.Log ("Level Completed");
	}
}
