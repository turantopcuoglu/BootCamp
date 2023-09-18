using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boost : MonoBehaviour
{
	[SerializeField] private ParticleSystem _particleSystem;
	[SerializeField] private float boostSpeed = 15.0f, boostCoolDown = 2.0f, lastBoost;
	[SerializeField] private Button bt;

	public void Start()
	{
		lastBoost = Time.time;
		bt.interactable = false;

		PlayerPrefs.SetInt("Boost", 0);
	}
	public void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			bt.interactable = true;
			int i = PlayerPrefs.GetInt("Boost", 0);
			i += 2;
			PlayerPrefs.SetInt("Boost", i);


			_particleSystem.Play();
			Destroy(_particleSystem.gameObject, 2f);
			_particleSystem.transform.parent = null;


			gameObject.SetActive(false);
		}
	}

	public void Bost()
	{
		int i = PlayerPrefs.GetInt("Boost", 0);
		
		i--;
		PlayerPrefs.SetInt("Boost", i);
		if (i == 0)
		{
			bt.interactable = false;
			return;
		}


		hareket.Instance.controller.AddForce(hareket.Instance.controller.velocity.normalized * boostSpeed, ForceMode.VelocityChange);

	}


}
