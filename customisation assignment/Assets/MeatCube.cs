using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(13);

            Destroy(this.gameObject);
        }
    }
}
