using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MoveBossBullet : MonoBehaviour {
	public GameObject player; 
	public float		m_StartVel = 50.0f;
	public GameObject shot;
	public GameObject shoo2;
	public GameObject start;
	public GameObject creationeffect;
	public GameObject Dieeffect;
	public List<Transform> path;
	public Image BossHealthBar;
	public float bosshealth;
	public AudioClip bosscreation;

	float maxhealth;
	GameObject go;
	bool m_CanFire;
	Animator animator;
	PlayerController playercontroller;
	GameObject boss; 

	// Use this for initialization
	void Start () {
		maxhealth = bosshealth;
		animator = this.gameObject.GetComponent<Animator> ();
		playercontroller = GameObject.Find ("PlayerController").GetComponent<PlayerController> ();
		m_CanFire = true;
		StartCoroutine (BossSpawning ());
		//iTween.MoveTo(this.gameObject,iTween.Hash("position",player.transform.position,"time",2f));
		//iTween.LookTo (this.gameObject, player.transform.position, 0.0f);
		//this.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
	}
	void CoolDown()
	{
		m_CanFire = true;
	}
	void Update()
	{
		iTween.LookUpdate (this.gameObject, player.transform.position, 0.1f);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerShot") {

		 float impact =	other.gameObject.GetComponent<WeaponImpact>().impactcapacity;
			Sethealth(impact);
			Debug.Log("Inside Boss Trigger");
		}
	}


	IEnumerator BossSpawning()
	{
		while (true) {
			if(path.Count > 0)
			{
				if(creationeffect!= null)
				boss = 	Instantiate(creationeffect,this.gameObject.transform.GetChild (4).gameObject.transform.position,this.gameObject.transform.rotation) as GameObject;
				this.gameObject.GetComponent<AudioSource>().clip = bosscreation;
				if(!this.gameObject.GetComponent<AudioSource>().isPlaying)
					this.gameObject.GetComponent<AudioSource>().Play();
				animator.SetBool("holdgun",true);
			yield return new WaitForSeconds(1.5f);
			//animator.SetBool("holdgun",false);
			animator.SetBool("shot",true);
			yield return new WaitForSeconds(1f);
			for(int i=0;i<15;i++)
			{
				FireShot();
				yield return new WaitForSeconds(0.6f);
				
			}
			animator.SetBool("holdgun",false);
			animator.SetBool("shot",false);
			yield return new WaitForSeconds(1f);
	//		this.gameObject.SetActive(false);
	//		yield return new WaitForSeconds(2f);
			int random = Random.Range(0,path.Count);
			this.gameObject.transform.position = path[random].position;
			}
		}
		yield return 0;
	}

	void FireShot()
	{
		
			Vector3 bulletVel = (player.transform.position - start.transform.position).normalized * m_StartVel;
			go = GameObject.Instantiate (shot,
			                             start.transform.position,
			                             start.transform.rotation) as GameObject;
			go.transform.GetComponent<Rigidbody>().AddForce(bulletVel * 40f);
			//m_CanFire = false;
			//Invoke("CoolDown",0.8f);
		
	}

	void Sethealth(float impact)
	{
		bosshealth = bosshealth - impact;
		BossHealthBar.fillAmount = bosshealth / maxhealth;

		if (bosshealth <= 0) {
			if(Dieeffect != null)
				Instantiate(Dieeffect,this.gameObject.transform.GetChild (4).gameObject.transform.position,this.gameObject.transform.rotation);

			Destroy (this.gameObject);
			playercontroller.bossalive = false;
			playercontroller.levelcompleted ();
		}

	}

}
