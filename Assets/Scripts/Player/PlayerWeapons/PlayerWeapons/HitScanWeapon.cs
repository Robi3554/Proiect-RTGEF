using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class HitScanWeapon : PlayerBasicWeapon
{
    private GameObject startSprite;
    private GameObject middleSprite;
    private GameObject endSprite;

    public LineRenderer lr;

    public float fadeTime;
    public float timeBetweenShots;

    public LayerMask enemyMask;
    public LayerMask destructibleMask;
    private LayerMask combinedMask;

    public GameObject startSpritePrefab;
    public GameObject middleSpritePrefab;
    public GameObject endSpritePrefab;

    protected override void Start()
    {
        base.Start();

        startSprite = Instantiate(startSpritePrefab);
        endSprite = Instantiate(endSpritePrefab);
        middleSprite = Instantiate(middleSpritePrefab);

        startSprite.SetActive(false);
        endSprite.SetActive(false);
        middleSprite.SetActive(false);

        combinedMask = enemyMask | destructibleMask;
    }

    protected override void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !GameManager.Instance.isPaused)
        {
            nextFireTime = Time.time + (1 / fireRate);

            StartCoroutine(Shoot());
        }
    }

    protected new IEnumerator Shoot()
    {
        for (int i = 0; i < shootCount; i++)
        {
            int damagedCount = 0;

            RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.position, firePoint.up, range, combinedMask);

            System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

            lr.SetPosition(0, firePoint.position);

            Vector3 finalPoint = firePoint.position + firePoint.up * range;

            AudioManager.Instance.PlaySFXClip(AudioManager.Instance.shooting);

            foreach (RaycastHit2D hit in hits)
            {
                bool shouldStop = false;

                Enemy enemy = hit.transform.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);

                    finalPoint = hit.point;

                    damagedCount++;

                    if (damagedCount >= pierce)
                    {
                        shouldStop = true;
                    }
                }
                else
                {
                    Debug.Log("Object Hit");
                    DestructibleObject destructible = hit.transform.GetComponent<DestructibleObject>();
                    if (destructible != null)
                    {
                        destructible.DamageObject();

                        finalPoint = hit.point;

                        damagedCount++;

                        if (damagedCount >= pierce)
                        {
                            shouldStop = true;
                        }
                    }
                }

                if (shouldStop)
                    break;
            }

                lr.SetPosition(1, finalPoint);

                startSprite.SetActive(true);
                endSprite.SetActive(true);
                middleSprite.SetActive(true);

                lr.enabled = true;

                float elapsedTime = 0f;

                while (elapsedTime < fadeTime)
                {
                    startSprite.transform.position = firePoint.position;
                    startSprite.transform.rotation = firePoint.rotation * Quaternion.Euler(0, 0, 90f);

                    endSprite.transform.position = finalPoint;
                    endSprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(finalPoint.y - firePoint.position.y, finalPoint.x - firePoint.position.x) * Mathf.Rad2Deg);

                    middleSprite.transform.position = (firePoint.position + finalPoint) / 2f;
                    middleSprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(finalPoint.y - firePoint.position.y, finalPoint.x - firePoint.position.x) * Mathf.Rad2Deg);
                    middleSprite.transform.localScale = new Vector3(Vector2.Distance(firePoint.position, finalPoint), middleSprite.transform.localScale.y, middleSprite.transform.localScale.z);

                    elapsedTime += Time.deltaTime;

                    yield return null;
                }

                yield return new WaitForSeconds(fadeTime);

                lr.enabled = false;
                startSprite.SetActive(false);
                endSprite.SetActive(false);
                middleSprite.SetActive(false);

                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
}