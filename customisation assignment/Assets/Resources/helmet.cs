using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helmet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            UIManager.AddItem(11);

            Destroy(this.gameObject);
        }
    }

}
