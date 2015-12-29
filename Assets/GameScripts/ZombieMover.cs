using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class ZombieMover : MonoBehaviour {

	// Use this for initialization
	public float health;
	public float speed;
	public Image healthbar;
	public GameObject DeathEffect;
	Animator animator;
	public float tempspeed;
	public float impact;
	Coroutine attackingcoroutine;
	PlayerController playercontroller;

	public GameObject player;
	ArrayList weapons = new ArrayList();
	public bool attacking ;
	public AudioClip ZombieDie;
	public bool zombiecollided;
	 List<Transform> path;
	string Type;
	Transform targettpoint;
	int currentpoint;
	public float PathPointWait;
	float timecount;
	public AudioClip playerinsight;
	public AudioClip zombieScream1;

	public AudioClip zombieScream3;


	//public GameObject showfireeffect;
	//public GameObject FireEffect;

	public bool fireeffect;
	//GameObject
	//public AudioClip Die;
	//public AudioClip Groan;
	// Update is called once per frame
	void FixedUpdate () {

		//Vector3 screenPos = camera.WorldToScreenPoint(player.transform.position);
		//iTween.LookTo (this.gameObject,player.transform.position,2.0f);
		//iTween.Hash ("axis", "y", "time", 2.0f)
		                            
		//iTween.MoveTo(player,iTween.Hash("position",transform.position += Vector3.left*2,"easetype",iTween.EaseType.easeInOutSine,"time",.2f));
		//transform.Translate (0, 0, speed * Time.deltaTime);
	}
	void Start()
	{
		path = new List<Transform> ();
		fireeffect = false;
		playercontroller = GameObject.Find ("PlayerController").GetComponent<PlayerController> ();
		//enemyspawning = GameObject.Find ("GameController").GetComponent<PlayerController> ();
		if (playercontroller == null)
			Debug.Log ("PLayer controler not found");
		animator = this.GetComponent<Animator> ();
		//tempspeed = new float ();
		//tempspeed = speed;
		weapons.Add ("PlayerShot");
		weapons.Add ("RomanCandle");
		currentpoint = -1;
		timecount = PathPointWait;
		//targettpoint = path [0];
		attacking = false;
		zombiecollided = false;
	
	}
	
	void Update()
	{
		if (attacking == false && player!= null) {
			Vector3 movement = new Vector3 (Vector3.forward.x, 0.0f, Vector3.forward.z);
			float distancefrmplayer = Vector3.Distance (player.transform.position, this.gameObject.transform.position);
			if (Type == "FollowPath") {
				//Debug.Log(path.Count);
				if(path.Count != 0)
				{
				float distance = Vector3.Distance (targettpoint.position, this.gameObject.transform.position);
			//	Debug.Log (distance);
				if (distance < 1f) {
						timecount = 0 ;
					currentpoint = (currentpoint + 1) % path.Count;
					targettpoint = path [currentpoint];
				}

				if(distancefrmplayer <5f)
					{
						this.gameObject.GetComponent<AudioSource>().clip = playerinsight;
						if(!this.gameObject.GetComponent<AudioSource>().isPlaying)
							this.gameObject.GetComponent<AudioSource>().Play();
						speed = Random.Range(3.0f,4.0f);
						iTween.LookTo (this.gameObject, player.transform.position, 2.0f);
						transform.Translate (movement * speed * Time.deltaTime);
					}
				else
					{
						timecount += Time.deltaTime;
						if(timecount >= PathPointWait)
						{
							speed = tempspeed;
							iTween.LookTo (this.gameObject, targettpoint.position, 2.0f);
							transform.Translate (movement * speed * Time.deltaTime);
						}
					}
				}
			} else if (Type == "FollowPlayer") {
			//	if (fireeffect == true)
			//iTween.MoveTo(showfireeffect,iTween.Hash("position",this.gameObject.transform.position,"speed",0.01f));
//				if(distancefrmplayer <5f)
//				{
//					this.gameObject.GetComponent<AudioSource>().clip = playerinsight;
//					if(!this.gameObject.GetComponent<AudioSource>().isPlaying)
//						this.gameObject.GetComponent<AudioSource>().Play();
//	
					iTween.LookTo (this.gameObject, player.transform.position, 2.0f);
				//iTween.MoveTo(this.gameObject,iTween.Hash("looktarget",player.transform.position,"y",0.0f,"position",player.transform.position,"speed",speed));		
				transform.Translate (movement * speed * Time.deltaTime);
			}
		}
	}
	public void setPath(List<Transform> Path)
	{
		this.path = Path;
		targettpoint = this.path [Random.Range(0,this.path.Count -1)];
	}
	public void setType(string type)
	{
		this.Type = type;
	}
	public void FireEffect(bool onoff)
	{
		if(this.gameObject != null)
		this.gameObject.transform.GetChild (1).gameObject.SetActive(onoff);
	}
	void OnTriggerEnter(Collider other)
	{
		if (weapons.Contains (other.gameObject.tag)) {
			ReduceHealth(other.gameObject.GetComponent<WeaponImpact>().impactcapacity);
			PKFxFX fx = other.gameObject.GetComponent<PKFxFX>();
			if (fx != null)
				fx.StopEffect();
			Destroy(other.gameObject);
		}

//
//		else if(other.gameObject.name == "CamPivot")
//			{
//
//				PKFxFX fx = other.gameObject.GetComponent<PKFxFX>();
//				if (fx != null)
//					fx.StopEffect();
//				Destroy(other.gameObject);
//			}
		else if (other.gameObject.name == "CamPivot")
			{
			this.gameObject.GetComponent<AudioSource>().clip = zombieScream1;
			if(!this.gameObject.GetComponent<AudioSource>().isPlaying)
			this.gameObject.GetComponent<AudioSource>().Play();
			attacking = true;
				attack();
			}
	


	}

	IEnumerator SlowZombie()
	{
		float tspeed = Random.Range (tempspeed/3, tempspeed*1.5f);
		if(this != null)
			this.speed = tspeed;
		//Debug.Log (speed);
		yield return new WaitForSeconds (7);
		//this.speed = tempspeed;
		//yield return 0;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "CamPivot") {
			attacking = false;
			walk();
		}
	}
	IEnumerator Attacking()
	{
		yield return new WaitForSeconds (0.3f);
		playercontroller.SetHealth(impact);
		while (true) {
			yield return new WaitForSeconds (0.8f);
			playercontroller.SetHealth(impact);
		//	iTween.ShakePosition(player, Vector3.one, 5f);
			//iTween.ShakePosition(player,iTween.Hash("x",0.3f,"time",1.0f));
		}
	}
	void attack()
	{
		speed = 0;
		animator.SetBool("attack", true);
	 attackingcoroutine =StartCoroutine (Attacking ());
	}
	void walk()
	{
		if (attackingcoroutine != null)
			StopCoroutine (attackingcoroutine);
		StartCoroutine (SlowZombie ());
		animator.SetBool("attack", false);
	//	speed = tempspeed;
	}

	void Die()
	{
		playercontroller.UpdateLevelTarget ();
		StartCoroutine (DieAnimation ());
		GameObject.Find ("GameController").GetComponent<EnemySpawning> ().setenemycount ();
		//Write enemgy die code
	}
	IEnumerator  DieAnimation()
	{

		speed = 0.0f;
	//	this.gameObject.GetComponent<Animator> ().SetTrigger ("Die");
	// yield return new WaitForSeconds (1);
		Instantiate (DeathEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
	//	yield return new WaitForSeconds (2);
		gameObject.GetComponent<AudioSource> ().clip = ZombieDie;
		gameObject.GetComponent<AudioSource> ().Play ();
		while(gameObject.GetComponent<AudioSource> ().isPlaying)
		Destroy(this.gameObject);
		yield return 0;
	}
	public void ReduceHealth(float impact)
	{
		health = health - impact;
		if (health <= 0)
			Die ();
		else 
			healthbar.fillAmount = health / 100;

	}
}
