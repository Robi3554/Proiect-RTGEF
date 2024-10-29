using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    private Transform enemyTransform = null;

    public float rotationSpeed;

    private bool isLockedOn = false;

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

    private IEnumerator ShootingStart()
    {
        yield return new WaitForSeconds(1);

        isLockedOn = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            enemyTransform = col.transform;
            StartCoroutine(ShootingStart());
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && isLockedOn)
        {
            BroadcastMessage("OnParentStay2D", col, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            enemyTransform = null;
            StopCoroutine(ShootingStart());
            isLockedOn = false;
        }
    }
}
