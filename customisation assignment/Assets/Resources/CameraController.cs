using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public GameObject MainCamera;
    private bool locked;

    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float xRot;
    public float yRot;

    void Update()
    {
        float horizontal = horizontalSpeed * Input.GetAxis("Mouse X");
        float vertical = -verticalSpeed * Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (locked)
            {
                locked = false;
            }
            else
            {
                locked = true;
            }
        }

        if (locked)
        {
            return;
        }
        else
        {
            //as long as the main camera isn't locked, it can rotate within clamps
            MainCamera.transform.Rotate(0, horizontal * mouseSensitivity * Time.deltaTime, 0, Space.World);
            MainCamera.transform.Rotate(vertical * mouseSensitivity * Time.deltaTime, 0, 0, Space.Self);

            xRot = MainCamera.transform.localEulerAngles.x;
            if (xRot > 180)
                xRot -= 360;
            xRot = Mathf.Clamp(xRot, -60f, 20f);

            yRot = MainCamera.transform.localEulerAngles.y;
            if (yRot > 180)
                yRot -= 360;
            yRot = Mathf.Clamp(yRot, -30f, 30f);


            MainCamera.transform.localEulerAngles = new Vector3(xRot, yRot, transform.localEulerAngles.z);
        }

    }
}

