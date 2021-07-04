using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(20);

            Destroy(this.gameObject);
        }
    }
}
