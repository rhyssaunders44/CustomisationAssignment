using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantsCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(16);

            Destroy(this.gameObject);
        }
    }
}
