using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloakCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(8);

            Destroy(this.gameObject);
        }
    }
}
