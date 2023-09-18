using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnCollusion : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private void OnCollisionEnter(Collision other)
    {
        // It other's impulse.magnitude is greater than the speed,
        // destroy the object that this class is attached to.
        if (other.impulse.magnitude > speed)
            Destroy(gameObject);
    }
}
