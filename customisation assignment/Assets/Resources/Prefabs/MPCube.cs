using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        {
            UIManager.AddItem(14);

            Destroy(this.gameObject);
        }
    }
}
