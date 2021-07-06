using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootcube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(6);

            Destroy(this.gameObject);
        }
    }
}
