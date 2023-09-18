using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMotora : MonoBehaviour
{
    private const float ZAMAN_BASLA = 3f;
    private float startTime;

    public Transform bakis;
    public Vector3 Denge, istenilenPozisyon;
    private Vector2 touchPosition;
    private float kaydirmaHizi = 200.0f;
    [SerializeField] private float mesafe = 19.0f, puruzHizi = 7.5f, yDenge = 10f;

    void Start()
    {
        Denge = new Vector3(0f, yDenge, 1 * mesafe);
    }
    void Update()
    {
        // Wait till ZAMAN_BASLA time passes till the game starts.

        if (Time.time - startTime < ZAMAN_BASLA)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SlideCamera(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SlideCamera(false);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            float swpForce = touchPosition.x - Input.mousePosition.x;
            if (Mathf.Abs(swpForce) > kaydirmaHizi)
            {
                if (swpForce < 0)
                    SlideCamera(true);
                else
                    SlideCamera(false);
            }
        }




    }

    private void FixedUpdate()
    {
		if (Time.time - startTime < ZAMAN_BASLA)
			return;

		istenilenPozisyon = bakis.position + Denge;
        transform.position = Vector3.Lerp(transform.position, istenilenPozisyon, puruzHizi * Time.deltaTime);
        transform.LookAt(bakis.position + Vector3.up);
    }

    public void SlideCamera(bool left)
    {
        if (left)
            Denge = Quaternion.Euler(0, 90, 0) * Denge;
        else
            Denge = Quaternion.Euler(0, -90, 0) * Denge;
    }
}
