using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is to destroy the invisible wall which hides the
// key to move forward.
public class KeyWall : MonoBehaviour
{
    [SerializeField] private Transform wall;

    private void OnCollisionEnter(Collision other)
    {
        if (wall != null)
        {
            Destroy(wall.gameObject);
        }
    }
}
