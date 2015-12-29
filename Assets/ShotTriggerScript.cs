using UnityEngine;
using System.Collections;

public class ShotTriggerScript : MonoBehaviour {
	float impact;
	PlayerController playercontroller;
	void Start()
	{
		playercontroller = GameObject.Find ("PlayerController").GetComponent<PlayerController> ();
	}
	void OnTriggerEnter(Collider other)
	{
	
		if (other.gameObject.tag == "BossShot") {
			impact = other.gameObject.GetComponent<WeaponImpact>().impactcapacity;
			playercontroller.SetHealth(impact);
			Debug.Log ("Inside"+impact);
		}
	}
}
