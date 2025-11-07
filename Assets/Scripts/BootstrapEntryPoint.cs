using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

namespace EntryPoint.Scripts
{
    public class BootStrapEntrypoint : MonoBehaviour
    {
        private IEnumerator Start()
        {
            //throw new NotImplementedException();
            //инициализировать геймплейные штуки 
            //от объектов, менеджеры и т.д
            var loadingDuration = 4f;
            while (loadingDuration > 0f)
            {
                loadingDuration -= Time.deltaTime;
                Debug.Log("Loading...........");
                yield return null;
            }

            Debug.Log("Everything is initialised");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}