using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
    
{
    private void Awake()
    {
        this.enabled = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.WakeUp();
        PlayerCamera cam = gameObject.GetComponent<PlayerCamera>();
        cam.enabled = true;
    }
    
    public Gun gun;
    public Camera playerCamera;
    public float curSpeedX = 0;
    public float startWalkSpeed = 13f;
    public float startRunSpeed = 26f;
    public float crouchSpeed = 7f;
    public float walkSpeed = 13f;
    public float runSpeed = 26f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float speedBoostPerJump = 2f;
    public float speedBoostDuration = 3f;
    public float speedDecayRate = 2f;
    
    private float currentSpeedBoost = 0f;
    private float speedBoostTimer = 0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private bool wasGrounded = false;
    
    void Start()
    
    {
        characterController = GetComponent<CharacterController>();
        CursorLock();
    }
    
    void Update()

    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentWalkSpeed = walkSpeed + currentSpeedBoost;
        float currentRunSpeed = runSpeed + currentSpeedBoost;
        
        curSpeedX =  (isRunning ? currentRunSpeed : currentWalkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY =  (isRunning ? currentRunSpeed : currentWalkSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }

        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (characterController.isGrounded && !wasGrounded)
        {
            currentSpeedBoost += speedBoostPerJump;
            speedBoostTimer = speedBoostDuration;
            //playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, walkSpeed, Time.deltaTime);
        }

        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                currentSpeedBoost = Mathf.Max(0, currentSpeedBoost - speedDecayRate * 2);
            }
        }
        else if(currentSpeedBoost > 0)
        {
            currentSpeedBoost = Mathf.Max(0, currentSpeedBoost - speedDecayRate * 2);
        }
       
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        
        wasGrounded = characterController.isGrounded;
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            characterController.height = crouchHeight;

        }

        else
        
        {
            characterController.height = defaultHeight;
            
           
        }
        
        characterController.Move(moveDirection * Time.deltaTime);

        if (Input.GetKey(KeyCode.R) && gun.currentAmmo >= 0)
        {
            gun.Reload();
        }
        
    }

    
    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}