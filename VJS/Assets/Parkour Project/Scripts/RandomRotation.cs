using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
	private Vector3 rotVector = Vector3.zero;
	public float rotSpeed = 15.0f;
	void Start()
	{
		float x = Random.Range(0, 2);
		float y = Random.Range(0, 2);
		float z = Random.Range(0, 2);

		if (x == 0 & y == 0 & z == 0)
		{
			x = 1;
			y = 1;
			z = 1;
		}

		rotVector = new Vector3(x, y, z);
	}

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(rotVector * rotSpeed * Time.deltaTime);
	}
}
