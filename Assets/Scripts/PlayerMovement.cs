using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private CinemachineVirtualCamera playeCam;

    private Vector3 mousePos;
    private Vector3 previousMousePos;

    public float moveSpeed;
    public float offsetAngle = 90f;
    public float rotationSpeed;
    public float stopRotationThreshold;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        previousMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        previousMousePos.z = 0;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Rotation();
        Movement();
    }

    private void Rotation()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        float distanceToMouse = Vector2.Distance(transform.position, mousePos);

        if (distanceToMouse > stopRotationThreshold)
        {
            Vector2 direction = (Vector2)mousePos - (Vector2)transform.position;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            targetAngle -= offsetAngle;

            float rotationSpeedAdjustment = Mathf.Clamp(distanceToMouse, 0, 1) * rotationSpeed;

            rb.rotation = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeedAdjustment * Time.deltaTime);
        }
        else
        {
            return;
        }
    }

    private void Movement()
    {
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = transform.up;

        rb.velocity = moveDirection * moveSpeed * verticalInput;
    }
}
