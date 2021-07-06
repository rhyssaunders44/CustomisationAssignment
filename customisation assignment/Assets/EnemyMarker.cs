using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMarker : MonoBehaviour
{

    public GameObject EnemyCanvas;
    public GameObject Enemy;
    public GameObject Player;
    public Transform lookat;
    // Start is called before the first frame update
    void Start()
    {
        lookat = Player.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCanvas.transform.LookAt(lookat);
    }
}
