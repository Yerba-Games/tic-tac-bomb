using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float classSpeed,classJump;
    [SerializeField] private Camera playerCamera;
    private float inputX, inputZ;
    private Rigidbody rb;
    PlayerMovemnt Movemnt;
    private InputAction move;
    private Vector3 PlayerMovementInput;
    private Vector3 forceDirection = Vector3.zero;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Movemnt = new PlayerMovemnt();

    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * classSpeed;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * classSpeed;
        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > classSpeed * classSpeed)
            rb.velocity = horizontalVelocity.normalized * classSpeed + Vector3.up * rb.velocity.y;
        LookAt();
    }
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void OnEnable()
    {
        Movemnt.Player.Jump.started += DoJump;
        move = Movemnt.Player.Move;
        Movemnt.Player.Enable();
    }
    private void OnDisable()
    {
        Movemnt.Player.Jump.started -= DoJump;
        Movemnt.Player.Move.Disable();
    } 
    private void DoJump(InputAction.CallbackContext obj)
    {
        Debug.Log("jump");
        if (IsGrounded())
        {
           forceDirection += Vector3.up * classJump;
            Debug.Log("dzi³a");
        }
    }
    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;

    }

    private bool IsGrounded()
    {
        //Debug.DrawRay();
        Ray ray = new Ray(this.transform.position - (Vector3.up * 0.25f), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false;
    }
}
