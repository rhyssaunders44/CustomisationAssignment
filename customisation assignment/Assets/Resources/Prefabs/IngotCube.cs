using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngotCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(12);

            Destroy(this.gameObject);
        }
    }
}
