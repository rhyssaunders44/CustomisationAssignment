using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggTalk : MonoBehaviour
{

    public Canvas canvas;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.LookAt(Player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.chatting = true;
            UIManager.StartChat();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.chatting = false;
        }
    }
}
