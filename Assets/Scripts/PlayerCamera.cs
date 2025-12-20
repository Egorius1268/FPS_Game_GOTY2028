using UnityEngine;
using DG.Tweening;

public class PlayerCamera: MonoBehaviour
{
    private float xRotation = 0f;
    //private float yRotation = 0f; 
    public Camera playerCamera;
    //public Transform player;
    public float mouseSensitivity = 2f;
    public float yRotationLimit = 90f;
    public Transform camHolder;

    private float currentCameraTilt = 0f;
    private Vector3 originalRotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //originalRotation = transform.localEulerAngles;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        
        
        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -yRotationLimit, yRotationLimit);
        //transform.localRotation = Quaternion.Euler(xRotation, yRotation, currentCameraTilt);
        camHolder.transform.localRotation = Quaternion.Euler(xRotation, 0f, currentCameraTilt);
    }

    public void DoFov(float endValue)
    {
        playerCamera.DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt, float duration = 0.25f)
    {
        DOTween.To(() => currentCameraTilt, x => currentCameraTilt = x, zTilt, duration)
            .SetEase(Ease.OutQuad);
    }
}
