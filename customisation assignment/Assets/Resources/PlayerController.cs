using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Transform spawnPoint;

    public float movementSpeed;
    public float rotateSpeed = 0.01F;
    public Animator animation;

    public void Start()
    {
        //movement speed affected by stats
        movementSpeed = DataMaster.characterStats[0][2];
    }

    void FixedUpdate()
    {
        //animation trigger
        CharacterController controller = GetComponent<CharacterController>();

        if (Input.GetKey(KeyCode.W))
        {
            animation.SetFloat("Speed", 1);
        }
        else
        {
            animation.SetFloat("Speed", 0);
        }
      
        //crouch walk and run speeds
        if (Input.GetKey(KeyCode.LeftShift) && player.AssignableStatManager.regenStats[1][1] > 5)
        {
            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = movementSpeed * 1.5f * Input.GetAxis("Vertical");
            controller.SimpleMove(forward * curSpeed);
        }
        if(Input.GetKey(KeyCode.C))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = movementSpeed  * 0.5f * Input.GetAxis("Vertical");
            controller.SimpleMove(forward * curSpeed);
        }

        else
        {
            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = movementSpeed * Input.GetAxis("Vertical");
            controller.SimpleMove(forward * curSpeed);
        }

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        if (player.AssignableStatManager.dead)
        {
            controller.transform.position = spawnPoint.position;
        }
    }
}


