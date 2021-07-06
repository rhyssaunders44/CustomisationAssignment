using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(9);

            Destroy(this.gameObject);
        }
    }
}
