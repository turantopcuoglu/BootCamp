using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Rigidbody rigidbody = other.GetComponent<Rigidbody>();
			if (rigidbody == null) { return; }
			if (rigidbody.velocity.magnitude > 2f)
			{
				Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

				foreach (Rigidbody rb in rbs)
				{
					if (rb == null) { continue; }
					rb.gameObject.transform.parent = null;
					rb.isKinematic = false;
					rb.AddExplosionForce(150f, other.gameObject.transform.position, 5f, 5f, ForceMode.Impulse);
					//rb.gameObject.GetComponent<MeshCollider>().enabled = false;
					Destroy(rb.gameObject, 5f);
				}

				GetComponent<BoxCollider>().enabled = false;

				Invoke(nameof(SelfDestroy), 5f);
			}
		}
	}

	private void SelfDestroy()
	{
		Destroy(gameObject);
	}
}
