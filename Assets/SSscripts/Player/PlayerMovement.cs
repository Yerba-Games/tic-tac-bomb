using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float classSpeed;
    private float inputX, inputZ;
    private Rigidbody rb;
    PlayerMovemnt Movemnt;
    private void awake()
    {
        rb = GetComponent<Rigidbody>();
        Movemnt = new PlayerMovemnt();
        Movemnt.Player.Move.performed += ctx => Debug.Log(ctx.ReadValueAsObject());
    }

    private void OnEnable()
    {
        Movemnt.Player.Move.Enable();
    }
    private void OnDisable()
    {
        Movemnt.Player.Move.Disable();
    }
}
