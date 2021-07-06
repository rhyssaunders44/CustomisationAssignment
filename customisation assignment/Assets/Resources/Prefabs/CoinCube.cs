using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.currency = UIManager.currency + Random.Range(1, 201);

            Destroy(this.gameObject);
        }
    }
}
