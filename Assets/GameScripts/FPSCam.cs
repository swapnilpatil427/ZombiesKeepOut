using UnityEngine;
using System.Collections;

public class FPSCam : MonoBehaviour {
	public Camera 				m_Camera;
	public CharacterController	m_Controller;
	private Vector3				m_Velocity;

	public GameObject			m_Pivot;
	public float				m_Speed = 5.0f;

	public Texture			tex;
	bool pause;
	Coroutine Playwalkingsound;
	void Start()
	{
		if (QualitySettings.desiredColorSpace != ColorSpace.Linear)
			Debug.LogWarning("The rendering color space is not set to linear, " +
				"colors won't be accurate\n" +
				"To change this setting go to \"Player Settings\">\"Other Settings\">\"Color Space\"");
	}

	void OnGUI()
	{
	//	if (tex != null)
	//		GUI.DrawTexture(new Rect(10, 10, tex.width/2.0f, tex.height/2.0f), tex);
	}

	void Update () {
		if (m_Controller.isGrounded)
		{
			m_Velocity = Vector3.zero;


			if(Input.GetKeyDown(KeyCode.W))
				if(Playwalkingsound == null)
				Playwalkingsound = StartCoroutine(playwalkingsound());
			if(Input.GetKeyUp(KeyCode.W))
			  {
			   if(Playwalkingsound != null){
			   StopCoroutine(Playwalkingsound);
					Playwalkingsound = null;
				}
			}
			 //  StopCoroutine(playwalkingsound);
			if (Input.GetKey(KeyCode.W))
			{
				m_Velocity += (this.transform.forward) * m_Speed;
			}

			if(Input.GetKeyDown(KeyCode.S))
			{
				if(Playwalkingsound == null)
				Playwalkingsound = StartCoroutine(playwalkingsound());
			}
			if(Input.GetKeyUp(KeyCode.S))
			{
				if(Playwalkingsound != null){
					StopCoroutine(Playwalkingsound);
					Playwalkingsound = null;
				}
			}
			//  StopCoroutine(playwalkingsound);
			if (Input.GetKey(KeyCode.S))
			{
				m_Velocity += (-this.transform.forward) * m_Speed;
			}


			if(Input.GetKeyDown(KeyCode.A))
			{
				if(Playwalkingsound == null)
					Playwalkingsound = StartCoroutine(playwalkingsound());
			}
			if(Input.GetKeyUp(KeyCode.A))
			{
				if(Playwalkingsound != null){
					StopCoroutine(Playwalkingsound);
					Playwalkingsound = null;
				}
			}
			//  StopCoroutine(playwalkingsound);
			if (Input.GetKey(KeyCode.A))
			{
				m_Velocity += (-this.transform.right * m_Speed);
			}



			
			if(Input.GetKeyDown(KeyCode.D))
			{
				if(Playwalkingsound == null)
					Playwalkingsound = StartCoroutine(playwalkingsound());
			}
			if(Input.GetKeyUp(KeyCode.D))
			{
				if(Playwalkingsound != null){
					StopCoroutine(Playwalkingsound);
					Playwalkingsound = null;
				}
			}
			//  StopCoroutine(playwalkingsound);
			if (Input.GetKey(KeyCode.D))
			{
				m_Velocity += (this.transform.right * m_Speed);
			}

		


			if (Input.GetKey(KeyCode.Space))
				m_Velocity.y += 15.0f;
		}
		m_Velocity.y -= 0.9f;
		this.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
		m_Camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"),0,0));
		m_Controller.Move(m_Velocity * Time.deltaTime);
	}

	IEnumerator playwalkingsound()
	{
		while (true) {
			gameObject.GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds (0.5f);
		}
	}
}
