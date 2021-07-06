using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(17);

            Destroy(this.gameObject);
        }
    }
}
