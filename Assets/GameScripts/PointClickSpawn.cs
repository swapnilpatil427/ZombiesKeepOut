using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PointClickSpawn : MonoBehaviour {
	GameObject go;
	public List<GameObject>	m_FX;
	public List<Sprite> m_FX_images;
	public Image weaponimage;
	public Texture 			tex;
	public Texture 			tex2;
	private int				m_CurrentFx = 0;
	public Transform		m_StartPoint;
	public PKFxFX			m_FlameThrower;
	public float		m_StartVel = 50.0f;
	private bool		m_CanFire = true;
	public List<float>		m_CoolDowns;
	public Texture			m_Crossheir;
	public Text FiringType;
	public AudioClip gaussfire;
	public AudioClip romancandlefire;
	public AudioClip boom;

	void Start()
	{
		Cursor.visible = false;
		FiringType.text =  m_FX[m_CurrentFx].name;
		weaponimage.sprite = m_FX_images [m_CurrentFx];
	}
	void CoolDown()
	{
		m_CanFire = true;
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width / 2 - m_Crossheir.width / 2,
		                         Screen.height / 2 - m_Crossheir.height / 2,
		                         m_Crossheir.width, m_Crossheir.height),
		                m_Crossheir);

	}

	IEnumerator HitZombies(GameObject zombie)
	{
		float waitime = 0;
		ZombieMover zombieobject = zombie.GetComponent<ZombieMover> ();
		//zombieobject.ShowEffect ();
		if(zombieobject !=null)
		zombieobject.FireEffect (true);
		if (zombieobject != null) {
			while (waitime <= 3) {
				if (zombieobject.health > 0)
					zombieobject.ReduceHealth (2f);
				yield return new WaitForSeconds (0.4f);
				waitime += 0.4f;
			}
		}
		if(zombieobject !=null)
		zombieobject.FireEffect (false);
	}
	
	void Update () {
	

		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			if (Cursor.visible == true)
				Cursor.visible = false;
			else 
				Cursor.visible = true;
		}
		float d = Input.GetAxis("Mouse ScrollWheel");
		if (d > 0f)
		{
			m_CurrentFx = (m_CurrentFx - 1) % m_FX.Count;
			if (m_CurrentFx < 0)
				m_CurrentFx = m_FX.Count + m_CurrentFx;
			FiringType.text =  m_FX[m_CurrentFx].name;
			weaponimage.sprite = m_FX_images [m_CurrentFx];
		}
		else if (d < 0f)
		{
			m_CurrentFx = (m_CurrentFx + 1) % m_FX.Count;
			FiringType.text =  m_FX[m_CurrentFx].name;
			weaponimage.sprite = m_FX_images [m_CurrentFx];
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			m_CurrentFx = (m_CurrentFx + 1) % m_FX.Count;
			FiringType.text =  m_FX[m_CurrentFx].name;
			weaponimage.sprite = m_FX_images [m_CurrentFx];
		}
			
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			m_CurrentFx = (m_CurrentFx - 1) % m_FX.Count;
			if (m_CurrentFx < 0)
				m_CurrentFx = m_FX.Count + m_CurrentFx;
			FiringType.text =  m_FX[m_CurrentFx].name;
			weaponimage.sprite = m_FX_images [m_CurrentFx];
		}
	
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
			RaycastHit hit = new RaycastHit();
			GameObject prefab = m_FX[m_CurrentFx];
		
			if(m_CurrentFx >2)
			{
				if(Physics.Raycast(ray,out hit, 500))
				{
					GameObject hitobject = hit.transform.gameObject;
					go = Instantiate(prefab, hit.point + hit.normal.normalized / 10.0f, prefab.transform.rotation) as GameObject;
					go.transform.Rotate(Quaternion.FromToRotation(Vector3.up, hit.normal).eulerAngles);
					go.transform.Translate(prefab.transform.position);
					if(hitobject.tag == "Zombie")
					{
						StartCoroutine(HitZombies(hitobject));
					}
				}
				if(FiringType.text.ToLower() == "boom")
				{
					gameObject.GetComponent<AudioSource>().clip = boom;
				}
				//GameObject prefab = m_FX[m_CurrentFx];

			}

			//Ray r = Camera.main..ScreenPointToRay(Input.mousePosition);
		//	RaycastHit rh = new RaycastHit();
			else
				{
			Vector3 aimPoint = ray.GetPoint(500.0f);
			if (Physics.Raycast(ray, out hit))
				aimPoint = hit.point;
			
			Vector3 bulletVel = (aimPoint - m_StartPoint.position).normalized * m_StartVel;
			
			go = GameObject.Instantiate(prefab,
			                                       m_StartPoint.position,
			                                       m_StartPoint.rotation) as GameObject;
				if(FiringType.text == "Gauss")
				{
					gameObject.GetComponent<AudioSource>().clip = gaussfire;
				}
				else
				{
					gameObject.GetComponent<AudioSource>().clip = romancandlefire;
				}

				go.transform.GetComponent<Rigidbody>().AddForce(bulletVel + new Vector3((Random.value-0.5f)*20.0f,
			                                                                        (Random.value-0.5f)*10.0f,0.0f));
			m_CanFire = false;
					Invoke("CoolDown", m_CoolDowns[m_CurrentFx]);
				}
			gameObject.GetComponent<AudioSource>().Play();
		}
		//else if (Input.GetMouseButton(1))
		//	m_FlameThrower.StartEffect();
		else
			m_FlameThrower.StopEffect();
	}
}
