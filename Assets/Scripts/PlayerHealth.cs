using System;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public GameManager gameManager;
    public TMP_Text hpDisp;
    
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = 150;
    }
    private void Start()
    {
        UpdateHPDisplay();
    }
    
    private void UpdateHPDisplay()
    {
        hpDisp.text = currentHealth.ToString();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHPDisplay();
        if (currentHealth <= 0)
        {
             gameManager.GameOver();
        }
    }

}
