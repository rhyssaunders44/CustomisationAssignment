using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public GameObject MainCamera;
    public GameObject MainCameraSpot;
    public GameObject SecondaryCameraSpot;
    private bool locked;
    private bool firstPerson;

    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float xRot;
    public float yRot;
    float horizontal;
    float vertical;

    private void Start()
    {
        //if(GameSceneManager.loadCharacter)
            //Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        horizontal = horizontalSpeed * Input.GetAxis("Mouse X");
        vertical = -verticalSpeed * Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.R))
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

        if (Input.GetKeyDown(KeyCode.V))
        {
            SetFirstPerson();
        }

        if (locked)
        {
            return;
        }
        else
        {
            if (firstPerson)
            {
                MainCamera.transform.position = SecondaryCameraSpot.transform.position;
            }
            else
            {
                MainCamera.transform.position = MainCameraSpot.transform.position;
            }
            RotateCamera(MainCamera);

        }

    }

    public void SetFirstPerson()
    {
        if (firstPerson)
        {
            firstPerson = false;
        }
        else
        {
            firstPerson = true;
        }
    }

    public void SetLock()
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

    public void RotateCamera(GameObject selectedCamera)
    {
        //as long as the main camera isn't locked, it can rotate within clamps
        selectedCamera.transform.Rotate(0, horizontal * mouseSensitivity * Time.deltaTime, 0, Space.World);
        selectedCamera.transform.Rotate(vertical * mouseSensitivity * Time.deltaTime, 0, 0, Space.Self);

        xRot = selectedCamera.transform.localEulerAngles.x;
        if (xRot > 180)
            xRot -= 360;
        xRot = Mathf.Clamp(xRot, -60f, 20f);

        yRot = selectedCamera.transform.localEulerAngles.y;
        if (yRot > 180)
            yRot -= 360;
        yRot = Mathf.Clamp(yRot, -30f, 30f);


        selectedCamera.transform.localEulerAngles = new Vector3(xRot, yRot, transform.localEulerAngles.z);
    }
}

