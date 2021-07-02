using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool moving;
    [SerializeField] private Animation[] playerAnimations;
    [SerializeField] private KeyCode[] playerInputs;


    void Start()
    {
        playerInputs = new KeyCode[4];
        playerInputs[0] = KeyCode.W;
    }


    void Update()
    {
        if (Input.GetKey(playerInputs[0]))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        //if (moving)
        //{
        //    playerAnimations[1].Play();
        //}
        //else
        //{
        //    playerAnimations[1].Play();
        //}
    }
}
