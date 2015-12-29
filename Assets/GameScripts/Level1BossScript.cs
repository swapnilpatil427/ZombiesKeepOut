using UnityEngine;
using System.Collections;

public class Level1BossScript : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		//StartCoroutine (BossSpawning ());
	}
	
	// Update is called once per frame
	void Update () {
	//	iTween.LookTo (this.gameObject, player.transform.position, 0.0f);
//		float dot = Vector3.Dot (this.gameObject.transform.position, player.transform.position);
//		dot = dot/(this.gameObject.transform.position.magnitude*player.transform.position.magnitude);
//		var acos = Mathf.Acos(dot);
//		var angle = acos*180/Mathf.PI;
//		print(angle);
		//iTween.RotateTo(this.gameObject,iTween.Hash("rotation",player.transform.position,"time",0f));
	}

}
