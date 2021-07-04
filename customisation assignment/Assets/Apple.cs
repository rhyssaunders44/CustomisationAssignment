using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {  
            UIManager.AddItem(1);

            Destroy(this.gameObject);
        }
    }

}
