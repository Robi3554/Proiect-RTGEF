using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float offsetAngle = 90f;
    public float rotationSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 direction = (Vector2)mousePos - (Vector2)transform.position;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetAngle -= offsetAngle;

        float distanceToMouse = Vector2.Distance(transform.position, mousePos);

        float rotationSpeedAdjustment = Mathf.Clamp(distanceToMouse, 0, 1) * rotationSpeed;

        rb.rotation = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeedAdjustment * Time.deltaTime);
    }

    private void Movement()
    {
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = transform.up;

        rb.velocity = moveDirection * moveSpeed * verticalInput;
    }
}
