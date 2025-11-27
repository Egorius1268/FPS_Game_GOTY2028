using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public PlayerMovementNew playerMovement;
    public TextMeshProUGUI text;

    void Start()
    {
        UpdateSpeedText();
    }

    private void Update()
    {
        UpdateSpeedText();
    }

    private void UpdateSpeedText()
    {
        text.text = $"{playerMovement.GetCurrentSpeed()}";
        
    }
    
}
