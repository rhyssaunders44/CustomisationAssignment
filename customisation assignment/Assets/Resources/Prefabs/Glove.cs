using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(10);

            Destroy(this.gameObject);
        }
    }
}
