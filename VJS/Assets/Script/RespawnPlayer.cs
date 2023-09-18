using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is to destroy the ball when it drops over the scene.
public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] float resetHeight = -35f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= resetHeight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }
    }
}
