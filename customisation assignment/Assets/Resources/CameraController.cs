using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public GameObject MainCamera;

    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float xRot;
    public float yRot;
    private bool paused;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        paused = false;
    }

    void Update()
    {
        float horizontal = horizontalSpeed * Input.GetAxis("Mouse X");
        float vertical = -verticalSpeed * Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizontal * mouseSensitivity * Time.deltaTime, 0, Space.World);
        transform.Rotate(vertical * mouseSensitivity * Time.deltaTime, 0, 0, Space.Self);

        xRot = transform.localEulerAngles.x;
        if (xRot > 180)
            xRot -= 360;
        xRot = Mathf.Clamp(xRot, -30f, 0f);

        yRot = transform.localEulerAngles.y;
        if (yRot > 180)
            yRot -= 360;
        yRot = Mathf.Clamp(yRot, -15f, 15f);


        transform.localEulerAngles = new Vector3(xRot, yRot, transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            Pause();
        }
    }

    private void Pause()
    {
        if (!paused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            paused = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            paused = false;
        }

    }
}

