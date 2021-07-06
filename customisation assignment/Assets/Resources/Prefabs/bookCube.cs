using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.AddItem(5);

            Destroy(this.gameObject);
        }
    }
}
