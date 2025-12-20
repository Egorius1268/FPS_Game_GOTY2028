using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int playerStartHP = 150;
    
    
    public PlayerMovementNew playerMovement;
    public TMP_Text hpDisp;
    public GameObject deathPanel;
    public GameObject pauseManager;
    public GameObject levelCompletePanel;
    public PlayerCamera playerCamera;

    

 private void Awake()
 {
     deathPanel.SetActive(false);
     levelCompletePanel.SetActive(false);
     playerMovement.CursorLock();
     Time.timeScale = 1;
     deathPanel.SetActive(false);
     playerCamera.GetComponent<PlayerCamera>().enabled = true;
     
 }

 private void Start()
 {
   Time.timeScale = 1;
   
 }

 public void GameOver()
 {
     playerMovement.CursorUnlock();  
     Debug.Log("Game Over");
     Time.timeScale = 0;
     deathPanel.SetActive(true);
     playerCamera.GetComponent<PlayerCamera>().enabled = false;
 }
 public void LevelComplete()
 {
     levelCompletePanel.SetActive(true);
     playerMovement.CursorUnlock();  
     Debug.Log("Game Over");
     Time.timeScale = 0;
     playerCamera.GetComponent<PlayerCamera>().enabled = false;
 }
}
