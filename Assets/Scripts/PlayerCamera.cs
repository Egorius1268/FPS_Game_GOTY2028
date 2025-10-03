using UnityEngine;

public class PlayerCamera: MonoBehaviour
{
    private float xRotation = 0f;

    [SerializeField] public float mouseSensitivity = 2f;
    public float yRotationLimit = 90f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -yRotationLimit, yRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
