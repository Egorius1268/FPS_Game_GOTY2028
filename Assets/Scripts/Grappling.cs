using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")] 
    private PlayerMovementNew pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lineRenderer;
    
    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;

    private Vector3 grapplePoint;
    
    [Header("Cooldown")]
    public float grapplingCooldown;
    private float grapplingCooldownTimer;
    
    
    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;
    
    private bool grappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovementNew>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();
        
        if(grapplingCooldownTimer > 0)
            grapplingCooldownTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (grappling)
            lineRenderer.SetPosition(0, gunTip.position);
    }

    private void StartGrapple()
    {
        if (grapplingCooldownTimer > 0) return;
        
        grappling = true;

        pm.freeze = true;
        
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
            
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        pm.freeze = false;
    }

    private void StopGrapple()
    {   
        pm.freeze = false;
     
        grappling = false;
     
        grapplingCooldownTimer = grapplingCooldown;
     
        lineRenderer.enabled = false;
    }
}
