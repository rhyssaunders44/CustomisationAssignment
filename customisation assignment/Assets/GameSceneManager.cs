using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] public static bool loadCharacter;

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            loadCharacter = true;
        }
        else
        {
            loadCharacter = false;
        }
    }

    public void LoadNewScene() 
    {
        SceneManager.LoadSceneAsync(1);
    }
}
