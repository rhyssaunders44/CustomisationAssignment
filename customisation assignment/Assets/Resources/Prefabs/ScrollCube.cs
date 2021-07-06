using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(18);

            Destroy(this.gameObject);
        }
    }
}
