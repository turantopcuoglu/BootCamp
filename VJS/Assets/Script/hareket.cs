using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hareket : MonoBehaviour
{
    private static hareket instance;
    public static hareket Instance { get { return instance; } }

    public Rigidbody controller;
    [SerializeField] private float hareketHizi = 5.0f, Surtunme = 0.5f, DonmeHizi = 25.0f;

    [SerializeField] private float fallHeight = -35f;

    private Transform camTransform;

    public FixedJoystick joystick;
    void Start()
    {
       instance=this; 
        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = DonmeHizi;
        controller.drag = Surtunme;

        camTransform = Camera.main.transform;
    }

    void Update()
    {
        if(Time.time < 3f) { return; }

        Vector3 xyz = Vector3.zero;

        //if keyboard
        xyz.x = Input.GetAxis("Horizontal");
        xyz.z = Input.GetAxis("Vertical");

        if(xyz ==  Vector3.zero )
        {
			// if joystick 
			xyz.x = joystick.Horizontal;
			xyz.z = joystick.Vertical;
		}

        xyz.Normalize();

        // Camera Rotasyon 
        
        Vector3 rotatexyz = camTransform.TransformDirection(xyz);
        rotatexyz = new Vector3(rotatexyz.x, 0, rotatexyz.z);
        rotatexyz = rotatexyz.normalized * xyz.magnitude * Time.deltaTime;

        controller.AddForce(rotatexyz * hareketHizi);

        if(transform.position.y < fallHeight)
        {
            LoadSameLevel();
        }
    }

    private void LoadSameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
