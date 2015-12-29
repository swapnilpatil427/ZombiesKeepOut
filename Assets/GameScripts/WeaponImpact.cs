using UnityEngine;
using System.Collections;

public class WeaponImpact : MonoBehaviour {
	public float impactcapacity;

	// Use this for initialization
	void Start () {
	
		StartCoroutine (Die ());
	}



	IEnumerator Die()
	{
		yield return new WaitForSeconds (3);
		PKFxFX fx = this.gameObject.GetComponent<PKFxFX>();
		if (fx != null)
		{

			fx.StopEffect();
		}
		Destroy (this.gameObject);
	}
}
