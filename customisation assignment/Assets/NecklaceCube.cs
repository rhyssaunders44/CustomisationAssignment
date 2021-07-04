using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecklaceCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(15);

            Destroy(this.gameObject);
        }
    }
}
