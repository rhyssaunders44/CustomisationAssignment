using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(4);

            Destroy(this.gameObject);
        }
    }
}
