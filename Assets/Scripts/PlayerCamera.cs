using UnityEngine;
using DG.Tweening;

public class PlayerCamera: MonoBehaviour
{
    private float xRotation = 0f;
    public Camera playerCamera;
    //public Transform player;
    public float mouseSensitivity = 2f;
    public float yRotationLimit = 90f;
    public Transform camHolder;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -yRotationLimit, yRotationLimit);
        camHolder.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void DoFov(float endValue)
    {
        playerCamera.DOFieldOfView(endValue, 0.25f);
    }

    /*public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }*/
}
