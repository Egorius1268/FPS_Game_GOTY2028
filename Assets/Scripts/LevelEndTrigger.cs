using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.LevelComplete();
        }
    }   
    
}