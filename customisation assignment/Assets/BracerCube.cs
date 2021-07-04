using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracerCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(7);

            Destroy(this.gameObject);
        }
    }
}
