using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent (typeof(Rigidbody))]

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private float tiltUpTorque = 500f;
    [SerializeField]
    private float tiltDownTorque = 1000f;
    [SerializeField]
    private float rollTorque = 1000f;
    [SerializeField]
    private float powerTorque = 100f;
    [SerializeField]
    private float downUpTorque = 50f;
    [SerializeField]
    private float turningTorque = 50f;
    [SerializeField]
    private float boostMultiplier = 5f;
    public bool boosting = false;
    
    [SerializeField, Range(0.001f, 0.999f)]
    private float powerGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float upDownGlideReduction = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float turningGlideReduction = 0.111f;
    float glide, verticalGlide, horizontalGlide = 0f;

    Rigidbody rb;
    private float powerID;
    private float turningID;
    private float downUpID;
    private float rollID;
    private Vector2 tiltID;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();

    }

    void HandleMovement()
    {
        rb.AddRelativeTorque(Vector3.back * rollID * rollTorque * Time.deltaTime);

        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-tiltID.y, -1f, 1f) * tiltUpTorque * Time.deltaTime);

        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(tiltID.x, -1f, 1f) * tiltDownTorque * Time.deltaTime);

        if (powerID > 0.1f || powerID < -0.1f)
        {
            float currentSpeed = powerTorque;
            
            if (boosting)
            {
                currentSpeed = powerTorque * boostMultiplier;
            }
            else
            {
                currentSpeed = powerTorque;
            }
            
            rb.AddRelativeForce(Vector3.forward * powerID * currentSpeed * Time.deltaTime);
            glide = powerTorque;
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            glide = powerGlideReduction;
        }


        if (downUpID > 0.1f || downUpID < -0.1f)
        {
            rb.AddRelativeForce(Vector3.up * downUpID * downUpTorque * Time.fixedDeltaTime);
            verticalGlide = downUpID * downUpTorque;
        }
        else
        {
            rb.AddRelativeForce(Vector3.up * verticalGlide * Time.fixedDeltaTime);
            verticalGlide = upDownGlideReduction;
        }


        if (turningID > 0.1f || turningID < -0.1f)
        {
            rb.AddRelativeForce(Vector3.right * turningID * downUpTorque * Time.fixedDeltaTime);
            horizontalGlide = turningID * turningTorque;
        }
        else
        {
            rb.AddRelativeForce(Vector3.right * horizontalGlide * Time.fixedDeltaTime);
            horizontalGlide = turningGlideReduction;
        }
    }

    #region Input Methods
    public void OnPower(InputAction.CallbackContext context)
    {
        powerID = context.ReadValue<float>();
    }
    public void OnTurn(InputAction.CallbackContext context)
    {
        turningID = context.ReadValue<float>();
    }
    public void OnDownUp(InputAction.CallbackContext context)
    {
        downUpID = context.ReadValue<float>();
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        rollID = context.ReadValue<float>();
    }
    public void OnTilt(InputAction.CallbackContext context)
    {
        tiltID = context.ReadValue<Vector2>();
    }
    public void OnBoost(InputAction.CallbackContext context)
    {
        boosting = context.performed;
    }
    #endregion
}