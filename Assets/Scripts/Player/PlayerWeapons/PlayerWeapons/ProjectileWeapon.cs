using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : PlayerBasicWeapon
{
    private int ProjectileCount
    {
        get { return projectileCount; }

        set
        {
            if (projectileCount != value)
            {
                projectileCount = value;
                CheckActivation();
            }
        }
    }

    protected float projectileSpeed;
    protected int projectileCount;

    public GameObject fp_1;
    public GameObject fp_2;
    public GameObject fp_3;
    public GameObject fp_4;

    private Transform fp_1Child;
    private Transform[] fp_2Children;
    private Transform[] fp_3Children;
    private Transform[] fp_4Children;

    public GameObject projectile;

    protected override void Awake()
    {
       base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        CheckActivation();

        CheckFP();
    }

    protected override void Shoot()
    {
        base.Shoot();

        int activeFP = -1;

        if (fp_1.activeSelf) activeFP = 1;
        else if (fp_2.activeSelf) activeFP = 2;
        else if (fp_3.activeSelf) activeFP = 3;
        else if (fp_4.activeSelf) activeFP = 4;

        if (activeFP == -1)
        {
            Debug.LogWarning("No active FP detected!");
            return;
        }

        switch (activeFP)
        {
            case 1:
                ProjectileFire(fp_1Child);
                break;

            case 2:
                foreach (Transform child in fp_2Children)
                {
                    ProjectileFire(child);
                }
                break;

            case 3:
                foreach (Transform child in fp_3Children)
                {
                    ProjectileFire(child);
                }
                break;

            case 4:
                foreach (Transform child in fp_4Children)
                {
                    ProjectileFire(child);
                }
                break;
            default:
                Debug.LogError("Unsupported FP type: " + activeFP);
                break;
        }
    }

    private void ProjectileFire(Transform firePoint)
    {
        GameObject shotProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);

        shotProjectile.GetComponent<ProjectileScript>().FireProjectile(CheckDamage(damage), projectileSpeed, range, enemyHit);
    }

    private void CheckFP()
    {
        if (fp_1.transform.childCount > 0)
        {
            fp_1Child = fp_1.transform.GetChild(0);
        }

        fp_2Children = new Transform[fp_2.transform.childCount];

        for (int i = 0; i < fp_2.transform.childCount; i++)
        {
            fp_2Children[i] = fp_2.transform.GetChild(i);
        }

        fp_3Children = new Transform[fp_3.transform.childCount];
        for (int i = 0; i < fp_3.transform.childCount; i++)
        {
            fp_3Children[i] = fp_3.transform.GetChild(i);
        }

        fp_4Children = new Transform[fp_4.transform.childCount];
        for (int i = 0; i < fp_4.transform.childCount; i++)
        {
            fp_4Children[i] = fp_4.transform.GetChild(i);
        }
    }

    private void CheckActivation()
    {
        GameObject[] firePoints = { fp_1, fp_2, fp_3, fp_4 };

        foreach (GameObject fp in firePoints)
        {
            fp.SetActive(false);
        }

        if (projectileCount > 0 && projectileCount <= firePoints.Length)
        {
            firePoints[projectileCount - 1].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid projectileCount: " + projectileCount);
        }
    }

    protected override void GetStats()
    {
        base.GetStats();

        projectileSpeed = PlayerStatsManager.Instance.projectileSpeed;
        ProjectileCount = PlayerStatsManager.Instance.projectileCount;
    }
}
