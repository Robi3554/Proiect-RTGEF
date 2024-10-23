using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    private Transform enemyTransform = null;

    public float rotationSpeed;

    void Start()
    {

    }

    void Update()
    {
        if (enemyTransform != null)
        {
            RotateTowards(enemyTransform.position);
        }
        else
        {
            RotateOrigin();
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void RotateOrigin()
    {
        Quaternion parentRotation = transform.parent.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, parentRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyTransform = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyTransform = null;
        }
    }
}
