using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(21);

            Destroy(this.gameObject);
        }
    }
}
