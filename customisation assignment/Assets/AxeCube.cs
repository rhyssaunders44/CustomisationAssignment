using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(3);

            Destroy(this.gameObject);
        }
    }
}
