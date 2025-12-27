using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{

    private int levelLoadIndex;
    private void Start()
    {
        levelLoadIndex = SceneManager.GetActiveScene().buildIndex;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(levelLoadIndex + 1, LoadSceneMode.Single);
        }
    }
}